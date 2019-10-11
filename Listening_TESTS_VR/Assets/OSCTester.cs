using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCTester : MonoBehaviour
{
    OSC_IN _osc;
    [SerializeField]  TextPlacer _text;
    [SerializeField] SliderPlacer _sliders;

    public List<string> textlabels = new List<string>();
    public int numberOfLabels;

    public int numberOfSliders;
    public int sliderMin;
    public int sliderMax;

    [SerializeField] GameObject[] buttonsMushra;
    [SerializeField] GameObject[] buttons3G;
    public bool showUI;

    public bool isMushra;
    public bool is3G;

    // Start is called before the first frame update
    void Start()
    {
        _osc = GetComponent<OSC_IN>();
    }


    public void SetSliders()
    {
        _sliders._segments = numberOfSliders;
        _sliders.SetUI(numberOfSliders);
    }

    public void SetText()
    {

        for (int i = 0; i < _text._textList.Count; i++)
        {
            Destroy(_text._textList[i]);
        }

        _text._segments = numberOfLabels;
        _text.SetText();
    }

    public void ClearUI()
    {
        _osc.showUI(false);
        _text.ClearText();
        _sliders.ClearUI();
    }

    public void SetUI()
    {
        ClearUI();
        SetText();

        _sliders._sliderMushra = isMushra;
        _sliders._slider3G = is3G;
        SetSliders();
        _osc.updateSliders();

        for (int i = 0; i < buttons3G.Length; i++)
        {
            buttons3G[i].SetActive(is3G);
        }

        for (int i = 0; i < buttonsMushra.Length; i++)
        {
            buttonsMushra[i].SetActive(isMushra);
        }

        _osc.showUI(true);
    }
}
