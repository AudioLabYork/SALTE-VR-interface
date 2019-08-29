using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Valve.VR;

public class sliderScale : MonoBehaviour
{
    public GameObject slider;
    private float sliderMin = 0.0f;
    private float sliderMax = 1.0f;

    public TextMeshPro text;

    public float scaledAmount, b;

    public float userLimitMax = 3;
    public float userLimitMin = -3;



    Vector2 joystickInput;


    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Vector2 sliderAction;



    public DataSave_Slider sliderValue;


    private void Start()
    {
        
    }


    public void scaleAmount()
    {

        if (joystickInput.y > 0)
        {
            scaledAmount += 0.001f * ((Mathf.Abs(joystickInput.y * 500f) * Time.deltaTime));
            //   Debug.Log("up pressed");
        }

        if (joystickInput.y < 0)
        {
            scaledAmount -= 0.001f * ((Mathf.Abs(joystickInput.y)) * 500f) * Time.deltaTime;
            //    Debug.Log("up pressed");
        }

        Debug.Log(joystickInput.y);
    }

    public void resetScale()
    {
        scaledAmount = 0;
    }
  

    // Update is called once per frame
    void Update()
    {

     


        joystickInput = sliderAction.GetAxis(handType);

     //   print("x = " + joystickInput.x + " y = " + joystickInput.y);

        UpdateScale();
    }

    public  void UpdateScale()
    {
        scaledAmount = Mathf.Clamp(scaledAmount, sliderMin, sliderMax);

        slider.transform.localScale = new Vector3(1, scaledAmount, 1);

        float newScaled = scale(0.0f, 1.0f, userLimitMin, userLimitMax, scaledAmount);

        text.text = Math.Round(newScaled, 1).ToString();
        // text.text = Mathf.RoundToInt(scaledAmount * 100).ToString();
        sliderValue.sliderValue = (float)Math.Round(newScaled, 1);
    }

        private float scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }


}
