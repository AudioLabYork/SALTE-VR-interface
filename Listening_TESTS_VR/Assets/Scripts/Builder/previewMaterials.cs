using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewMaterials : MonoBehaviour
{

    public Material[] materials;

    public void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerEnter(Collision collision)
    {
        if (collision.gameObject.tag == "node")
        {
            updateMaterial(1);
        }
        else
            updateMaterial(2);
    }


    public void updateMaterial(int index)
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material = materials[index];
        
    }

    
}
