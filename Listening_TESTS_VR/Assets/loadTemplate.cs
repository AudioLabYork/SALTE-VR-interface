using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadTemplate : MonoBehaviour
{

  public  JSON_SaveTest _saveData;
    public string _fileName;
    bool _loadData = false;


    SingleLoadManager _loadManager;

    // Start is called before the first frame update
    void Start()
    {
        _saveData = GetComponent<JSON_SaveTest>();
        _loadManager = GameObject.Find("LoadManager").GetComponent<SingleLoadManager>();
        _fileName = PlayerPrefs.GetString("_loadTemplate");
   
     //   _saveData.ReadData();

    }

    // Update is called once per frame
    void Update()
    {

        if (!_loadData)
        {
            _loadData = true;

            if (_fileName != null)
            {
                _saveData.fullPath = PlayerPrefs.GetString("_loadTemplate");
                _saveData.ReadData();
            } else
            {
                return;
            }
        }
        
    }
}
