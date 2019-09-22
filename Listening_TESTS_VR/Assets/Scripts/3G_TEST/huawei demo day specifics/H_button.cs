using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class H_button : MonoBehaviour
{
    // OSC Variables
    public string IPAddress = "127.0.0.1"; // IP address for OSC 
    public int oscPortOut = 9000; // Port for OSC
    OscClient client;

    string buttonAddress = "/button";

    public string msg;

    private void Start()
    {
        client = new OscClient(IPAddress, oscPortOut);
    }

    public void sendData()
    {
        client.Send(buttonAddress, msg);
    }
}
