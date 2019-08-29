using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave_Slider : MonoBehaviour
{

    public string savePath;
    public string filename;

    public float sliderValue;
    public int sampleNumber;

    TestData testData = new TestData();

    List<TestData> saveSliderData = new List<TestData>();

    public float getValue()
    {
        
       float value = sliderValue;
        return value;
    }


 






}
