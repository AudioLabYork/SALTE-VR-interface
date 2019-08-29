using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setScale : MonoBehaviour
{

    public GridGenerator tileManager;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdatePos", 0.001f);
    }

    // Update is called once per frame
    void UpdatePos()
    {
        gameObject.transform.localScale = new Vector3(tileManager.gridWidth / 10, gameObject.transform.localScale.y, tileManager.gridHeight / 10);



    }
}
