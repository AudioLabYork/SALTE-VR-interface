using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System;

public class WriteTestCSV : MonoBehaviour
{
    // List created to store information from sliders
    public List<string[]> rowData = new List<string[]>();

    // Array to store all sliders in the scene
    public GameObject[] sliders;

    // Location of CSV file
    public string savePath;
    public string filename;


    public int currentStimulus;

    string[] tags = new string[]{ "Slider", "Slider_Cont3G" };

    private void Start()
    {
        // Grab all the sliders in the scene.
        sliders = GameObject.FindGameObjectsWithTag(tags[1]);


       
        currentStimulus = 0;

        listSetup();
       
    }


    private string getPath()
    {

        string folder = savePath;
        string file = filename + ".csv";
        string fullpath = System.IO.Path.Combine(folder, file);


        return fullpath;


    }


    void listSetup()
    {
        // create an string array based on the amount of sliders + 1.
        string[] rowDataTemp = new string[sliders.Length + 5];

        // create the first row of data, based on names of the sliders;
        rowDataTemp[0] = "Session Test ID";
        rowDataTemp[1] = "Name";
        rowDataTemp[2] = "Age";
        rowDataTemp[3] = "Gender";
        rowDataTemp[4] = "Stimulus";



        for (int i = 0; i < sliders.Length; i++)
        {

            rowDataTemp[i + 5] = sliders[i].name;

        }

        rowData.Add(rowDataTemp); // add the first row to the list.

    }


    void saveCSV()
    {
        // This method saves the created list to CSV at the desired file location

        // create a mulit dimensional string array to store the list
        string[][] output = new string[rowData.Count][];

        // copy information from list to array 
        for(int i = 0; i < output.Length;i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);

        // set the delimeter 
        string delimiter = ",";

        // Instantiate string builder class
        StringBuilder sb = new StringBuilder();

        // Create String from array
        for(int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));

        }

        // Get filepath
        string filePath = getPath();

        // Creates and writes the CSV file
        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();




    }




    public void addRecord()
    {
        // add a line to the list with slider and stimulus information 
        string[] rowDataTemp = new string[sliders.Length + 5];

        for(int i = 0; i < sliders.Length; i++)
        {

            var save = sliders[i].GetComponent<DataSave_Slider>();

            // Add constant variables 
            rowDataTemp[0] = PersistenManagerScript.Instance.sessionID.ToString();
            rowDataTemp[1] = PersistenManagerScript.Instance.participantName.ToString();
            rowDataTemp[2] = PersistenManagerScript.Instance.participantAge.ToString();
            rowDataTemp[3] = PersistenManagerScript.Instance.participantGender.ToString();
            rowDataTemp[4] = save.sampleNumber.ToString();

            // Add the slider value 
            rowDataTemp[i + 5] = save.sliderValue.ToString();


        }


        rowData.Add(rowDataTemp);

        saveCSV();

    }


}
