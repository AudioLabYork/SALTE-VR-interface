using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using OscJack;

// Change distance of the red dot in the display
public class DistanceControl : MonoBehaviour {
    public Vector2 joystick; // controller joystick
    //public float trigger;
    public float speed;  // move speed
    public Canvas canvas; // get canvas object
    public float initDistance; // initial distance
    //public float delay;
    [HideInInspector]
    public float dist; // distance

	public string IPAddress = "127.0.0.1"; // IP address for OSC 
	public int oscPortOut = 9001; // Port for OSC out
	OscClient client;

    // Use this for initialization
    void Start () 
	{
		// Setup the osc clients 
		client = new OscClient(IPAddress, oscPortOut);

        speed = 8; // initialise speed
        initDistance = 2.5f; // initialise distance
        canvas.planeDistance = initDistance; // set distance
    }
	
	// Update is called once per frame
	void Update () 
	{
        joystick = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick); // get oculus rift joystick

        canvas.planeDistance += joystick.y * speed * Time.deltaTime;
        //canvas.planeDistance += joystick.y * ((float)Math.Pow (speed, Time.deltaTime));

        if (canvas.planeDistance < 0.12) // stop if too close
        {
            canvas.planeDistance = 0.12f;
        }
        else if (canvas.planeDistance > 5.0) // stop if too far
        {
            canvas.planeDistance = 5.0f;
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick)) // reset distance
        {
            canvas.planeDistance = initDistance;
        }

        dist = canvas.planeDistance; // distance output
		client.Send ("/lochead/test", dist);
    }
}
