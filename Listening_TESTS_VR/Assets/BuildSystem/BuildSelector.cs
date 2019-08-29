using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSelector : MonoBehaviour
{

    public GameObject buildPanel;
    public BuildSystem buildSystem;
    private bool showPanel = true;



    public void StartBuild(GameObject go)//--this is what the buttons hook into
                                         //on the button click event in the inspector there is a place to 
                                         // add your own preview gameobject
    {
        buildSystem.NewBuild(go);//this "Starts" a new build in the build system
        TogglePanel();
    }

    public void TogglePanel()
    {
        showPanel = !showPanel;
        buildPanel.SetActive(showPanel);
    }

}
