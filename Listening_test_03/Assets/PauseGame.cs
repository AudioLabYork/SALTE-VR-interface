using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

// Pause game
public class PauseGame : MonoBehaviour {

    //[SerializeField] private GameObject pausePanel;
    private float TimeScaleRef = 1f; 
    private float VolumeRef = 1f;
    [HideInInspector]
    public bool Paused = false; // pause state
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer;

	public string IPAddress = "127.0.0.1"; // IP address for OSC 
	public int oscPortOut = 9001; // Port for OSC out
	OscClient client;

    void Start()
    {
		// Setup the osc clients 
		client = new OscClient(IPAddress, oscPortOut);

        //pausePanel.SetActive(false);
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();
    }
		
    private void Pause()
    {
        // save the orginal time scale
        TimeScaleRef = Time.timeScale;
        // change the time scale to 0 
        Time.timeScale = 0f;
        // save the orginal volume
        VolumeRef = AudioListener.volume;
        // change the volume to 0
        AudioListener.volume = 0f;
        // cahnge pause state to true
        Paused = true;
		client.Send ("/lochead/test", "pause");

    }
    private void Continue()
    {
        // change back the time scale to the value before the pause
        Time.timeScale = TimeScaleRef;
        // change back the volume to the value before the pause
        AudioListener.volume = VolumeRef;
        // cahnge pause state to false
        Paused = false;
		client.Send ("/lochead/test", "resume");

    }
		
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start) && timer.gameOver == false)
        {
            if (Paused == false) // if the game is not paused
            {
                Pause();
            }
            else
            {
                Continue();
            }
        }
    }

}

