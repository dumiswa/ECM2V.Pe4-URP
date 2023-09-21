using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class pickUp : MonoBehaviour
{
    public Camera rayCamera;
    public GameObject Player;
    public bool isLockedOn = false;

    public float itemPickUpDistance;
    public float campfireBlueprintDistance;
    public float followSpeed = 5.0f;

    bool isItemSelected = false;
    bool isLookingAtFire = false;

    bool toPlaceCampfireBlueprint = false;

    public GameObject selectedItem;
    public GameObject lastSelectedItem;
    public GameObject pickedUpItem;
    //public GameObject fire;

    public GameObject campBlueprint;
    public GameObject camp;

    public Transform pickUpPoint;
    public Transform fireBlueprint;

    public Color originalColor;
    public Color highlightedColor = Color.cyan;

    public TMP_Text guideText;

    LayerMask item;
    LayerMask campFire;
    LayerMask groundLayerMask;

    RaycastHit hitForSmallRange;
    RaycastHit hitForCampBlueprint;

    [SerializeField] AudioSource pickUpSound;
    [SerializeField] AudioSource placeWoodSound;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        item = LayerMask.NameToLayer("item");
        campFire = LayerMask.NameToLayer("CampFire");
        groundLayerMask = LayerMask.NameToLayer("whatIsGround");
        //GameObject particleSystemObject = fire;
        //particleSystemObject.SetActive(false);
    }


    void Update()
    {

        guideText.text = "";
        isLookingAtFire = false;

        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hitForSmallRange, itemPickUpDistance))
        {

            selectedItem = hitForSmallRange.collider.gameObject;

            if (hitForSmallRange.transform.gameObject.layer == item)
            {
                guideText.text = "Press E to pick up";

                if (isItemSelected == false)
                {                   
                    isItemSelected = true;

                    originalColor = selectedItem.GetComponent<Renderer>().material.color;
                    selectedItem.GetComponent<Renderer>().material.color = highlightedColor;
                    lastSelectedItem = selectedItem;                   
                }

                if (selectedItem != lastSelectedItem && isItemSelected)
                {
                    if (lastSelectedItem != null)
                    {
                        lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
                    }
                    isItemSelected = false;
                    lastSelectedItem = null;
                }
            }

            else
            {
                if (lastSelectedItem != null)
                {
                    lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
                }
                isItemSelected = false;
                lastSelectedItem = null;
            }

            if (hitForSmallRange.transform.gameObject.layer == campFire)
            {
                isLookingAtFire = true;

                if (pickedUpItem != null)
                {
                    guideText.text = "Press G to place in the campfire";
                }

            }
            
        }

        else
        {
            if (lastSelectedItem != null)
            {
                lastSelectedItem.GetComponent<Renderer>().material.color = originalColor;
            }
            isItemSelected = false;
            lastSelectedItem = null;
            selectedItem = null;
        }



        if (Physics.Raycast(ray, out hitForCampBlueprint, campfireBlueprintDistance))
        {


            if (toPlaceCampfireBlueprint)
            {
                campBlueprint.transform.position = new Vector3(hitForCampBlueprint.point.x, 0, hitForCampBlueprint.point.z);
                campBlueprint.transform.rotation = Quaternion.identity;
            }

        }

        

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedItem != null && pickedUpItem == null && hitForSmallRange.transform.gameObject.layer == item && !toPlaceCampfireBlueprint)
            {
                PickUp();
            }

            if (hitForCampBlueprint.transform.gameObject.layer == groundLayerMask && campBlueprint!=null && pickedUpItem == null)
            {
                campBlueprint.SetActive(true);
                toPlaceCampfireBlueprint = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (pickedUpItem != null)
            {
                if (!isLookingAtFire)
                {
                    Drop();
                }
                else
                {
                    Destroy(pickedUpItem);

                    hitForSmallRange.transform.gameObject.GetComponent<CampFireBehaviour>().AddLogs();
                    placeWoodSound.Play();
                }
                
            }

            else if (toPlaceCampfireBlueprint && campBlueprint.GetComponent<CampBlueprintScript>().ChildMaterial.color != campBlueprint.GetComponent<CampBlueprintScript>().invalidColor)
            {
                PlaceCampFire();
                toPlaceCampfireBlueprint = false;
            }
        }


    }

    void FixedUpdate()
    {

    }


    void PickUp()
    {
        pickUpSound.Play();
        selectedItem.GetComponent<Rigidbody>().useGravity = false;
        selectedItem.GetComponent<Rigidbody>().isKinematic = true;
        selectedItem.GetComponent<CapsuleCollider>().enabled = false;
        selectedItem.transform.position = Vector3.zero;
        selectedItem.transform.rotation = Quaternion.identity;
        selectedItem.transform.SetParent(pickUpPoint, false);
        pickedUpItem = selectedItem;
    }

    void Drop()
    {
        pickedUpItem.GetComponent<CapsuleCollider>().enabled = true;
        pickedUpItem.GetComponent<Rigidbody>().useGravity = true;
        pickedUpItem.GetComponent<Rigidbody>().isKinematic = false;
        pickedUpItem.transform.parent = null;
        pickedUpItem = null;
        selectedItem = null;
    }

    void PlaceCampFire()
    {
        Instantiate(camp, campBlueprint.transform.position, Quaternion.identity);
        Destroy(campBlueprint);
    }


}
