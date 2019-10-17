using System.Collections;
using System.Collections.Generic;
using static UnityEditor.ArrayUtility;
using UnityEngine;
using TMPro;
using OscJack;
using System;

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
    public bool referenceButtonPresent = false;
    public bool ABbuttonsPresent = false;


    public List<GameObject> sliders = new List<GameObject>();

    public Material[] material;

    public int slidersNum;

    public bool createUI;
    public bool clearUI;

    public List<string> labelStrings = new List<string>();
    public List<GameObject> labels = new List<GameObject>();

    public int numberOfAttributeLabels;
    public string[] attributeLabels;

    // UI Placer Objects 
    [SerializeField] SliderPlacer _sliders;
    [SerializeField] TextPlacer _text;

    [SerializeField] int testParadigm;
    [SerializeField] OSCTester oscManager;

    public bool uiStart;

    public int showUIi;

    private void Start()
    {
        BlankList();

        sliderValues = new float[12];
        for (int i = 0; i < sliderValues.Length; ++i)
        {
            sliderValues[i] = 0;
        }

        buttonStates = new int[6];
        for (int i = 0; i < buttonStates.Length; ++i)
        {
            buttonStates[i] = 0;
        }

        attributeLabels = new string[4];

        // set text
        messageReceived = "\n\nSpatial Audio Listening Test Environment";

        // hide UI
        visibleUI = false;
        updateSlidersLatch = false;


        var server = new OscServer(oscPortIN); // Port number
        Debug.Log("OSC server created");

        // Receives OSC data to display two messages on the screen
        server.MessageDispatcher.AddCallback(
               "/screenMessages", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsString(1) != null)
                   {
                       smallMessageReceived = data.GetElementAsString(0);
                       messageReceived = data.GetElementAsString(1);
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

                       showUIi = data.GetElementAsInt(0);

                       if (data.GetElementAsInt(0) == 1)
                       {

                           createUI = true;
                           clearUI = false;
                           visibleUI = false;
                           updateSlidersLatch = true;
                       }
                       else
                       {
                           createUI = false;
                           clearUI = true;
                           visibleUI = true;
                           updateSlidersLatch = false;
                       }
                   }
               }
           );
        server.MessageDispatcher.AddCallback(
               "/RefTrigButtonPresent", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsInt(0) == 1)
                   {
                       referenceButtonPresent = true;
                       ABbuttonsPresent = false;
                   }
                   else
                   {
                       referenceButtonPresent = false;
                       ABbuttonsPresent = true;
                   }
               }
           );

        server.MessageDispatcher.AddCallback(
               "/ABTrigButtonsPresent", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsInt(0) == 1)
                   {
                       ABbuttonsPresent = true;
                       referenceButtonPresent = false;
                   }

                   else
                   {
                       ABbuttonsPresent = false;
                       referenceButtonPresent = true;
                   }
               }
           );

        // Receives OSC data to control button hightlights
        server.MessageDispatcher.AddCallback(
               "/buttonState", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsInt(1) != null)
                   {
                       string oscButton = data.GetElementAsString(0);
                       int state = data.GetElementAsInt(1);

                       if (oscButton == "play") buttonStates[0] = state;
                       else if (oscButton == "stop") buttonStates[1] = state;
                       else if (oscButton == "loop") buttonStates[2] = state;
                       else if (oscButton == "A") buttonStates[3] = state;
                       else if (oscButton == "B") buttonStates[4] = state;
                       else if (oscButton == "reference") buttonStates[5] = state;
                   }
               }
           );


        // Receives OSC data to control button hightlights
        server.MessageDispatcher.AddCallback(
               "/buttonState", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsString(0) != null && data.GetElementAsInt(1) != null)
                   {
                       string oscButton = data.GetElementAsString(0);
                       int state = data.GetElementAsInt(1);

                       if (oscButton == "play") buttonStates[0] = state;
                       else if (oscButton == "stop") buttonStates[1] = state;
                       else if (oscButton == "loop") buttonStates[2] = state;
                       else if (oscButton == "A") buttonStates[3] = state;
                       else if (oscButton == "B") buttonStates[4] = state;
                       else if (oscButton == "reference") buttonStates[5] = state;
                   }
               }
           );

        server.MessageDispatcher.AddCallback(
               "/condTrigButtonState", // OSC address
               (string address, OscDataHandle data) =>
               {
                   if (data.GetElementAsInt(0) != null && data.GetElementAsInt(1) != null)
                   {
                       int sliderIndex = data.GetElementAsInt(0); // 0,1,2,3... equals A,B,C,D...
                       int state = data.GetElementAsInt(1); // 0 off, 1 on
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

                      sliderValues[data.GetElementAsInt(0)] = data.GetElementAsFloat(1);

                      if (sliderValues[index] != null) sliderValues[index] = value;
                  }
              }
          );

        // Receives messages to determine the slider type and amount 
        server.MessageDispatcher.AddCallback(
                  "/numOfSliders", // OSC address
                  (string address, OscDataHandle data) =>
                  {
                      if (data.GetElementAsInt(0) != null)
                      {
                          if (data.GetElementAsInt(0) > 0)
                          {
                              uiStart = true;
                          }
                          oscManager.numberOfSliders = data.GetElementAsInt(0);
                          slidersNum = data.GetElementAsInt(0);
                      }
                  }
              );

        server.MessageDispatcher.AddCallback(
                 "/sliderState", // OSC address
                 (string address, OscDataHandle data) =>
                 {
                     if (data.GetElementAsInt(0) != null && data.GetElementAsFloat(1) != null && data.GetElementAsFloat(2) != null && data.GetElementAsFloat(3) != null)
                     {
                         if (data.GetElementAsInt(3) == 3.00f && uiStart)
                         {
                             oscManager.is3G = true;
                             oscManager.isMushra = false;
                             uiStart = false;
                         }
                         if (data.GetElementAsFloat(3) == 100.0f && uiStart)
                         {
                             oscManager.is3G = false;
                             oscManager.isMushra = true;
                             uiStart = false;
                         }

                         sliderValues[data.GetElementAsInt(0)] = data.GetElementAsFloat(1);
                     }
                 }
             );


        // receives messages about labels 
        server.MessageDispatcher.AddCallback(
                  "/numOfRatingLabels", // OSC address
                  (string address, OscDataHandle data) =>
                  {
                      if (data.GetElementAsInt(0) != null)
                      {
                          oscManager.numberOfLabels = data.GetElementAsInt(0);
                      }
                  }
              );

        server.MessageDispatcher.AddCallback(
                "/ratingLabel", // OSC address
                (string address, OscDataHandle data) =>
                {
                    if (data.GetElementAsInt(0) != null && data.GetElementAsString(1) != null)
                    {
                        labelStrings[data.GetElementAsInt(0)] = data.GetElementAsString(1);
                    }
                }
            );

        // receives messages about labels 
        server.MessageDispatcher.AddCallback(
                  "/numOfAttributeLabels", // OSC address
                  (string address, OscDataHandle data) =>
                  {
                      if (data.GetElementAsInt(0) != null)
                      {
                          //oscManager.numberOfLabels = data.GetElementAsInt(0);
                          numberOfAttributeLabels = data.GetElementAsInt(0);

                      }
                  }
              );

        server.MessageDispatcher.AddCallback(
                "/attributeLabel", // OSC address
                (string address, OscDataHandle data) =>
                {
                    if (data.GetElementAsInt(0) != null && data.GetElementAsString(1) != null)
                    {
                        //labelStrings[data.GetElementAsInt(0)] = data.GetElementAsString(1);
                        attributeLabels[data.GetElementAsInt(0)] = data.GetElementAsString(1);
                    }
                }
            );

        // initialise sliders and buttons
        updateSliders();
        highlightButtons();

        // set UI visibility
        oscManager.SetUI();
    }


    private void BlankList()
    {
        for (int i = 0; i < 20; i++)
        {
            labelStrings.Add("");
        }
    }

    private void Update()
    {
        if (screenMessage.text != messageReceived) screenMessage.text = messageReceived;
        if (smallScreenMessage.text != smallMessageReceived) smallScreenMessage.text = smallMessageReceived;

        if (createUI == true)
        {
            oscManager.SetUI();
            
            createUI = false;
        }

        buttons[3].SetActive(ABbuttonsPresent);
        buttons[4].SetActive(ABbuttonsPresent);
        buttons[5].SetActive(referenceButtonPresent);
        highlightButtons();
    }

    // Takes in OSC data and changes value of the slider
    public void updateSliders()
    {

        for (int i = 0; i < sliders.Count; ++i)
        {
            sliderScale slider = sliders[i].GetComponent<sliderScale>();
            slider.scaledAmount = (sliderValues[i] - slider.userLimitMin) / (slider.userLimitMax - slider.userLimitMin);
        }
        updateSlidersLatch = false;

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

    public void showUI(bool show)
    {
        for (int i = 0; i < sliders.Count; ++i)
        {
            sliders[i].SetActive(show);
        }
    }

    public void SetSliders(int numberOfSliders, int sliderMin, int sliderMax)
    {
        _sliders.SetUI(numberOfSliders);

        // set min and max values here
    }


    public void SetText()
    {
        for (int i = 0; i < labels.Count; i++)
        {
            TextMeshPro labelText = labels[i].GetComponentInChildren<TextMeshPro>();
            labelText.text = labelStrings[i];
        }
    }

}
