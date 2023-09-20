using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingAudio : MonoBehaviour
{
    [SerializeField] public AudioSource grassWalk;

    private bool isWalking = false;

    void Update()
    {     
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        if (isMoving && !isWalking)
        {
            grassWalk.Play();
            grassWalk.loop = true;
            isWalking = true;
        }

        else if (!isMoving && isWalking)
        {
            grassWalk.Stop();
            isWalking = false;
        }
    }
}