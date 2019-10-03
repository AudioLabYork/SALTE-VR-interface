using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SliderText : MonoBehaviour
{

    [SerializeField] TextMeshPro _text;

    public string _thisID;

    [SerializeField] string _saveID;


    private void Start()
    {
       _thisID = _text.text.ToString();
    }
   
    public void UpdateText()
    {
        _thisID = _text.text.ToString();
        _saveID = _thisID;
    }


}
