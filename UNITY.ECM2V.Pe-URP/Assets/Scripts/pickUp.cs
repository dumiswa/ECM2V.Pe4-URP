using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
    public Camera rayCamera;
    public GameObject Player;
    public bool isLockedOn = false;

    public float rayDistance;
    public float followSpeed = 5.0f;

    bool isItemSelected = false;
    bool canPlace = false;

    public GameObject selectedItem;
    public GameObject lastSelectedItem;
    public GameObject pickedUpItem;
    public GameObject fire;

    public Transform campBlueprint;

    public Transform pickUpPoint;
    public Transform fireBlueprint;

    public Color originalColor;
    public Color highlightedColor = Color.cyan;

    LayerMask item;
    LayerMask campFire;
    LayerMask groundLayerMask;

    RaycastHit hit;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        item = LayerMask.NameToLayer("item");
        campFire = LayerMask.NameToLayer("CampFire");
        groundLayerMask = LayerMask.NameToLayer("whatIsGround");
        GameObject particleSystemObject = fire;
        particleSystemObject.SetActive(false);
    }


    void Update()
    {

        Ray ray = rayCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, rayDistance))
        {

            selectedItem = hit.collider.gameObject;

            if (hit.transform.gameObject.layer == groundLayerMask)
            {
                if (Input.GetMouseButton(0))
                {

                    rayDistance = 5;

                    campBlueprint.position = new Vector3(hit.point.x, 0, hit.point.z);
                    //campBlueprint.rotation = Quaternion.identity;
                }
            }

            if (hit.transform.gameObject.layer == item)
            {
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectedItem != null && pickedUpItem == null)
            {
                PickUp();
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (pickedUpItem != null )
            {
                Drop();
            }
        }

        //if (Input.GetKeyDown(KeyCode.E) && canPlace)
        //{
        //    Place();
        //}

        //if (Physics.Raycast(ray, out hit, rayDistance))
        //{
        //    if (hit.transform.gameObject.layer == campFire)
        //    {
        //        if (pickedUpItem != null)
        //        {
        //            print("placed item");
        //            canPlace = true;
        //        }

        //        if (Input.GetKey(KeyCode.X))
        //        {
        //            // isLockedOn = !isLockedOn;
        //            startFire();
        //        }

        //        if (isLockedOn)
        //        {

        //        }
        //    }

        //    else
        //    {
        //        canPlace = false;
        //    }
        //}

        //fireBlueprint.position = new Vector3(hit.point.x, 0, hit.point.z);
        //fireBlueprint.rotation = Quaternion.identity;


    }

    void FixedUpdate()
    {

    }


    void PickUp()
    {
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

    void Place()
    {
        Destroy(pickedUpItem.gameObject);

        Color placedColor = new Color(255f / 255f, 87f / 255f, 51f / 255f);

        Transform[] cubes = fireBlueprint.GetComponentsInChildren<Transform>();

        foreach (Transform cube in cubes)
        {
            if (cube != fireBlueprint)
            {
                Renderer cubeRenderer = cube.GetComponent<Renderer>();

                if (cubeRenderer != null)
                {
                    cubeRenderer.material.color = placedColor;
                }
            }
        }
        pickedUpItem = null;
        canPlace = false;
    }

    void startFire()
    {
        /*  Vector3 lockInPosition = fireBlueprint.position;
          lockInPosition.z = transform.position.z;

          transform.position = Vector3.Lerp(transform.position, lockInPosition, followSpeed);
          print("lockIn");*/
        GameObject particleSystemObject = fire;
        particleSystemObject.SetActive(true);
    }

}
