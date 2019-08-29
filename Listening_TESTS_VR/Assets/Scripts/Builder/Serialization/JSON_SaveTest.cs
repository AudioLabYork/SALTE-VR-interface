using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class JSON_SaveTest : MonoBehaviour
{
    #region Singleton
    private static JSON_SaveTest instance;

    //  Creat Singleton
    public static JSON_SaveTest Instance
    {

        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<JSON_SaveTest>();
            }
            return instance;
        }

    }
    #endregion


         
    public string filename = "testNEW.json";
    public string path;
    private string fullPath;


    Test_Object test_Object = new Test_Object();
    Saved_Objects saveList = new Saved_Objects();

    public List<GameObject> testObjects = new List<GameObject>();

    private void Start()
    {
        fullPath = Path.Combine(path, filename);
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            /*
            Test_Object object1 = new Test_Object();
            object1.objectID = 1;
            object1.objectType = "bum";
            object1.objectPostion = this.transform.position;
            object1.objectScale = this.transform.localScale;
            object1.objectRotation = this.transform.localRotation;
            saveList.Save_Objects_Json.Add(object1);

            Test_Object object2 = new Test_Object();
            object2.objectID = 2;
            object2.objectType = "bum";
            object2.objectPostion = this.transform.position;
            object2.objectScale = this.transform.localScale;
            object2.objectRotation = this.transform.localRotation;
            saveList.Save_Objects_Json.Add(object2);
            */

            SaveData();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadData();
        }


    }

    #region Save Data
  public  void SaveData()
    {
        updateList();


        JSON_Wrapper wrapper = new JSON_Wrapper();
        wrapper.saveList = saveList;
        string contents = JsonUtility.ToJson(wrapper, true);
        if(File.Exists(fullPath))
        {
            File.Delete(fullPath);
            File.WriteAllText(fullPath, contents);
        } else
            File.WriteAllText(fullPath, contents);
    }

    private void updateList()
    {
        for(int i  = 0; i < testObjects.Count; i++)
        {
            Test_Object tmp = new Test_Object();
            tmp.objectID = i;
            tmp.objectType = testObjects[i].GetComponent<Saveable_Object>().type;
            tmp.objectPostion = testObjects[i].transform.position;
            tmp.objectScale = testObjects[i].transform.localScale;
            tmp.objectRotation = testObjects[i].transform.localRotation;
            saveList.Save_Objects_Json.Add(tmp);
            saveList.objectCount++;
        }
    }
    #endregion

    #region Load Data
  public  void ReadData()
    {

        try {

            if (File.Exists(fullPath))
            {
                string contents = File.ReadAllText(fullPath);
                JSON_Wrapper wrapper = JsonUtility.FromJson<JSON_Wrapper>(contents);
                saveList = wrapper.saveList;

                foreach (GameObject go in testObjects)
                {
                    if (go != null)
                    {
                        Destroy(go.gameObject);
                    }
                }
                testObjects.Clear();

                foreach (Test_Object obj in saveList.Save_Objects_Json)
                {
                    Debug.Log(obj.objectID);
                    LoadObjects(obj);
                }



            }
            else {
                Debug.Log("No File");
                test_Object = new Test_Object();
            }

        } catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }

        
    }

    private void LoadObjects(Test_Object obj)
    {

        int objectCount = saveList.objectCount;

        GameObject tmp = null;
        tmp = Instantiate(Resources.Load(obj.objectType) as GameObject);
        tmp.transform.position = obj.objectPostion;
        tmp.transform.localScale = obj.objectScale;
        tmp.transform.localRotation = obj.objectRotation;
        


    }
    #endregion
}
