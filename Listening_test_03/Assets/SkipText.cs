using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OscJack;

// show "skip" text when user decided to skip the sample
public class SkipText : MonoBehaviour {

    public float TextDuration = 2.0f; // set how long the text will be displayed
    public AudioSource sound; // call for AudioSource component in the current game object (make sure to drag to link the AudioSource component in the inspector)
    public Text text; // call for Text component in the current game object (make sure to drag to link the text component in the inspector)

	public string IPAddress = "127.0.0.1"; // IP address for OSC 
	public int oscPortOut = 9001; // Port for OSC out
	OscClient client;

    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;  // call PaseGame script in Pause object
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer; // call DisplayTime script in TimeDisplay

    // Use this for initialization
    void Start () 
	{
		// Setup the osc clients 
		client = new OscClient(IPAddress, oscPortOut);

        //find PauseGame script
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();
        // find DisplayTime script
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();

        // disable text
        text.enabled = false;
    }

	void Update()
	{
		if (PauseScript.Paused == false && timer.gameStarted == true) 
		{
			if (OVRInput.GetDown (OVRInput.Button.One) || OVRInput.GetDown (OVRInput.Button.Three)) 
			{
				sound.Play (); // play sound
				StartCoroutine (ShowMessage ()); // trigger ShowMessage()
				client.Send ("/lochead/test", "skip");
			}
		}
	}

    // show message and disable it after certain amount of time (TextDuration)
    IEnumerator ShowMessage()
    {
        text.enabled = true; // show text
        yield return new WaitForSeconds(TextDuration); // wait for certain amount of time (TextDuration)
        text.enabled = false; // disable text
    }

}
