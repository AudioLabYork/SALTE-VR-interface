using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySound : MonoBehaviour {

    Object[] myMusic; // declare this as Object array
    public string AudioLocation = "BinauralNoise";
    //private AudioSource audioSource;
    public int triggerRange;
    [HideInInspector]
    public AudioSource audio;
    [Range(0.0f, 1.0f)]
    public float NoiseLength = 1.0f; // playback noise length
    [HideInInspector]
    public int azi_int;
    [HideInInspector]
    public int ele_int;
    [HideInInspector]
    public int dist_int;
    [HideInInspector]
    public string soundType;
    [HideInInspector]
    GameObject CenterEyeAnchor;
    Print tracking;

    [HideInInspector]
    public float azi_anchor = 0.0f;
    [HideInInspector]
    public float ele_anchor = 0.0f;

    private float azi_diff_a = 0.0f;
    private float azi_diff_b = 0.0f;
    [HideInInspector]
    public float azi_diff = 0.0f;

    private float ele_diff_a = 0.0f;
    private float ele_diff_b = 0.0f;
    [HideInInspector]
    public float ele_diff = 0.0f;

    [HideInInspector]
    public float azi_rela;
    [HideInInspector]
    public float ele_rela;

    public float aziRange = 1.0f;
    public float eleRange = 1.0f;

    [HideInInspector]
    public float start_time;
    [HideInInspector]
    public float stop_time;
    [HideInInspector]
    public float played_time = 0.0f;
    [HideInInspector]
    public bool save_state = false;

    [HideInInspector]
    public List<int> randList;
    [HideInInspector]
    public int audioCount = 0;

    [HideInInspector]
    GameObject Pause;
    PauseGame PauseScript;


    void Awake()
    {
        audio = GetComponent<AudioSource>();
        myMusic = Resources.LoadAll(AudioLocation, typeof(AudioClip)); // load audio from Resources folder
        audio.clip = myMusic[0] as AudioClip; // get the first audio clip ready AS A TEST

        // get data from another script ("Print" in "CenterEyeAnchor")
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor"); 
        tracking = CenterEyeAnchor.GetComponent<Print>();

        Pause = GameObject.Find("Pause");
        PauseScript = Pause.GetComponent<PauseGame>();

        // initialise counter
        start_time = 0.0f;
        stop_time = 0.0f;
    }

    
    void Start()
    {
        //print(myMusic[0]);

        randList = Enumerable.Range(0, myMusic.Length).ToList();
        for (int t = 0; t < randList.Count; t++)
        {
            int tmp = randList[t];
            int r = Random.Range(0, randList.Count);
            randList[t] = randList[r];
            randList[r] = tmp;
        }
        print(myMusic.Length);
        print(randList.Count);
        var uniqueItems = randList.Distinct().ToList();
        print(uniqueItems.Count);
    }

    // Update is called once per frame
    void Update()
    {

        // play sound by trigger (if no sound is playing at the moment)
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            print("ele: " + tracking.ele);
            if (!audio.isPlaying && Mathf.Abs(tracking.ele) < triggerRange) // play audio if no sound is playing and head is at the horizontal position
            {
                // set head anchor (start position)
                azi_anchor = tracking.azi;
                ele_anchor = tracking.ele;
                //print(azi_anchor);

                start_time = Time.time;
                playRandomMusic();
                print("playing");
                save_state = true;

            }
        }
        if (PauseScript.Paused == false)
        {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.GetDown(OVRInput.Button.One) || OVRInput.GetDown(OVRInput.Button.Three))
            {
                if (save_state == true)
                {
                    // load random audio clip
                    //audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
                    audio.clip = myMusic[randList[audioCount]] as AudioClip;

                    // convert clip name to string and print
                    string clipName = audio.clip.ToString();
                    string[] parts = clipName.Split('_');
                    // print("play");
                    print(audio.clip);

                    // get audio clip HRTF azi and ele angle, then convert it to int
                    soundType = parts[parts.Length - 4];
                    string azi = parts[parts.Length - 3];
                    string ele = parts[parts.Length - 2];
                    string dist = parts[parts.Length - 1];
                    azi_int = 0;
                    int.TryParse(azi, out azi_int);
                    ele_int = 0;
                    int.TryParse(ele, out ele_int);
                    dist_int = 0;
                    int.TryParse(dist, out dist_int);

                    // print HRTF angles
                    //print("azi: " + azi_int + " | ele: " + ele_int + " | dist: " + dist_int);

                    audioCount = audioCount + 1;
                }
            }

            if (OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Four))
            {
                audioCount = audioCount - 2;
                // load random audio clip
                audio.clip = myMusic[randList[audioCount]] as AudioClip;

                // convert clip name to string and print
                string clipName = audio.clip.ToString();
                string[] parts = clipName.Split('_');
                // print("play");
                print(audio.clip);

                // get audio clip HRTF azi and ele angle, then convert it to int
                soundType = parts[parts.Length - 4];
                string azi = parts[parts.Length - 3];
                string ele = parts[parts.Length - 2];
                string dist = parts[parts.Length - 1];
                azi_int = 0;
                int.TryParse(azi, out azi_int);
                ele_int = 0;
                int.TryParse(ele, out ele_int);
                dist_int = 0;
                int.TryParse(dist, out dist_int);

                audioCount = audioCount + 1;
            }

        }


        // Calculate azimuth angle different (current location vs anchor)
        azi_diff_a = Mathf.Abs((tracking.azi - azi_anchor) % 360);
        azi_diff_b = Mathf.Abs((azi_anchor - tracking.azi) % 360);
        if (azi_diff_a < azi_diff_b)
        {
            azi_diff = azi_diff_a;
        }
        else
        {
            azi_diff = azi_diff_b;
        }
        // Calculate elevation angle different (current location vs anchor)
        ele_diff_a = Mathf.Abs((tracking.ele - ele_anchor) % 360);
        ele_diff_b = Mathf.Abs((ele_anchor - tracking.ele) % 360);
        if (ele_diff_a < ele_diff_b)
        {
            ele_diff = ele_diff_a;
        }
        else
        {
            ele_diff = ele_diff_b;
        }

        // relative angle (compare to the starting point of the play sound)
        azi_rela = (tracking.azi - azi_anchor) % 360;
        ele_rela = (tracking.ele - ele_anchor) % 360;
        //print("azi: " + azi_rela + " | ele: " + ele_rela);
        if (azi_rela >= 180)
        {
            azi_rela = azi_rela - 360;
        }
        else if (azi_rela < -180)
        {
            azi_rela = azi_rela + 360;
        }

        // record played duration
        if (audio.time > 0.0)
        {
            played_time = audio.time;
        }
        // stop playing if user head movement exceed range
        if (azi_diff > aziRange || ele_diff > eleRange)
        {
            audio.Stop();

            //print("played: " + played_time);
        }
    }

    void playRandomMusic()
    {      
        // load random audio clip
    //    audio.clip = myMusic[Random.Range(0, myMusic.Length)] as AudioClip;
        // adjust playback length
        audio.time = audio.clip.length - audio.clip.length * NoiseLength;
        audio.Play();

    }
}
