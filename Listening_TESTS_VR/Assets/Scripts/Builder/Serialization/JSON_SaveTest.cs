using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using TMPro;

public class JSON_SaveTest : MonoBehaviour
{
    #region Singleton
    private static JSON_SaveTest instance;

    //  Creat Singleton
    public static JSON_SaveTest Instance
    {

        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<JSON_SaveTest>();
            }
            return instance;
        }

    }
    #endregion


         
    public string filename = "testNEW.json";
    public string path;
    public string fullPath;


    // UI
    Test_Object test_Object = new Test_Object();
    Saved_Objects saveList = new Saved_Objects();


    // Test Settings
    GlobalTestSettings globalSettings = new GlobalTestSettings();
    TrialSettings trial = new TrialSettings();
    StimuliSettings stim = new StimuliSettings();

    public int numberOfTrials;
    public int globalAmbOrder = 1;
    public string trialType;
    public int trialNumber;
    public int ambOrder = 1;
    public string reference;
    public int numberOfStimuli;
    public string ambixconfig;
    public List<StimuliSettings> teststimuli = new List<StimuliSettings>();


    // stimuli settingsa 
    public string source;
    public string ambixconfigS;
    public int order;



    public List<GameObject> testObjects = new List<GameObject>();

    private void Start()
    {
        fullPath = Path.Combine(path, filename);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveDataUI();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadData();
        }


    }

    #region Save Data
  public  void SaveDataUI()
    {
        updateList();


        JSON_Wrapper wrapper = new JSON_Wrapper();
        wrapper.saveList = saveList;
        string contents = JsonUtility.ToJson(wrapper, true);
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
            File.WriteAllText(fullPath, contents);
        } else
            File.WriteAllText(fullPath, contents);
    }


    public void SaveDataAudio()
    {


        updateTrialSettings();

        JSON_Wrapper_Settings wrapper = new JSON_Wrapper_Settings();
        wrapper.globalSettings = globalSettings;
        string contents = JsonUtility.ToJson(wrapper, true);
        if (File.Exists(fullPath))
        {
            
            File.AppendAllText(fullPath, contents);
        }
        else
            return;

    }

    private void updateList()
    {
        for(int i  = 0; i < testObjects.Count; i++)
        {
            Test_Object tmp = new Test_Object();
            tmp.objectID = i;         
            tmp.objectType = testObjects[i].GetComponent<Saveable_Object>().type;
            tmp.objectPostion = testObjects[i].transform.position;
            tmp.objectScale = testObjects[i].transform.localScale;
            tmp.objectRotation = testObjects[i].transform.localRotation;
            saveList.Save_Objects_Json.Add(tmp);
            saveList.objectCount++;
        }
    }

    private void updateTrialSettings()
    {
        // update stim 
 



        globalSettings.numberOfTrials++;
        globalSettings.globalAmbOrder = globalAmbOrder;
        TrialSettings tmp = new TrialSettings();
        tmp.trialNumber = globalSettings.numberOfTrials;
        tmp.trialType = trialType;
      
       
        
        for(int i = 0; i < teststimuli.Count; i++)
        {
            tmp.testStimuli.Add(teststimuli[i]);
        }

        tmp.numberOfStimuli = teststimuli.Count;

        globalSettings.Save_trials_Json.Add(tmp);

    }


    #endregion

    #region Load Data
  public  void ReadData()
    {

        try {

            if (File.Exists(fullPath))
            {
                string contents = File.ReadAllText(fullPath);
                JSON_Wrapper wrapper = JsonUtility.FromJson<JSON_Wrapper>(contents);
                saveList = wrapper.saveList;

                foreach (GameObject go in testObjects)
                {
                    if (go != null)
                    {
                        Destroy(go.gameObject);
                    }
                }
                testObjects.Clear();

                foreach (Test_Object obj in saveList.Save_Objects_Json)
                {
                    Debug.Log(obj.objectID);
                    LoadObjects(obj);
                }



            }
            else {
                Debug.Log("No File");
                test_Object = new Test_Object();
            }

        } catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

        
    }

    private void LoadObjects(Test_Object obj)
    {

        int objectCount = saveList.objectCount;

        GameObject tmp = null;
        tmp = Instantiate(Resources.Load(obj.objectType) as GameObject);
        
        tmp.transform.position = obj.objectPostion;
        tmp.transform.localScale = obj.objectScale;
        tmp.transform.localRotation = obj.objectRotation;








    }
    #endregion
}
