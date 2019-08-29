using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class TrialSettings 
{
    public string trialType;
    public int trialNumber;
    
   
    public string reference;

    public int numberOfStimuli;

    public List<StimuliSettings> testStimuli = new List<StimuliSettings>();
    


}
