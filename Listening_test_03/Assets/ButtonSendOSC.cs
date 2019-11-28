using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using OscJack;

public class ButtonSendOSC : MonoBehaviour
{
	public string IPAddress = "127.0.0.1"; // IP address for OSC 
	public int oscPortOut = 9001; // Port for OSC out
	public int oscPortIn = 9000; // Port for OSC in
	OscClient client;
	OscServer server;

	private string buttonAddress = "/lochead/trigger";

	//private bool isPlaying = false;
	private string playerState = "stopped";

	public float triggerRange = 0.0f; // trigger range (the vertial tolerance of head pointing position to trigger the sample)

	public AudioClip hapticAudioClip; // audio clip for haptic

	[HideInInspector]
	GameObject Pause;
	PauseGame pauseGame; // call PauseGame script in Pause
	[HideInInspector]
	GameObject TimeDisplay;
	DisplayTime timer; // call DisplayTime script in Time Display
	[HideInInspector]
	GameObject EnergyExplosion;
	TriggerParticle particleEffect; // call TriggerParticle script in EnergyExplosion

	private void Start()
	{
		// Setup the osc clients 
		client = new OscClient(IPAddress, oscPortOut);
		server = new OscServer (oscPortIn);

		Pause = GameObject.Find("Pause");
		pauseGame = Pause.GetComponent<PauseGame>();
		// locate DisplayTime script
		TimeDisplay = GameObject.Find("TimeDisplay");
		timer = TimeDisplay.GetComponent<DisplayTime>();
		EnergyExplosion = GameObject.Find("EnergyExplosion");
		particleEffect = EnergyExplosion.GetComponent<TriggerParticle>();

		// start OSC server, and filer OSC recieve data
		server.MessageDispatcher.AddCallback(
			"/player/state", // OSC address
			(string address, OscDataHandle data) => {
				playerState = string.Format(data.GetElementAsString(0));
				print(string.Format(data.GetElementAsString(0)));
			}
		);
	}

	private void Update()
	{
		//print (pauseGame.Paused);
		//print(timer.gameOver);
		if (pauseGame.Paused == false && timer.gameOver == false) 
		{
			if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown (OVRInput.Button.SecondaryIndexTrigger)) 
			{
				if (playerState == "playing") 
				{
					// haptics feedback
					OVRHapticsClip hapticsClip = new OVRHapticsClip (hapticAudioClip);
					OVRHaptics.LeftChannel.Preempt (hapticsClip);
					particleEffect.EffectStart (); 

					client.Send (buttonAddress, "stop");
				} else if (playerState == "stopped") 
				{
					if (triggerRange == 0) {
						client.Send (buttonAddress, "play");
					} else {
						if (Mathf.Abs (convertDegree (transform.localEulerAngles.x) * -1) < triggerRange) 
						{
							client.Send (buttonAddress, "play");
						}
					}
				}
			}
		}
	}

	private float convertDegree(float deg)
	{
		float angle = deg;

		if (deg > 180)
			angle -= 360;

		return angle;
	}

}

