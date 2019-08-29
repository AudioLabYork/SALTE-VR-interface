using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GlobalTestSettings
{

    public int numberOfTrials;
    public int globalAmbOrder;

    public List<TrialSettings> Save_trials_Json = new List<TrialSettings>();

}
