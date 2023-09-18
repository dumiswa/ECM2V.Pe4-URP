using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCamera : MonoBehaviour
{
    public Transform cameraPosition;
    void Start()
    {
        cameraPosition = GameObject.Find("CameraPos").transform;
    }

    void Update()
    {
        transform.position = cameraPosition.position;
    }
}
