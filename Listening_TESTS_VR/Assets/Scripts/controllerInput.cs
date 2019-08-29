using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class controllerInput : MonoBehaviour
{

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 sliderAction;

    public Vector2 getSlider()
    {
        return sliderAction.GetAxis(handType);

    }



    void Update()
    {

        print("slider values is " + sliderAction.GetAxis(handType));
            
                
                
                }
}
