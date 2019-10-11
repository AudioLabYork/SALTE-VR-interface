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
    public List<GameObject> sliders = new List<GameObject>();

    public Material[] material;

    public int slidersNum;

    public bool createUI;
    public bool clearUI;

    public List<string> labelStrings = new List<string>();
    public List<GameObject> labels = new List<GameObject>();

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


        sliderValues = new float[4];
        buttonStates = new int[5];

        // set text
        messageReceived = "\n\nSpatial Audio Listening Test Environment";

        // hide UI
        visibleUI = true; 
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
                       smallMessageReceived = data.GetElementAsString(0);
                     //  smallMessageReceived = message1;
                      // smallScreenMessage.text = message1;
                       string message2 = data.GetElementAsString(1);
                       messageReceived = message2;
                       screenMessage.text = message2;
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
                        //  
                       }
                       else
                       {
                           createUI = false;
                           clearUI = true;
                           visibleUI = true;
                           updateSlidersLatch = false;
                         //  oscManager.ClearUI();
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
                       else if (oscButton == "reference") buttonStates[5] = state;
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

        // Receives messages to determine the slider type and amount 
        server.MessageDispatcher.AddCallback(
                  "/numOfSliders", // OSC address
                  (string address, OscDataHandle data) =>
                  {
                      if (data.GetElementAsInt(0) != null)
                      {
                          if(data.GetElementAsInt(0) > 0)
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
                     if (data.GetElementAsFloat(0) != null && data.GetElementAsFloat(1) != null && data.GetElementAsFloat(2) != null && data.GetElementAsFloat(3) != null)
                     {

                //       if(data.GetElementAsInt(3) == 3.00f && uiStart) { oscManager.is3G = true; oscManager.isMushra = false; uiStart = false; }
                //       if(data.GetElementAsFloat(3) == 100.0f && uiStart) { oscManager.is3G = false; oscManager.isMushra = true; uiStart = false; }

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
                    if (data.GetElementAsInt(0) != null && data.GetElementAsString(1) != null )
                    {

                        labelStrings[data.GetElementAsInt(0)] = data.GetElementAsString(1);
                    }
                }
            );




        // initialise sliders and buttons
        updateSliders();
        //   highlightButtons();

        // set UI visibility
        //CreateUI();
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

   //     updateSliders();
 //    highlightButtons();
    //  showUI(visibleUI);
        CreateUI();

        UpdateMessage();
        
    }

    private void UpdateMessage()
    {
        screenMessage.text = 
    }

    private void CreateUI() {

     
            if (createUI == true)
            {
                createUI = false;
                Debug.Log("create ui");
          
                oscManager.SetUI();
           
            }

      
    }
   

    // Takes in OSC data and changes value of the slider
    private void updateSliders()
    {
        if (updateSlidersLatch == true)
        {
            for (int i = 0; i < sliders.Count; ++i)
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

    public void showUI(bool show)
    {
        for (int i = 0; i < sliders.Count; ++i)
        {
            sliders[i].SetActive(show);
        }
        /*
        if (test == 0)  // Disables all buttons
        {
            buttons[3].SetActive(show);
            buttons[4].SetActive(show);
            buttons[5].SetActive(show);
        }

        if (test == 1) // only shows buttons for 3G test
        {
            buttons[3].SetActive(show); // A
            buttons[4].SetActive(show); // B
        }

        if (test == 2) // only shows buttons for Mushra
        {
            buttons[5].SetActive(show); // Reference
        }
        */
    }

    public void SetSliders(int numberOfSliders, int sliderMin, int sliderMax)
    {
        _sliders.SetUI(numberOfSliders);

        // set min and max values here
    }


    public void SetText()
    {
      
        for( int i = 0; i < labels.Count; i++)
        {
            TextMeshPro labelText = labels[i].GetComponentInChildren<TextMeshPro>();
            labelText.text = labelStrings[i];
        }
    }

}
