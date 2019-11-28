using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// show the "PAUSE" text to the user
public class PauseText : MonoBehaviour {

    public Text text;  // call for Text component in the current game object (make sure to drag to link the text component in the inspector)
    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript; // call PauseGame script in Pause object

    // Use this for initialization
    void Start()
    {
        // locate PauseGame script
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();

        // disable text by defult
        text.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        // show text when game is paused
        if (PauseScript.Paused == false)
        {
            text.enabled = false;
        }
        else
        {
            text.enabled = true;
        }
    }

}
