using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{

 public   GameObject[] objects;
    ObjectPlace placeObj;

    // Start is called before the first frame update
    void Start()
    {
        placeObj = GetComponent<ObjectPlace>();
        
    }

    public void createObeject(int index)
    {
        placeObj.setItem(objects[index]);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
