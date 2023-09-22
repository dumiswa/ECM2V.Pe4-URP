using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogues : MonoBehaviour
{
    public TMP_Text guideText;
    [SerializeField] AudioSource pickUpSound;

    public BoxCollider entrance;

    public Animator animator;

    public pickUp pickUpScript;

    bool todo = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) ) 
        {
            pickUpSound.Play();
            guideText.text = "";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12 && !todo)
        {
            guideText.text = "Press E to get the backpack";
            entrance.isTrigger = true;
            todo = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 13) 
        {
            guideText.text = "Brr, It's getting cold, I think it would be a great idea to set up a campfire.";
            animator.SetBool("toAnimate", true);
            StartCoroutine(DelayedExecution());
        }
    }

    public void Burning()
    {
        pickUpScript.toDo = false;
        guideText.text = "Oh crap...";

    }

    private IEnumerator DelayedExecution()
    {
        yield return new WaitForSeconds(3f); // Wait for 3 seconds


        pickUpScript.toDo = true;
    }

}
