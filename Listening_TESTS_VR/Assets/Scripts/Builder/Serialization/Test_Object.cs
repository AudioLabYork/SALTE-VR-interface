using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Test_Object 
{
    /// <summary>
    /// This class holds all the information regarding postion, rotation, scale and type. This is used for saving and loading the interface objects.
    /// </summary>


    // Type of Object 
    public int objectID;
    public string objectType;

    // Postion and rotation of object 
    public Vector3 objectPostion;
    public Vector3 objectScale;
    public Quaternion objectRotation;



}
