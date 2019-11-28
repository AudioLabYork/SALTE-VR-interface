using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using System.IO;
using UnityEngine.Events;
using OscJack;
using UnityEditor;

// Write user's tracking data to a csv file
public class Print : MonoBehaviour {
    public string fileName; // file name
    public float range = 50f; // ray length
    public float printFreq = 0.001f; // how frequent the data will be printed
    private Camera fpsCam; // not in use for VR
    private float counter = 1.0f; // counting print sequence
    [HideInInspector]
    public float azi = 0.0f;  // initialise azimuth angle
    [HideInInspector]
    public float ele = 0.0f;  // initialise elevation angle
    [HideInInspector]
    public float euler_z = 0.0f;  // initialise euler z distnace
    public bool printData;  // print data boolean 
    [HideInInspector]
    public int index_trigger_idx = 0; // index trigger press indicator
    [HideInInspector]
    public int sound_trigger_idx = 0; // sound trigger press indicator
    [HideInInspector]
    public int nan_trigger_idx = 0; // nan trigger press indicator
    public string SOFAName;
    private bool print_gameover = false;
    private string backup_path = "C:/Users/Benjamin/Google Drive/Listening_Test_Results";

    OSCData oscData;
    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;  // call PauseGame script in Pause object
    [HideInInspector]
    GameObject Trigger;
    TriggerPrint TriggerPrint;  // call PaseGame script in Pause object
    [HideInInspector]
    GameObject Canvas;
    DistanceControl dist_resp; // call dist_resp script in Canvas
    [HideInInspector]
    GameObject CenterEyeAnchor;
    OSCData player;  // call the player script in CenterEyeAnchor
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer; //// call DisplayTime script in TimeDisplay
    [HideInInspector]
    GameObject SoundSource; // call the Score script
    LocateSoundSource locateSoundSource;


    void CreateTest()
    {
        //Path of the file
        string path = Application.dataPath + "/" + fileName;

        //Create File if it doesn't exist
        if (!File.Exists(path))
        {
            //write file
            File.WriteAllText(path, "Login log \n\n");
        }
        //set date and headers strings
        string content = "\n- Login date: " + System.DateTime.Now + "\n";
        string header = "count," + "time," + "rela.azi," + "rela.ele," + "dist," + "HRTF," + "t.azi," + "t.ele," + "playing," + 
            "sound.trigger," + "index.trigger," + "nan.trigger," + "start.azi," + "start.ele," + "real.azi," + "real.ele," + "real.z," + 
            "real.tar.azi," + "real.tar.ele," + "sound.type" + "\n";

        //Add the strings into the file
        File.AppendAllText(path, content);
        File.AppendAllText(path, header);
    }

        
	// Use this for initialization
	void Start () {
        //locate PauseGame script
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();
        //locate TriggerPrint script
        Trigger = GameObject.Find("Trigger");
        TriggerPrint = Trigger.GetComponent<TriggerPrint>();
        // locate DistanceControl script
        Canvas = GameObject.Find("Canvas");
        dist_resp = Canvas.GetComponent<DistanceControl>();
        // locate DisplayTime script
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();
        // locate LocateSoundSource script in SoundSource
        SoundSource = GameObject.Find("SoundSource");
        locateSoundSource = SoundSource.GetComponent<LocateSoundSource>();
        // locate OSCData script
        player = GetComponent<OSCData>();

        // get file name
        fileName = TriggerPrint.subjectName + "_main.csv";
        // set backup path
        backup_path = backup_path + "/" + fileName;

        //initialise file
        CreateTest();
        //start writing tracking data to the file
        StartCoroutine(PrintData());

        //initialise OSC server (recieve only)
        var server = new OscServer(9001); // Port number
        // start OSC server, and filer OSC recieve data
        server.MessageDispatcher.AddCallback(
            "/SOFAName", // OSC address
            (string address, OscDataHandle data) => {
                SOFAName = string.Format(data.GetElementAsString(0));
                print(string.Format(data.GetElementAsString(0)));
            }
        );

    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Start))  // if start button is pressed (user tries to pause or un-pause the game)
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
                "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "Game," + "Over," + "\n");
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

    // write data to the main csv script
    IEnumerator PrintData()
    {
        // locate file
        string path = Application.dataPath + "/" + fileName;

        // ray cast
        Vector3 forward = transform.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(transform.position, forward, Color.green);

        while (true)
        {
            yield return new WaitForSeconds(printFreq);  // write data with certain time gap

            // euler's angle x
            if (transform.eulerAngles.x <= 90)
            {
                ele = transform.eulerAngles.x * -1;
            }
            else
            {
                ele = (transform.eulerAngles.x - 360) * -1;
            }

            // euler's angle y
            azi = transform.eulerAngles.y;

            //euler's angle z
            euler_z = transform.eulerAngles.z;

            // main trigger press indicator
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) // Trigger print
            {
                sound_trigger_idx = 1;
            }
            else
            {
                sound_trigger_idx = 0;
            }

            if (PauseScript.Paused == false && timer.gameOver == false) // if the game is not paused
            {
                // write data to the end of the file
                File.AppendAllText(path, counter + "," + System.DateTime.Now.TimeOfDay + "," + player.azi_rela + "," + player.ele_rela + "," + 
                    dist_resp.dist + "," + SOFAName + "," + player.targetAzimuth + "," + player.targetElevation + "," + player.save_state +  "," 
                    + sound_trigger_idx + "," + index_trigger_idx + "," + nan_trigger_idx + "," + player.azi_anchor + "," + player.ele_anchor + "," + azi + "," + ele +  "," + 
                    euler_z + "," + locateSoundSource.azi_rela + "," + locateSoundSource.ele_rela + "," + "Pink Noise" + "," + "\n");
            }

            //timeCount = 0.0f;
            counter += 1;  // sample count
            //timeCount = timeCount + Time.deltaTime;
        }
        
    }

}
