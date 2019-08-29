using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightSnap : MonoBehaviour
{
    Renderer rend;

    public GameObject slider;
    public int value;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        slider.SetActive(false);
    }

    public void snapPostion(Vector3 pos)
    {
        slider.SetActive(true);
        slider.transform.position = pos;
    }

    private void OnTriggerEnter(Collider other)
    {      
        rend.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {    
        rend.enabled = false;  
    }


}
