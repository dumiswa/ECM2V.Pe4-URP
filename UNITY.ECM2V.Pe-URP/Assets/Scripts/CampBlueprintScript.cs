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
        ChildMaterial.color = validColor;
    }

    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("whatIsGround")) {
            ChildMaterial.color = invalidColor;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        ChildMaterial.color = validColor;
    }

}
