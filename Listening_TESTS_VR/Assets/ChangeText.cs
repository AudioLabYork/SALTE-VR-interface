using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangeText : MonoBehaviour
{


    [SerializeField] TextMeshPro textLAbel;
    public void ChangeLabel(string text)
    {
      textLAbel.text = text;
    }
}
