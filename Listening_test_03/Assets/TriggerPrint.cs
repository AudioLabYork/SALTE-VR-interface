using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

// Write user's responses to a CSV file by oculus rift left hand trigger
public class TriggerPrint : MonoBehaviour {

    private float counter = 1.0f; // count samples
    public string subjectName; // initialise file name
    public string fileName; // initialise file name
    private float resp_time = 0.0f; // initiallise response time
    public float azi_score = 0.0f; //initiallise azimuth score
    public float ele_score = 0.0f; //initiallise elevation score
    public float overall_score = 0.0f; //initiallise overall score
    private bool print_gameover = false;
    private string backup_path = "C:/Users/Benjamin/Google Drive/Listening_Test_Results";

    [HideInInspector]
    GameObject Canvas; 
    DistanceControl dist_resp; // call the dist_resp script in Canvas
    [HideInInspector]
    GameObject CenterEyeAnchor;
    Print tracking; // call Print script in CenterEyeAnchor
    OSCData player;  // call the player script
    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;  // call PaseGame script in Pause object
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer;  // call the DisplayTime script in TimeDisplay
    [HideInInspector]
    GameObject Score; // call the Score script
    ShowScore showScore;
    [HideInInspector]
    GameObject SoundSource; // call the Score script
    LocateSoundSource locateSoundSource;

    public AudioClip hapticAudioClip; // audio clip for haptic

