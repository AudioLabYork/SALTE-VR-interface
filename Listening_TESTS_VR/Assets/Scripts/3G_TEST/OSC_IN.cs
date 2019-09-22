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

    public float[] sliderValues;
    public int[] buttonStates;

    bool visibleUI;
    bool updateSlidersLatch;

    public TextMeshPro screenMessage;
    public string messageReceived;

    public TextMeshPro smallScreenMessage;
    public string smallMessageReceived;

    Outline outline;
    public GameObject[] buttons;
    public GameObject[] sliders;

    public Material[] material;

    private void Start()
    {
        sliderValues = new float[4];
        buttonStates = new int[5];

        // set text
        messageReceived = "\n\nSpatial Audio Listening Test Environment";

        // hide UI
        visibleUI = false;
        updateSlidersLatch = false;

        var server = new OscServer(oscPortIN); // Port number
        Debug.Log("OSC server created");

        // Receives OSC data to display two messages on the screen
        server.MessageDispatcher.AddCallback(
               "/screen", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsString(1) != null)
                   {
                       // Debug.Log("screen callback");
                       // Debug.Log(data.GetElementAsString(0) + " - " + data.GetElementAsString(1));
                       string message1 = data.GetElementAsString(0);
                       smallMessageReceived = message1;
                       string message2 = data.GetElementAsString(1);
                       messageReceived = message2;
                   }
               }
           );

        // Receives OSC data to show / hide UI
        server.MessageDispatcher.AddCallback(
               "/showUI", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsInt(0) != null)
                   {
                       if (data.GetElementAsInt(0) == 1)
                       {
                           visibleUI = true;
                           updateSlidersLatch = true;
                       }
                       else
                       {
                           visibleUI = false;
                           updateSlidersLatch = false;
                       }
                   }
               }
           );

        // Receives OSC data to control button hightlights
        server.MessageDispatcher.AddCallback(
               "/button", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsInt(1) != null)
                   {
                       // Debug.Log("button callback");
                       // Debug.Log(data.GetElementAsString(0) + " - " + data.GetElementAsInt(1));
                       string oscButton = data.GetElementAsString(0);
                       int state = data.GetElementAsInt(1);

                       if (oscButton == "play") buttonStates[0] = state;
                       else if (oscButton == "stop") buttonStates[1] = state;
                       else if (oscButton == "loop") buttonStates[2] = state;
                       else if (oscButton == "A") buttonStates[3] = state;
                       else if (oscButton == "B") buttonStates[4] = state;
                   }
               }
           );

        // Receives OSC data to control sliders 
        server.MessageDispatcher.AddCallback(
              "/slider", // OSC address
              (string address, OscDataHandle data) =>
              {
                  if (data.GetElementAsInt(0) != null && data.GetElementAsFloat(1) != null)
                  {
                      Debug.Log("slider callback");
                      Debug.Log(data.GetElementAsInt(0) + " - " + data.GetElementAsFloat(1));
                      int index = data.GetElementAsInt(0);
                      float value = data.GetElementAsFloat(1);
                      if(sliderValues[index] != null) sliderValues[index] = value;
                  }
              }
          );



        // initialise sliders and buttons
        updateSliders();
        highlightButtons();

        // set UI visibility
        showUI(visibleUI);
    }

    private void Update()
    {
        if (screenMessage.text != messageReceived) screenMessage.text = messageReceived;
        if (smallScreenMessage.text != smallMessageReceived) smallScreenMessage.text = smallMessageReceived;

        updateSliders();
        highlightButtons();
        showUI(visibleUI);
    }

    // Takes in OSC data and changes value of the slider
    private void updateSliders()
    {
        if (updateSlidersLatch == true)
        {
            for (int i = 0; i < sliders.Length; ++i)
            {
                H_slider oscSendScript = sliders[i].GetComponent<H_slider>();
                oscSendScript.enabled = false;

                sliderScale slider = sliders[i].GetComponent<sliderScale>();
                slider.scaledAmount = (sliderValues[i] + 3) / 6;

                oscSendScript.enabled = true;
            }
            updateSlidersLatch = false;
        }
    }

    // Takes in OSC data and highlights button
    private void highlightButtons()
    {
        for (int i = 0; i < buttons.Length; ++i)
        {
            if (buttonStates[i] == 1) buttons[i].GetComponent<Renderer>().material = material[1];
            else buttons[i].GetComponent<Renderer>().material = material[0];
        }
    }

    private void showUI(bool show)
    {
        for (int i = 0; i < sliders.Length; ++i)
        {
            sliders[i].SetActive(show);
        }

        buttons[3].SetActive(show);
        buttons[4].SetActive(show);
    }
}
