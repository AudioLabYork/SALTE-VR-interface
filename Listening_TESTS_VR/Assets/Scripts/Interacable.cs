using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacable : MonoBehaviour
{

    public Pointer pointer;

    public bool sizeDebug;

    public Material[] materials;

    public void Pressed()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        bool flip = !renderer.enabled;


        renderer.enabled = flip;

    }

    private void Update()
    {
        if (pointer.isSlider == true)
            increase();
        else
            decrease();




    }

    public void increase()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = materials[1];
    }

    public void decrease()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        renderer.material = materials[0];
    }



}
