using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveBuildManager : MonoBehaviour
{
    #region Singleton
    private static SaveBuildManager instance;

    //  Creat Singleton
    public static SaveBuildManager Instance
    {

        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<SaveBuildManager>();
            }
            return instance;
        }
        
    }
    #endregion



    //  Collection of saveable objects 
    public List<Saveable_Object> Saveable_Objects;





    Saved_Objects saveList;

    Test_Object test_Object;


    private void Start()
    {

        Saved_Objects saveList = new Saved_Objects();
        Test_Object test_Object = new Test_Object();
    }

    public void UpdateList(Test_Object obj)
    {
        test_Object = obj;
        saveList.Save_Objects_Json.Add(test_Object);

    }





    private void Awake()
    {
        Saveable_Objects = new List<Saveable_Object>();
      

    }

    public void Save()
    {
        PlayerPrefs.SetInt("ObjectCount", Saveable_Objects.Count);

        for(int i = 0; i < Saveable_Objects.Count; i++)
        {
            Saveable_Objects[i].Save(i);            
        }



   

        string json = JsonUtility.ToJson(saveList, true);
        string folder = "H:/Unity3D/SALTE/SAVED_DATA/TEST"; // To be changed 
        string file = "testNEW.json"; // To be changed 
        string fullPath = Path.Combine(folder, file);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
        else
            File.WriteAllText(fullPath, json);

    }

    public void Load()
    {

        foreach(Saveable_Object obj in Saveable_Objects)
        {
            if(obj != null)
            {
                Destroy(obj.gameObject);
            }
        }

        Saveable_Objects.Clear();

        int objectCount = PlayerPrefs.GetInt("ObjectCount");

        for(int i = 0; i < objectCount; i++)
        {
            string[] value = PlayerPrefs.GetString(i.ToString()).Split('_');
            Debug.Log(value);
            GameObject tmp = null;

            tmp = Instantiate(Resources.Load(value[0]) as GameObject);
            /*
            switch (value[0])
            {
                case "Cube":
                  tmp  = Instantiate(Resources.Load("Cube") as GameObject);
                    break;
                case "Sphere":
                    tmp = Instantiate(Resources.Load("Sphere") as GameObject);
                    break;
            } 
            */

            if (tmp != null)
            {
                tmp.GetComponent<Saveable_Object>().Load(value);
            }
            
           
        }
    }

    public Vector3 StringToVector(string value)
    {
        value = value.Trim(new char[] {'(',')' });
        value = value.Replace(" ", "");
        string[] pos = value.Split(',');

        return new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]));
    }

 public Quaternion StringToQuaternion(string value)
   {
        value = value.Trim(new char[] { '(', ')' });
        value = value.Replace(" ", "");
        string[] pos = value.Split(',');

        return new Quaternion(float.Parse(pos[0]), float.Parse(pos[1]), float.Parse(pos[2]), float.Parse(pos[3]));

    }

}
