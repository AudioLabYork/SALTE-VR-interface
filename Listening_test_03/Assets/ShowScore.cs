using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// show the socre to the user
public class ShowScore : MonoBehaviour {

    public Text text; // call for Text component in the current game object (make sure to drag to link the text component in the inspector)
    public float score = 0.0f;  // initiallise score (and show it in the inspector

    [HideInInspector]
    GameObject Trigger;
    TriggerPrint triggerPrint; // locate triggerPrint in Trigger

    // Use this for initialization
    void Start () {

        score = 0.0f; // initialise score
        text.text = "SCORE: " + score; // initialise text

        // locate triggerPrint component in Trigger object
        Trigger = GameObject.Find("Trigger");
        triggerPrint = Trigger.GetComponent<TriggerPrint>();
    }
	
	// Update is called once per frame
	void Update () {

        text.text = "SCORE: " + score; // update text
    }

    public void updateSocre()
    {
        score += Mathf.Ceil(triggerPrint.overall_score); // update score (the function will be called in OSCData)
    }
}
