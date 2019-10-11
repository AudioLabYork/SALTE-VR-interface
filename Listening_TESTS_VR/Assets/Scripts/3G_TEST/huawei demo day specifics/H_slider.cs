using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class H_slider : MonoBehaviour
{
    // OSC Variables
    public string IPAddress = "127.0.0.1"; // IP address for OSC 
    public int oscPortOut = 9000; // Port for OSC
    OscClient client;

    string buttonAddress = "/slider";
    string msg;
    public int slider_index;
    public float value;

    private void Start()
    {
        client = new OscClient(IPAddress, oscPortOut);
    }

    public void updateValue()
    {
        DataSave_Slider data = GetComponent<DataSave_Slider>();
        value = data.getValue();

    }



    public void sendData()
    {
        client.Send(buttonAddress, slider_index, value);
    }
}
