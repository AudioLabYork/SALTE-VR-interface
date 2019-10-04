using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderPlacer : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;


    [SerializeField] int _segments;

    public GameObject tilePrefab;

    public float cellSize;

    GameObject grid;

    public GameObject ankerPoint;

    BuildGrid _gridTile;

    [SerializeField] GameObject _sliderPrefab;

    public List<Transform> _segmentList = new List<Transform>();


    string[] _labels = new string[] {"_", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };

    // Start is called before the first frame update
    void Start()
    {
        gridWidth = _segments + 1;
        _gridTile = tilePrefab.GetComponent<BuildGrid>();
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


                Vector3 cellOffset = new Vector3(x * (cellSize / (_segments + 1)), this.transform.position.y, z);
                GameObject tempGO = Instantiate(tilePrefab);
                tempGO.transform.position = new Vector3(this.transform.position.x + (cellOffset.x), this.transform.position.y, this.transform.position.z);
                //  tempGO.transform.rotation = Quaternion.identity;
                tempGO.transform.SetParent(transform);
                _segmentList.Add(tempGO.transform);

            }


        }
    }


    private void UpdateSliderPositions()
    {
        for (int i = 1; i < _segmentList.Count; i++)
        {
            GameObject tmpGo = Instantiate(_sliderPrefab);
            ChangeText Label = tmpGo.GetComponent<ChangeText>();
            Label.ChangeLabel(_labels[i]);
            tmpGo.transform.position = new Vector3(_segmentList[i].position.x, _segmentList[i].position.y, _segmentList[i].position.z + 0.22f);
            tmpGo.transform.SetParent(transform);
        }
    }

}
