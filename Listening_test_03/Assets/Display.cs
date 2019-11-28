using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// Show sample count in the display
public class Display : MonoBehaviour {
    [HideInInspector]
    GameObject Trigger;
    PlaySound player; // get player script in Trigger object

    public Text text; // get text component

    // Use this for initialization
    void Start () {
        Trigger = GameObject.Find("Trigger");
        player = Trigger.GetComponent<PlaySound>(); // locate PlaySound script

        count(); // start count function
    }
	
	// Update is called once per frame
	void Update () {
        count(); // keep counting
    }

    void count()
    {
        string content = player.audioCount.ToString() + "/" + player.randList.Count.ToString();  // set text (played samples/ total samples)
        text.text = content;  // update text
    }
}
