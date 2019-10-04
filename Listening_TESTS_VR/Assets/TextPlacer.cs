using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TextPlacer : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;


    [SerializeField] int _segments;

    public GameObject tilePrefab;

    public float cellSize;

    GameObject grid;

    public GameObject ankerPoint;

    BuildText _gridTile;

    [SerializeField] GameObject _textPrefab;

    public List<Transform> _segmentList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        gridWidth = 1;
        gridHeight = _segments +1 ;
        _gridTile = tilePrefab.GetComponent<BuildText>();
        _gridTile.cellSize = _gridTile.cellSize / (_segments + 1);
        UpdateGrid();
        UpdateSliderPositions();


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
                tempGO.transform.position = new Vector3( this.transform.localPosition.x - 0.25f , this.transform.position.y, this.transform.position.z + (cellOffset.z) +0.04f);
                //  tempGO.transform.rotation = Quaternion.identity;
                tempGO.transform.SetParent(transform);
                _segmentList.Add(tempGO.transform);

            }


        }
    }


    private void UpdateSliderPositions()
    {
        for (int i = 0; i < _segmentList.Count; i++)
        {
            GameObject tmpGo = Instantiate(_textPrefab);
            tmpGo.transform.position = new Vector3(_segmentList[i].position.x, _segmentList[i].position.y, _segmentList[i].position.z);
            tmpGo.transform.SetParent(transform);
        }
    }
}
