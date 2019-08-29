using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistenManagerScript : MonoBehaviour
{

    public static PersistenManagerScript Instance { get; private set;}  // create singleton script 


    public string participantName;
    public string participantGender;
    public int participantAge;

    string[] gender;

    public string sessionID = "Test_Session_ID";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

      gender  = new string[] { "Male", "Female", "Non-binary" };
    }

    

    public void nameUpdate(string name)
    {
        participantName = name;
    }

    public void ageUpdate(string age)
    {
        participantAge = int.Parse(age);
    }

    public void genderUpdate(int index)
    {

        participantGender = gender[index];

    }
}
