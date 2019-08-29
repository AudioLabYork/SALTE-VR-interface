using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trialUpdate : MonoBehaviour
{

    JSON_SaveTest data;

    public List<StimuliSettings> teststimuli = new List<StimuliSettings>();



    public string location = "test/location";
   public int order = 1;
    public string configLocation = "text/location" ;

    public SettingsMenuUI settingsUI;

    private void Start()
    {
        data = GetComponent<JSON_SaveTest>();

    }

    
    public void updateList()
    {
        StimuliSettings stim = new StimuliSettings();
        stim.ambixconfig = "test/location";
        stim.source = location;
        stim.order = 1;
        teststimuli.Add(stim);     

    }



    public void UpdateGlobalOrder(int n)
    {
        data.globalAmbOrder = n + 1;
    }

    public void UpdateTrialOrder(int n)
    {
        data.ambOrder = n;
    }


    public void UpdateTrialType(string type)
    {
        data.trialType = type;
    }

    public void UpdateReference(string location)
    {
        data.reference = location;
    }

    public void UpdateStimOrder(int n)
    {
        order = n;
        updateList();
    }

    public void SaveList()
    {
        for(int i = 0; i < teststimuli.Count; i++)
        {
            data.teststimuli.Add(teststimuli[i]);
        }
    }


    public void UpdateNumberOfTrials(string number)
    {
        data.numberOfTrials = int.Parse(number);
        settingsUI.trialNumber = int.Parse(number);
    }



}
