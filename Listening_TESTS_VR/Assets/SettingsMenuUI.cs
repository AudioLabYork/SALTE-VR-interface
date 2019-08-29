using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsMenuUI : MonoBehaviour
{


    public TextMeshProUGUI uiText;
    public JSON_SaveTest save;
    public ScrollListControl button;

    public int trialNumber;
    public int count = 0;

    public void UpdateTitle()
    {
        if (count < trialNumber) {

            count++;
            uiText.text = "Trial " + count + " Settings";
            save.trialNumber = count;

            button.numberOfStimuli = trialNumber;
                }
        
    }






}
