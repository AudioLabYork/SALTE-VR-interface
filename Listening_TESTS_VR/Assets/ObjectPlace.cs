using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlace : MonoBehaviour
{
    private Vector3 mouseOffset;

    private float mouseZCoord;

    private Transform currentObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObject != null)
        {
            moveObject();
        }
    }

    private void moveObject()
    {
        // Vector3 m = Input.mousePosition;
        //   m = new Vector3(m.x, m.y,transform.position.y);
        //  Vector3 p = Camera.main.ScreenToWorldPoint(m);
        //  currentObject.position = new Vector3(p.x, 1.78f, p.z);

        mouseZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

        mouseOffset = gameObject.transform.position - GetWorldPos();

        currentObject.transform.position = GetWorldPos() + mouseOffset;

    }

    private Vector3 GetWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mouseZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }


    public void setItem(GameObject obj)
    {
        currentObject = ((GameObject)Instantiate(obj)).transform;

    }
}
