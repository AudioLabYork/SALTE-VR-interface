using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(OSCTester))]
public class OSCTetserEditor : Editor
{


    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        OSCTester tester = (OSCTester)target;
        if(GUILayout.Button("Set Sliders"))
        {
            tester.SetSliders();
        }

        if (GUILayout.Button("Set Text"))
        {
            tester.SetText();
        }

        if (GUILayout.Button("Hide UI"))
        {
            tester.HideUI();
        }

        if (GUILayout.Button("Clear UI"))
        {
            tester.ClearUI();

        }


        if (GUILayout.Button("Set UI"))
        {
            tester.SetUI();

        }
    }

}

