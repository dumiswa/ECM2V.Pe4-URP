using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walkingAudio : MonoBehaviour
{
    [SerializeField] public AudioSource grassWalk;

    private bool isWalking = false;

    void Update()
    {
        // Check if any movement key is pressed
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

        // If any movement key is pressed and the audio is not playing, start playing it
        if (isMoving && !isWalking)
        {
            grassWalk.Play();
            grassWalk.loop = true;
            isWalking = true;
        }
        // If no movement keys are pressed and the audio is playing, stop it
        else if (!isMoving && isWalking)
        {
            grassWalk.Stop();
            isWalking = false;
        }
    }
}