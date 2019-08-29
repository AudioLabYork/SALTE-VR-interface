using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class previewVIEW : MonoBehaviour
{
    public Material[] mats;

    public GameObject cube;

  

    private void Start()
    {
      
    }

    public void UpdatePreview(int i)
    {
        if(i == 0)
        {
            cube.GetComponent<Renderer>().enabled = true;
            cube.GetComponent<Renderer>().material = mats[0];
        }

        if (i == 1)
        {
            cube.GetComponent<Renderer>().enabled = true;
            cube.GetComponent<Renderer>().material = mats[1];
        }
        if (i == 3)
        {
            cube.GetComponent<Renderer>().enabled = false;
         
        }
    }
}
