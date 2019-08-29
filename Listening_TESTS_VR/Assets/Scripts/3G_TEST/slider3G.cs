using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class slider3G : MonoBehaviour
{


    public GameObject slider;

    public int sliderValue;



    public GameObject[] snapPostions;



    void Start()
    {
        // Setup the tag for the object
        this.gameObject.tag = "Slider";

        // Set the gameObject layer as interactable
        this.gameObject.layer = 10;



     

    }


    void Update()
    {

   sliderValue = updateValue();


    }



    private int updateValue()
    {
        Vector3 pos = slider.transform.position;

        int value = 0;

  

      

        if(pos == snapPostions[0].transform.position)
        {
            value = snapPostions[0].GetComponent<highlightSnap>().value;
        }
        else if (pos == snapPostions[1].transform.position)
        {
            value = snapPostions[1].GetComponent<highlightSnap>().value;
        }
        else if (pos == snapPostions[2].transform.position)
        {
            value = snapPostions[2].GetComponent<highlightSnap>().value;
        }
        else if (pos == snapPostions[3].transform.position)
        {
            value = snapPostions[3].GetComponent<highlightSnap>().value;
        }
        else if (pos == snapPostions[4].transform.position)
        {
            value = snapPostions[4].GetComponent<highlightSnap>().value;
        }
        else if (pos == snapPostions[5].transform.position)
        {
            value = snapPostions[5].GetComponent<highlightSnap>().value;
        }
        else if (pos == snapPostions[6].transform.position)
        {
            value = snapPostions[6].GetComponent<highlightSnap>().value;
        }

    


        return value;
    }
}
