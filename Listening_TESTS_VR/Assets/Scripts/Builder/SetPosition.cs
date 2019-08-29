using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetPosition : MonoBehaviour
{
    public GridGenerator tileManager;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("UpdatePos", 0.001f);
        tileManager = GameObject.Find("TileManager").GetComponent<GridGenerator>();
      //  gameObject.transform.localScale = new Vector3(tileManager.gridWidth / 10, gameObject.transform.localScale.y, tileManager.gridHeight / 10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void UpdatePos()
    {
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x - 30f, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z);

        

    }

}
