using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;
using TMPro;
    
public class ButtonSendOSC : MonoBehaviour
{
    public string IPAddress = "127.0.0.1"; // IP address for OSC 
    public int oscPortOut = 9000; // Port for OSC

    OscClient client;

    public string buttonAddress = null;

    public TextMeshPro text;

    private void Start()
    {
        // Setup the osc client 
        client = new OscClient(IPAddress, oscPortOut);

        // Setup the address for OSC
        if(gameObject.tag  == "Play" || gameObject.tag == "Stop" || gameObject.tag == "Loop_3G")
        {
            buttonAddress = "/player/transport";
        }else
        buttonAddress = "/player/" + gameObject.name;

        // Setup the tag for object
  

        // Set the gameObject layer as interactable
        this.gameObject.layer = 10;



    }










    public void sendData(string msg )
    {
        // This method is used to send OSC data to the SALTE audio renderer

        Debug.Log(buttonAddress + "/" + msg);

        client.Send(buttonAddress, msg);

    }
}
