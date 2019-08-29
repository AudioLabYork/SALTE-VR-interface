using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Buttton_ID : MonoBehaviour
{

   public TextMeshPro buttonID;

    private void Start()
    {
        buttonID = GetComponent<TextMeshPro>();
    }

    public void changeButtonID(string id)
    {
        buttonID.text = id;
    }

}
