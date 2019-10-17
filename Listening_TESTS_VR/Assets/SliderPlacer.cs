using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SliderPlacer : MonoBehaviour
{
    public int gridWidth = 8;
    public int gridHeight = 8;

  [SerializeField]  OSC_IN _osc;

   public int _segments;

    public GameObject tilePrefab;

    public float cellSize;

    GameObject grid;

    [SerializeField] SetPosition _screenPostion;

    public GameObject ankerPoint;

    BuildGrid _gridTile;

    [SerializeField] GameObject[] _sliderMushraPrefab;
    [SerializeField] GameObject _slider3GPrefab;

    public List<GameObject> _segmentList = new List<GameObject>();

    public bool _sliderMushra;
    public bool _slider3G;


    string[] _labels = new string[] {"_", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" , "K" };

    // Start is called before the first frame update
    void Start()
    {

       
        gridWidth = _segments + 1;
        _gridTile = tilePrefab.GetComponent<BuildGrid>();
        _gridTile.cellSize = _gridTile.cellSize / (_segments + 1);

        _sliderMushra = true;
       
  
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = ankerPoint.transform.position;

    }


    public void SetUI(int segment)
    {

        gridWidth = _segments + 1;
        _gridTile.cellSize = _gridTile.cellSize / (_segments + 1);
        _segments = segment;
        _screenPostion.ResetPos();
        UpdateGrid();
        UpdateSliderPositions();
        _screenPostion.UpdatePos();
    }

    public void ClearUI()
    {
      foreach(GameObject slider in _osc.sliders)
        {
            Destroy(slider);
        }

      foreach(GameObject segment in _segmentList)
        {
            Destroy(segment);
        }
        _osc.sliders.Clear();

        _segmentList.Clear();

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
                _segmentList.Add(tempGO);

            }


        }
    }


    private void UpdateSliderPositions()
    {
        for (int i = 1; i < _segmentList.Count; i++)
        {
            if (_sliderMushra)
            {
                GameObject tmpGo = Instantiate(_sliderMushraPrefab[i - 1]);
                ChangeText Label = tmpGo.GetComponent<ChangeText>();
                Label.ChangeLabel(_labels[i]);
                tmpGo.transform.position = new Vector3(_segmentList[i].transform.position.x, _segmentList[i].transform.position.y, _segmentList[i].transform.position.z + 0.22f);
                
                tmpGo.transform.SetParent(transform);
                 _osc.sliders.Add(tmpGo);
            }  else if (_slider3G)
            {
                GameObject tmpGo = Instantiate(_slider3GPrefab);
                ChangeText Label = tmpGo.GetComponent<ChangeText>();
                if(Label != null) { Label.ChangeLabel(_osc.attributeLabels[i - 1]);
                Debug.Log("hello");}                
                tmpGo.transform.position = new Vector3(_segmentList[i].transform.position.x, _segmentList[i].transform.position.y, _segmentList[i].transform.position.z + 0.22f);
                tmpGo.transform.SetParent(transform);
                  _osc.sliders.Add(tmpGo);
            }
        }
    }

}