    // Use this for initialization
    void Start()
    {
        // initialise file name
        fileName = subjectName + "_trigger.csv";
        // set backup path
        backup_path = backup_path + "/" + fileName;

        // locate DistanceControl script in Cnavas
        Canvas = GameObject.Find("Canvas");
        dist_resp = Canvas.GetComponent<DistanceControl>();
        // locate OSCData and Print script in CenterEyeAnchor
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
        tracking = CenterEyeAnchor.GetComponent<Print>();
        player = CenterEyeAnchor.GetComponent<OSCData>();
        // locate DisplayTime script in TimeDisplay
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();
        // locate ShowScore script in Score
        Score = GameObject.Find("Score");
        showScore = Score.GetComponent<ShowScore>();
        // locate LocateSoundSource script in SoundSource
        SoundSource = GameObject.Find("SoundSource");
        locateSoundSource = SoundSource.GetComponent<LocateSoundSource>();
        // locate PauseGame script in Pause
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();

        // initalise file
        CreateTest();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Start))   // if start button is pressed (user tries to pause or un-pause the game)
        {
            if (PauseScript.Paused == false)
            {
                // write "continue" when user un-pause the game
                string path = Application.dataPath + "/" + fileName;
                File.AppendAllText(path, "Continue," + System.DateTime.Now.TimeOfDay + "," + "\n");
            }
            else
            {
                // write "paused" when user paused the game
                string path = Application.dataPath + "/" + fileName;
                File.AppendAllText(path, "Paused," + System.DateTime.Now.TimeOfDay + "," + "\n");
            }
        }
        // write the final score to the csv file when the game is over
        if (timer.gameOver == true && print_gameover == false)
        {
            // write the final score
            string path = Application.dataPath + "/" + fileName;
            File.AppendAllText(path, "Game Over," + System.DateTime.Now.TimeOfDay + "," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + 
                "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Overall," + "Score," + 
                showScore.score + "," + "\n");
            try
            {
                FileUtil.CopyFileOrDirectory(path, backup_path);
            }
            catch (IOException)
            {
                FileUtil.DeleteFileOrDirectory(backup_path);
                FileUtil.CopyFileOrDirectory(path, backup_path);
            }
            print_gameover = true;
        }
    }

    void CreateTest()
    {
        //Path of the file
        string path = Application.dataPath + "/" + fileName;

        //Create File if it doesn't exist
        if (!File.Exists(path))
        {
            //write file
            File.WriteAllText(path, "Trigger log \n\n");
        }
        //set date and headers strings
        string content = "\n- Login date: " + System.DateTime.Now + "\n";
        string header = "count," + "time," + "rela.azi," + "rela.ele," + "resp.dist," + "HRTF.file," + "tar.azi," + "tar.ele," + "resp.time," + "time.passed,"
            + "sound.type," + "start.azi," + "start.ele," + "real.azi," + "real.ele," + "real.z," + "real.tar.azi," + "real.tar.ele," + "azi.err," + "ele.err," + 
            "azi.score(max100)," + "ele.score(max80)," + "OA.score(max128)," + "\n";

        //Add the strings into the file
        File.AppendAllText(path, content);
        File.AppendAllText(path, header);
    }

    // write user's response to the csv file (called in OSCData)
    public void saveResponse()
    {
        resp_time = Time.time - player.start_time; // response time

        string path = Application.dataPath + "/" + fileName; // csv file location 

        float azi_err = player.azi_rela - player.targetAzimuth; // azimuth error
        // change azmith error angle from 0 - 360 to -180 to 180
        if (azi_err > 180)
        {
            azi_err = 360 - azi_err;
        }
        else if (azi_err < -180)
        {
            azi_err = 360 + azi_err;
        }
        float ele_err = player.ele_rela - player.targetElevation; // elevation error

        // calculate azimuth score
        azi_score = 100 - (Mathf.Abs(azi_err) * 3); // weighting equation
        // negitive number as 0
        if (azi_score <= 0)
        {
            azi_score = 0;
        }
        // calculate elevation score
        ele_score = 80 - ((Mathf.Abs(ele_err) / 2)); // weighting equation
        // negitive number as 0
        if (ele_score <= 0)
        {
            ele_score = 0;
        }
        // calcute overall score with educian distance
        overall_score = Mathf.Sqrt((azi_score * azi_score) + (ele_score * ele_score));

        if (player.save_state == true) // if save state is true (audio have been played)
        {
            // write reponse data to file
            File.AppendAllText(path, counter + "," + System.DateTime.Now.TimeOfDay + "," +
                player.azi_rela + "," + player.ele_rela + "," + dist_resp.dist + "," + tracking.SOFAName + "," +
                player.targetAzimuth + "," + player.targetElevation + "," + resp_time + "," + Time.time + "," +
                "Pink Noise" + "," + player.azi_anchor + "," + player.ele_anchor + "," + tracking.azi + "," + tracking.ele + "," + tracking.euler_z + "," +
                locateSoundSource.azi_rela + "," + locateSoundSource.ele_rela + "," + azi_err + ", " + ele_err + "," + azi_score + "," + ele_score + "," + 
                overall_score + "," + "\n");

            // haptics feedback
            OVRHapticsClip hapticsClip = new OVRHapticsClip(hapticAudioClip);
            OVRHaptics.LeftChannel.Preempt(hapticsClip);
            
            // print score
            print("score: " + overall_score);
            print("total score: " + showScore.score);
        }

        counter += 1; // sample count
    }

    // write Nan respone to the csv file (called in OSCData)
    public void saveNan()
    {
        resp_time = Time.time - player.start_time; // response time

        string path = Application.dataPath + "/" + fileName; // locate file

        // set all scores to 0
        azi_score = 0;
        ele_score = 0;
        overall_score = 0;

        if (player.save_state == true) // if save state is true (audio have been played)
        {
            // write reponse data to file
            File.AppendAllText(path, counter + "," + System.DateTime.Now.TimeOfDay + "," +
                "nan" + "," + "nan" + "," + "nan" + "," + tracking.SOFAName + "," +
                player.targetAzimuth + "," + player.targetElevation + "," + resp_time + "," + Time.time + "," +
                "Pink Noise" + "," + player.azi_anchor + "," + player.ele_anchor + "," + tracking.azi + "," + tracking.ele + "," + tracking.euler_z + "," +
                locateSoundSource.azi_rela + "," + locateSoundSource.ele_rela + "," + "nan" + ", " + "nan" + "," + azi_score + "," + ele_score + "," + 
                overall_score + "," + "\n");

            // print score
            print("score: " + overall_score);
            print("total score: " + showScore.score);
        }

        counter += 1; // sample count
    }
}
