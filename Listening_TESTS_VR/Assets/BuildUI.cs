using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildUI : MonoBehaviour
{

    OSC_IN _osc;


    

    // Recieves messgaes from renderer via OSC to place the UI

    // Define Test Paradigm
    public bool _isMushra;
    public bool _is3G;

    // Define the amount of sliders
    public int _sliderAmount;



    // Start is called before the first frame update
    void Start()
    {
        _osc = GetComponent<OSC_IN>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    public void SetText()
    {
     
    }
   
    public void SetSlidersMushra()
    {

    }


    public void SetSliders3G()
    {

    }
    

    public void ClearUI()
    {
        
    }
}
