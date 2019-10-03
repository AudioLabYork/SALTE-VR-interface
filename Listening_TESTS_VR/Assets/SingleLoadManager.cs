using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLoadManager : MonoBehaviour
{

  public  static SingleLoadManager _instance
    {
        get;
        set;
    }

    public string _loadData;


    void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        } else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            
        }
    }

}
