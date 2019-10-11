using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextPlacer : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;


    public int _segments;

    public GameObject tilePrefab;

    public float cellSize;

    GameObject grid;

    public GameObject ankerPoint;

    [SerializeField] SetPosition _screenPostion;

    [SerializeField] OSC_IN _osc;

    public GameObject screen;

    BuildText _gridTile;

    [SerializeField] GameObject _textPrefab;

    public List<GameObject> _textList = new List<GameObject>();

    public List<GameObject> _segmentList = new List<GameObject>();

    string[] _textDebug = new string[] { "Bad", "Poor", "Fair", "Good", "Excellent"};

    // Start is called before the first frame update
    void Start()
    {
        gridWidth = 1;
        gridHeight = _segments +1 ;
        _gridTile = tilePrefab.GetComponent<BuildText>();
        _gridTile.cellSize = _gridTile.cellSize / (_segments + 1);
       


    }

    public void ClearText()
    {
       
        foreach (GameObject go in _textList)
        {
            Destroy(go);
        }

        foreach(GameObject go in _segmentList)
        {
            Destroy(go);
        }
  

        _textList.Clear();
        _segmentList.Clear();
        
    }

    public void SetText()

    {


        _gridTile.cellSize = _gridTile.cellSize / (_segments + 1);
        gridHeight = _segments;
        _screenPostion.ResetPos();
        UpdateGrid();
        UpdateTextPositions();
        _screenPostion.UpdatePos();
        UpdateTextString();

       

    }

    private void UpdateTextString()
    {
        for (int i = 0; i < _segments; i++)
        {
            TextMeshPro text = _textList[i].GetComponentInChildren<TextMeshPro>();
            text.text = _osc.labelStrings[i];
        }
    }



    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = ankerPoint.transform.position;



    }

    private void UpdateGrid()
    {


        for (int x = 0; x < gridWidth; x++)
        {


            for (int z = 0; z < gridHeight; z++)
            {


                Vector3 cellOffset = new Vector3(x , this.transform.position.y, z * (0.405f / gridHeight));
                GameObject tempGO = Instantiate(tilePrefab);
                tempGO.transform.SetParent(screen.transform);
                tempGO.transform.position = new Vector3( this.transform.localPosition.x , this.transform.position.y, this.transform.position.z + (cellOffset.z) +0.04f);
                
                
                _segmentList.Add(tempGO);

            }


        }
    }


    private void UpdateTextPositions()
    {

     


        for (int i = 0; i < _segmentList.Count; i++)
        {
            GameObject tmpGo = Instantiate(_textPrefab);
            tmpGo.transform.SetParent(transform);
            tmpGo.transform.position = new Vector3(_segmentList[i].transform.position.x - 0.67f, _segmentList[i].transform.position.y, _segmentList[i].transform.position.z);
            
            _textList.Add(tmpGo);
        }


    }



    public void UpdateText(string textInput , int index)
    {
        TextMeshPro _text = _textList[index].GetComponentInChildren<TextMeshPro>();
        _text.text = textInput;
    }
}
