using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : MonoBehaviour
{

    #region Events
    public static UnityAction OnTouchpadUp = null;
    public static UnityAction OnTouchpadDwn = null;
    public static UnityAction<OVRInput.Controller, GameObject> OnControllerSource = null;
    #endregion

    public Pointer pointer;



    // Update is called once per frame
    void Update()
    {

        
        //Check for actual input
        Input();


    }
          
   private void Input()
    {

        // Index Trigger Left - PrimaryIndexTrigger
        if(OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger,OVRInput.Controller.LTouch))
        {
            Debug.Log("Left index trigger Down");
            OnLeftTriggerDown();
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            Debug.Log("Left index trigger Up");
            OnLeftTriggerUp();
        }




        // Index Trigger Right - SecondaryIndexTrigger
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("Right index trigger Down");
            OnRightTriggerDown();

        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("Right index trigger Up");
            OnRightTriggerUp();
        }


        // Hand Tigger Left - PrimaryHandTrigger



        // Hand Trigger Right - SecondaryHandTrigger

    }

    private void OnRightTriggerUp()
    {
       
    }

    private void OnLeftTriggerUp()
    {
       
    }

    private void OnRightTriggerDown()
    {
        pointer.ProcessTouchPadDown();
    }

    private void OnLeftTriggerDown()
    {
        
    }

    private OVRInput.Controller UpdateSource(OVRInput.Controller check, OVRInput.Controller previous)
    {

        return check;
    }
}
