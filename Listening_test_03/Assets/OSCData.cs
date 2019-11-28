using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// send OSC data to Max patch (note: send only, recieve in "Print" script)
public class OSCData : MonoBehaviour {
    // initialise OSC
    public OSC osc;

    [HideInInspector]
    public float azi_anchor = 0.0f; // azimuth anchor, the azimuth angle when the sample start playing
    [HideInInspector]
    public float ele_anchor = 0.0f; // elevation anchor, the elevation angle when the sample start playing
    [HideInInspector]
    public float azi_rela; // realative angle, the "real" azimuth angle - azimuth anchor, 0 when the sample start playing
    [HideInInspector]
    public float ele_rela; // realative angle, the "real" elevation angle - elevation anchor, 0 when the sample start playing
    [HideInInspector]
    public bool save_state = false; // initialise save state (whether it is allow to save)
    public int triggerRange; // trigger range (the vertial tolerance of head pointing position to trigger the sample)
    public float[] TargetAzimuthArray; // list of target azimuth angle (completly random between -180 to 180 when the array size is 0)
    public float[] TargetElevationArray; // list of target azimuth angle (completly random between -60 to 60 when the array size is 0)
    [HideInInspector]
    public float targetAzimuth; // target azimuth angle
    [HideInInspector]
    public float targetElevation; // target elvation angle
    [HideInInspector]
    public float start_time = 0.0f; // initailise start time

    private OscMessage message = new OscMessage(); // initialise OscMessage

    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript; // call PauseGame script in Pause
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer; // call DisplayTime script in Time Display
    [HideInInspector]
    GameObject EnergyExplosion;
    TriggerParticle particleEffect; // call TriggerParticle script in EnergyExplosion
    [HideInInspector]
    GameObject Skip;
    SkipText skipEffect; // call SkipText script in Skip
    [HideInInspector]
    Print tracker; // call Print script
    [HideInInspector]
    GameObject Trigger; 
    TriggerPrint triggerPrint; // call TriggerPrint script in Tirgger
    [HideInInspector]
    GameObject Score;
    ShowScore showScore; // call ShowScore script in Score

    // Use this for initialization
    void Start () {
        // locate PauseGame script
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();
        // locate DisplayTime script
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();
        // locate TriggerPrint script
        Trigger = GameObject.Find("Trigger");
        triggerPrint = Trigger.GetComponent<TriggerPrint>();
        // locate TriggerParticle script
        EnergyExplosion = GameObject.Find("EnergyExplosion");
        particleEffect = EnergyExplosion.GetComponent<TriggerParticle>();
        // locate SkipText script
        Skip = GameObject.Find("Skip");
        skipEffect = Skip.GetComponent<SkipText>();
        // locate ShowScore script
        Score = GameObject.Find("Score");
        showScore = Score.GetComponent<ShowScore>();
        // locate Print script
        tracker = GetComponent<Print>();

        // trigger next sample
        NextSample();

        // (Debug) print azimuth array
        //foreach (float i in TargetAzimuthArray)
        //    print(i);
    }

