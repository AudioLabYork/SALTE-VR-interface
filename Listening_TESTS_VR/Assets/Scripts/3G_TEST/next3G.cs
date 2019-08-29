using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class next3G : MonoBehaviour
{
    [SerializeField]
    GameObject[] sliders;

    WriteTestCSV dataManager;
    Audio_File_Manager audioFileManager;

    // OSC Variables
    public string IPAddress = "127.0.0.1"; // IP address for OSC 
    public int oscPortOut = 9000; // Port for OSC
    OscClient client;
    public string buttonAddress = null;

    private void Start()
    {
        sliders = GameObject.FindGameObjectsWithTag("Slider_Cont3G");
        dataManager = GameObject.Find("DataManager_3G").GetComponent<WriteTestCSV>();
        audioFileManager = GameObject.Find("DataManager_3G").GetComponent<Audio_File_Manager>();


        buttonAddress  = "/player/loadstimulus";
    }


    public void nextStimulus()
    {
       foreach(GameObject slider in sliders)
        {
            // reset sldiers to zero
            var reset = slider.GetComponent<sliderScale>();
            reset.resetScale();

        }

        dataManager.currentStimulus++;

        storeData();
    }

   private void loadStimulus()
    {
        string msg = audioFileManager.audioFilePaths[dataManager.currentStimulus];

        // send data to renderer to load next file
        client.Send(buttonAddress, msg);

    }


    private void storeData()
    {
        // store data here 
        dataManager.addRecord();
        
    }

}
