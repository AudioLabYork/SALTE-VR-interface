using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public TextMeshProUGUI theText;
    public RawImage[] line;

    Color main;
    Color highlight = new Color(112,178,234,255);


    private void Start()
    {
        main = theText.color;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        theText.color = Color.blue; //Or however you do your color
        foreach(RawImage l in line)
        {
            l.color = Color.blue;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        theText.color = main; //Or however you do your color
        foreach (RawImage l in line)
        {
            l.color = main;
        }
    }
}
