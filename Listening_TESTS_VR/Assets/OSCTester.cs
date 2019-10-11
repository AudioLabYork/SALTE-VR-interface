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
      
        for (int i = 0; i < _text._textList.Count; i++) Destroy(_text._textList[i]);
        
        
       
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
        

        if (is3G)
        {
            _sliders._sliderMushra = false;
            _sliders._slider3G = true;
            SetSliders();
           _osc.showUI(true);


            for (int i = 0; i < buttons3G.Length; i++)
            {
               
                    buttons3G[i].SetActive(true);

            }


            for (int i = 0; i < buttonsMushra.Length; i++)
            {
              
                    buttonsMushra[i].SetActive(false);

            }


        }

        if (isMushra)
        {
            _sliders._sliderMushra = true;
            _sliders._slider3G = false;
            SetSliders();
            _osc.showUI(true);
            for (int i = 0; i < buttonsMushra.Length; i++)
            {
               
                    buttonsMushra[i].SetActive(true);

              
            }

            for (int i = 0; i < buttons3G.Length; i++)
            {
           
                    buttons3G[i].SetActive(false);

               
            }


        }
    }

    public void HideUI()
    {
       
    }
 
}
