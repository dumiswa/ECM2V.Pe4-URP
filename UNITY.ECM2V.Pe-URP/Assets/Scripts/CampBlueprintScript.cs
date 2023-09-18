using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampBlueprintScript : MonoBehaviour
{

    public Color validColor;
    public Color invalidColor;

    public Material ChildMaterial;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {

        ChildMaterial.color = invalidColor;
    }

    void OnTriggerExit(Collider other)
    {
        ChildMaterial.color = validColor;
    }

}
