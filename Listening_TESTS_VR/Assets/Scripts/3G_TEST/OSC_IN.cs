using System.Collections;
using System.Collections.Generic;
using static UnityEditor.ArrayUtility;
using UnityEngine;
using TMPro;
using OscJack;

public class OSC_IN : MonoBehaviour
{
    public string IPAddress = "127.0.0.1"; // IP address for OSC 
    public int oscPortIN = 6000; // Port for OSC
    OscServer server;

    public float[] sliderValues, sliderValuesReceived;
    public int[] buttonStates, buttonStatesReceived;

    public TextMeshPro screenMessage;
    public string messageReceived;

    public TextMeshPro smallScreenMessage;
    public string smallMessageReceived;

    Outline outline;
    public GameObject[] buttons;
    public GameObject[] sliders;

    private void Start()
    {
        sliderValues = new float[4];
        sliderValuesReceived = new float[4];

        buttonStates = new int[5];
        buttonStatesReceived = new int[5];

        // set text
        messageReceived = "\n\nSpatial Audio Listening Test Environment";

        var server = new OscServer(oscPortIN); // Port number
        Debug.Log("server created");

        // Recieves OSC data to control button hightlights
        server.MessageDispatcher.AddCallback(
               "/ts26259/button", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsInt(1) != null)
                   {
                       Debug.Log("button callback");
                       Debug.Log(data.GetElementAsString(0) + " - " + data.GetElementAsInt(1));
                       string oscButton = data.GetElementAsString(0);
                       int state = data.GetElementAsInt(1);

                       if (oscButton == "play") buttonStatesReceived[0] = state;
                       else if (oscButton == "stop") buttonStatesReceived[1] = state;
                       else if (oscButton == "loop") buttonStatesReceived[2] = state;
                       else if (oscButton == "A") buttonStatesReceived[3] = state;
                       else if (oscButton == "B") buttonStatesReceived[4] = state;
                   }
               }
           );

        // Receives OSC data to control sliders 
        server.MessageDispatcher.AddCallback(
              "/ts26259/slider", // OSC address
              (string address, OscDataHandle data) =>
              {
                  if (data.GetElementAsInt(0) != null && data.GetElementAsFloat(1) != null)
                  {
                      Debug.Log("slider callback");
                      Debug.Log(data.GetElementAsInt(0) + " - " + data.GetElementAsFloat(1));
                      int index = data.GetElementAsInt(0);
                      float value = data.GetElementAsFloat(1);
                      if(sliderValuesReceived[index] != null) sliderValuesReceived[index] = value;
                  }
              }
          );

        // Recieves OSC data to display two messages on the screen
        server.MessageDispatcher.AddCallback(
               "/ts26259/screen", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsString(1) != null)
                   {
                       Debug.Log("screen callback");
                       Debug.Log(data.GetElementAsString(0) + " - " + data.GetElementAsString(1));
                       string message1 = data.GetElementAsString(0);
                       smallMessageReceived = message1;
                       string message2 = data.GetElementAsString(1);
                       messageReceived = message2;
                   }
               }
           );

        // initialise sliders and buttons
        for (int i = 0; i < 4; ++i)
        {
            checkOscSlider(i, sliderValues[i]);
        }

        for (int i = 0; i < 5; ++i)
        {
            checkOscButton(i, buttonStates[i]);
        }
    }

    private void Update()
    {
        if (!ArrayEquals(sliderValues, sliderValuesReceived))
        {
            sliderValues = (float[])sliderValuesReceived.Clone();

            for (int i = 0; i < 4; ++i)
            {
                checkOscSlider(i, sliderValues[i]);
            }
        }

        //if (!ArrayEquals(buttonStates, buttonStatesReceived))
        if (true) // this could be fixed, so buttons don't have to be updated every frame...
        {
            buttonStates = (int[])buttonStatesReceived.Clone();

            for (int i = 0; i < 5; ++i)
            {
                checkOscButton(i, buttonStates[i]);
            }
        }

        if (screenMessage.text != messageReceived) screenMessage.text = messageReceived;
        if (smallScreenMessage.text != smallMessageReceived) smallScreenMessage.text = smallMessageReceived;
    }

    // Takes in OSC data and changes value of scale
    private void checkOscSlider(int index, float value)
    {
        sliderScale slider = sliders[index].GetComponent<sliderScale>();
        slider.scaledAmount = (value + 3) / 6;
        Debug.Log("received: " + index + " / "+ value);
    }

    // Takes in OSC data and highlights button
    void checkOscButton(int index, int state)
    {
        if (state == 1)
        {
            //Renderer buttonRenderer = buttons[index].GetComponent<Renderer>();
            //buttonRenderer.material.shader = Shader.Find("_Color");
            //buttonRenderer.material.SetColor("_Color", Color.red);

            
            outline = buttons[index].GetComponent<Outline>();
            outline.enabled = true;
        }
        else
        {
            //Renderer buttonRenderer = buttons[index].GetComponent<Renderer>();
            //buttonRenderer.material.shader = Shader.Find("_Color");
            //buttonRenderer.material.SetColor("_Color", Color.blue);

            outline = buttons[index].GetComponent<Outline>();
            outline.enabled = false;
        }
    }
}