    // Update is called once per frame
    void Update () {

        // start dac in Max when the game start
        if (timer.gameStarted == true)
        {
            message = new OscMessage();
            message.address = "/StartTrig"; // OSC filter tag
            message.values.Add(1); // OSC message
            osc.Send(message);
        }
        else
        {
            message = new OscMessage();
            message.address = "/StartTrig"; // OSC filter tag
            message.values.Add(0); // OSC message
            osc.Send(message);
        }

        // trigger sound (when main trigger is pressed and the head is in the tolerance range)
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger) && Mathf.Abs(tracker.ele) < triggerRange && save_state == false)
        {
            // set head anchor (start position)
            azi_anchor = tracker.azi;
            ele_anchor = tracker.ele;
            //print(azi_anchor);

            // set start time
            start_time = Time.time;

            // trigger sound (unmute sound)
            message = new OscMessage();
            message.address = "/SoundTrig"; // OSC filter tag
            message.values.Add(0); // OSC message
            osc.Send(message);

            save_state = true;
        }

        // relative angle (compare to the starting point of the play sound)
        azi_rela = (tracker.azi - azi_anchor) % 360;
        ele_rela = (tracker.ele - ele_anchor) % 360;
        // convert form between 0 -360 to between -180 - 180
        if (azi_rela >= 180)
        {
            azi_rela = azi_rela - 360;
        }
        else if (azi_rela < -180)
        {
            azi_rela = azi_rela + 360;
        }
        // (Debug) print head angle after conversion
        //print("azi_rela: " + azi_rela + " | ele_rela: " + ele_rela);

        // send OSC message
        message = new OscMessage();
        message.address = "/Azi"; // OSC filter tag
        message.values.Add(azi_rela); // OSC message
        osc.Send(message);

        message = new OscMessage();
        message.address = "/Ele"; // OSC filter tag
        message.values.Add(ele_rela); // OSC message
        osc.Send(message);

        // if gmae not paused, save state is true (sample have been played and ready to save) and game is not over
        if (PauseScript.Paused == false && save_state == true && timer.gameOver == false) 
        {
            // user comfirm response
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                triggerPrint.saveResponse(); // save response to the trigger CSV file
                particleEffect.EffectStart(); // start particle effect
                showScore.updateSocre(); // update score
                tracker.index_trigger_idx = 1; // change index trigger press indicator

                NextSample(); // append next sample
                save_state = false; // change save state to pause (so user can not save more than 1 time with the same sample)
            }

            // if user is not sure and want to skip the sample
            if (OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
            {
                triggerPrint.saveNan(); // save response to the trigger CSV file
                //skipEffect.EffectStart(); // start skip effect
                showScore.updateSocre(); // update score
                tracker.nan_trigger_idx = 1; // change nan trigger press indicator

                NextSample(); // append next sample
                save_state = false; // change save state to pause (so user can not save more than 1 time with the same sample)
            }
        }
        else
        {
            tracker.index_trigger_idx = 0; // change index trigger press indicator back to defult
            tracker.nan_trigger_idx = 0; // change nan trigger press indicator back to defult
        }

        // when user tries to pause (by pressing the start button)
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            NextSample(); // append next sample (so the user can not listening to the same sample more than 1 time)
            save_state = false;
        }

        // when game is over, mute the sound
        if (timer.gameOver == true)
        {
            message = new OscMessage(); 
            message.address = "/SoundTrig"; // OSC filter tag
            message.values.Add(1); // OSC message
            osc.Send(message);
            save_state = false;
        }
    }

    // mute sound and turn off dac when quit gmae 
    private void OnApplicationQuit()
    {
        // mute sound
        message = new OscMessage();
        message.address = "/SoundTrig"; // OSC filter tag
        message.values.Add(1); // OSC message
        osc.Send(message);
        // turn off dac
        message = new OscMessage();
        message.address = "/StartTrig"; // OSc filter tag
        message.values.Add(0); // OSc message
        osc.Send(message);
    }

    // append next sample 
    void NextSample()
    {
        // stop sound (mute sound)
        message = new OscMessage();
        message.address = "/SoundTrig"; // OSC filter tag
        message.values.Add(1); // OSC message
        osc.Send(message);

        // get target azimuth angle
        if (TargetAzimuthArray.Length == 0) // if the array size is 0
        {
            targetAzimuth = Random.Range(-180, 180); // random angle between -180 and 180
        }
        else
        {
            // random select angle in the array
            int idx = Random.Range(1, TargetAzimuthArray.Length); // random select a index within the size of the array
            targetAzimuth = TargetAzimuthArray[idx]; // get the angle by the random index
        }

        // get target elevation angle
        if (TargetElevationArray.Length == 0) // if the array size is 0
        {
            targetElevation = Random.Range(-60, 60); // random angle between -60 and 60
        }
        else
        {
            // random select angle in the array
            int idx = Random.Range(1, TargetElevationArray.Length); // random select a index within the size of the array
            targetElevation = TargetElevationArray[idx]; // get the angle by the random index
        }
        print("target azi: " + targetAzimuth + " | target ele: " + targetElevation); // print target azimuth and elevation angle

        // send target azimuth angle
        message = new OscMessage();
        message.address = "/TarAzi"; // OSC filter tag
        message.values.Add(targetAzimuth); //OSC message
        osc.Send(message);
        // send target elevation angle
        message = new OscMessage();
        message.address = "/TarEle"; // OSC filter tag
        message.values.Add(targetElevation); //OSC message
        osc.Send(message);
        // trigger new HRTF
        message = new OscMessage();
        message.address = "/BangHRTF"; // OSC filter tag
        message.values.Add(1); //OSC message
        osc.Send(message);
    }

}
