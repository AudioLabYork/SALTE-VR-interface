using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

// Show warning if user's head is not pointing at the horizon and try to play sound
public class ShowWarning : MonoBehaviour {
    public Text text;
    [HideInInspector]
    GameObject CenterEyeAnchor;
	ButtonSendOSC ButtonSendOSC;
    public float TextDuration = 2.0f;
    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;

    // Use this for initialization
    void Start () {
        // get data from another script ("Print" in "CenterEyeAnchor")
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
		ButtonSendOSC = CenterEyeAnchor.GetComponent<ButtonSendOSC>();
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();

    }
    void Awake()
    {
        StartCoroutine(ShowMessage());
    }
	
	// Update is called once per frame
	void Update () {
        if (PauseScript.Paused == false)
        {
			if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && Mathf.Abs(convertDegree (transform.localEulerAngles.x) * -1) > ButtonSendOSC.triggerRange)
            {
                StartCoroutine(ShowMessage());
            }
			if (Mathf.Abs(convertDegree (transform.localEulerAngles.x) * -1) < ButtonSendOSC.triggerRange)
            {
                text.enabled = false;
            }
        }
        else
        {
            text.enabled = false;
        }
    }

    IEnumerator ShowMessage()
    {
        text.enabled = true;
        yield return new WaitForSeconds(TextDuration);
        text.enabled = false;
    }

	private float convertDegree(float deg)
	{
		float angle = deg;

		if (deg > 180)
			angle -= 360;

		return angle;
	}
}
