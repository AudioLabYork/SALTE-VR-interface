using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


enum ObjectType { Slider_3G_Cont, Slider_Mush_Cont , Button_Play, Button_Stop, Button_Loop, Button_Next, Button_Prev, Button_A, Button_B,  Cube, Sphere }
public abstract class Saveable_Object : MonoBehaviour
{


    /// <summary>
    ///  Most of this script will be trashed.  It will primarily be used to define the object type, colour etc.
    /// </summary>

    protected string save;

    [SerializeField]
    private ObjectType objectType;

    public string type;

    public TextMeshPro sliderTextID;
    public string _sliderID;

    public int _sliderIndex;

  public  H_slider sliderInfo;

    private void Start()
    {

        type = objectType.ToString();

        

        // Add the test object to the saveable manager list 
        // Player Prefs
        JSON_SaveTest.Instance.testObjects.Add(gameObject);

        
    
    }

    public void SetIndex(string i)
    {
        _sliderIndex = int.Parse(i);

        if(sliderInfo != null)
        {
            sliderInfo.slider_index = int.Parse(i);
        }
    }

    public void SetTEXTID(string id)
    {
        _sliderID = sliderTextID.text.ToString();
        sliderTextID.text = id;
    }



    public virtual void Save(int id)
    {
        // Save the object type, postion, scale and rotation.

        // Player Prefs save 
        PlayerPrefs.SetString(id.ToString(), objectType + "_" + transform.position.ToString() + "_" + transform.localScale.ToString() + "_" + transform.localRotation.ToString());


        // JSON

        // Save the object variables
        // Add object to the list
        SaveBuildManager.Instance.UpdateList(CreateObject(id));

      
    }


    private Test_Object CreateObject(int id)
    {
        Test_Object objectJSON = new Test_Object()
        {
            objectID = id,
            objectType = objectType.ToString(),         
        objectPostion = transform.position,
        objectScale = transform.localScale,
        objectRotation = transform.localRotation
        };
   
        return objectJSON;
    }

    public virtual void Load(string[] values)
    {
        transform.localPosition = SaveBuildManager.Instance.StringToVector(values[1]);
        transform.localScale = SaveBuildManager.Instance.StringToVector(values[2]);
        transform.localRotation = SaveBuildManager.Instance.StringToQuaternion(values[3]);
    }

    public void DestroySaveable()
    {

    }

}
