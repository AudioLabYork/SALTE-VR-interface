using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// trigger particle effect when user confirm their response
public class TriggerParticle : MonoBehaviour {
    public ParticleSystem particle; // call for ParticalSystem component in the current game object (make sure to drag to link the ParticalSystem component in the inspector)
    public AudioSource sound; // call for AudioSource component in the current game object (make sure to drag to link the AudioSource component in the inspector)

    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;  // call PaseGame script in Pause object
    [HideInInspector]
    GameObject CenterEyeAnchor;
    OSCData player;  // call OSCData script in CenterEyeAnchor
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer; // call DisplayTime script in TimeDisplay

    // Use this for initialization
    void Start () {
        //find PauseGame script
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();
        //find OSCData script
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
        player = CenterEyeAnchor.GetComponent<OSCData>();
        //find DisplayTime script
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();
    }
	
	// Update is called once per frame
	void Update () {

    }
    // start partical effect (the function will be called in OSCData)
    public void EffectStart()
    {
        //print("particals");
        particle.Play(); // play particle effect
        sound.Play(); // play sound
    }
}
