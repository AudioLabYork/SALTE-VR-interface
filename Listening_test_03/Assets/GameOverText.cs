using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// show "GAME OVER" when game is over
public class GameOverText : MonoBehaviour {

    public Text text; // call for Text component in the current game object (make sure to drag to link the text component in the inspector)
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer;  // call DisplayTime script in TimeDisplay

    // Use this for initialization
    void Start () {
        // locate DisplayTime script
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();
    }
	
	// Update is called once per frame
	void Update () {
        // show text when game is over
        if (timer.gameOver == false)
        {
            text.enabled = false;
        }
        else
        {
            text.enabled = true;
        }
    }
}
