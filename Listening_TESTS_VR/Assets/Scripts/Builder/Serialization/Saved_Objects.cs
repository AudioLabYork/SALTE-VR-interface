using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

    [Serializable]
public class Saved_Objects
{
    /// <summary>
    /// This class holds a list containing all the interface objects.  Used for storage within a JSON file. 
    /// </summary>


    public int objectCount;
    public List<Test_Object> Save_Objects_Json = new List<Test_Object>();
}
