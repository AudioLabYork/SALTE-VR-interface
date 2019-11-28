using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using System.IO;
using UnityEditor;

public class PrintHeadTrack : MonoBehaviour {

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
    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;  // calling PaseGame script in Pause object
    [HideInInspector]
    GameObject Trigger;
    TriggerPrint TriggerPrint;  // calling PaseGame script in Pause object
    public string fileName;
    [HideInInspector]
    GameObject TimeDisplay;
    DisplayTime timer; // calling DisplayTime script in TimeDisplay
    private bool print_gameover = false;
    private string backup_path = "C:/Users/Benjamin/Google Drive/Listening_Test_Results";

    // Use this for initialization
    void Start () {
        // locate PauseGame script in Pause
        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();
        // locate TriggerPrint script in Trigger
        Trigger = GameObject.Find("Trigger");
        TriggerPrint = Trigger.GetComponent<TriggerPrint>();
        // locate DisplayTime script in TimeDisplay
        TimeDisplay = GameObject.Find("TimeDisplay");
        timer = TimeDisplay.GetComponent<DisplayTime>();

        // set file name
        fileName = TriggerPrint.subjectName + "_headtrack.csv";
        // set backup path
        backup_path = backup_path + "/" + fileName;

        //initialise file
        CreateTest();
        //start writing tracking data to the file
        StartCoroutine(PrintData());

    }

    // Update is called once per frame
    void Update()
    {
        // ray cast
        Vector3 forward = transform.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(transform.position, forward, Color.green);

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
                "Game," + "Over," + "\n");
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
            File.WriteAllText(path, "Login log \n\n");
        }
        //set date and headers strings
        string content = "\n- Login date: " + System.DateTime.Now + "\n";
        string header = "count," + "time," + "azi," + "ele," + "euler_z," + "rotate_x," + "rotate_y," + "rotate_z," + "posit_x," + "posit_y," + "posit_z" + "\n";

        //Add the strings into the file
        File.AppendAllText(path, content);
        File.AppendAllText(path, header);
    }

    IEnumerator PrintData()
    {
        // locate file
        string path = Application.dataPath + "/" + fileName;

        while (true)
        {
            yield return new WaitForSeconds(printFreq);

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

            //print("azi: " + azi + " | ele: " + ele);
            // print out euler's azi (x) and ele angle (y)

            //euler's angle z
            euler_z = transform.eulerAngles.z;

            // rotation angles
            float rotate_x = transform.rotation.x;
            float rotate_y = transform.rotation.y;
            float rotate_z = transform.rotation.z;

            // position values
            float posit_x = transform.position.x;
            float posit_y = transform.position.y;
            float posit_z = transform.position.z;

            if (PauseScript.Paused == false && timer.gameOver == false) // if the game is not paused and not over
            {
                // write data to the end of the file
                File.AppendAllText(path, counter + "," + System.DateTime.Now.TimeOfDay + "," + azi + "," + ele + "," + euler_z + "," + rotate_x +
                    "," + rotate_y + "," + rotate_z + "," + posit_x + "," + posit_y + "," + posit_z + "\n");
            }

            //timeCount = 0.0f;
            counter += 1;  // sample count
            //timeCount = timeCount + Time.deltaTime;
        }
        
    }
}
