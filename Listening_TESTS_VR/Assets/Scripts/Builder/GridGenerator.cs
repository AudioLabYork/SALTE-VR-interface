using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;

    public GameObject tilePrefab;

    public float cellSize;

    GameObject grid;

    public GameObject ankerPoint;

    // Start is called before the first frame update
    void Start()
    {

        UpdateGrid();

    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = ankerPoint.transform.position;
        
    }

    private void UpdateGrid()
    {


       for( int x = 0; x < gridWidth; x++)
        {
         

            for(int z = 0; z < gridHeight; z++)
            {


                Vector3 cellOffset = new Vector3(x * cellSize, this.transform.position.y, z * cellSize);
                GameObject tempGO = Instantiate(tilePrefab);
                tempGO.transform.position = new Vector3(this.transform.position.x + (cellOffset.x), this.transform.position.y, this.transform.position.z + (cellOffset.z));
              //  tempGO.transform.rotation = Quaternion.identity;
                tempGO.transform.SetParent(transform);
            }


        }
    }
}
