using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollListControl : MonoBehaviour
{
    [SerializeField]
    private GameObject broswerPrefab;


    private List<int> intList;

    public int numberOfStimuli = 1;

    public void UpdateNumber(string n)
    {
        numberOfStimuli = int.Parse(n);

        GenerateBroswers();
    }

    private void Start()
    {
      

    }

    public void GenerateBroswers()
    {
        for(int i = 1; i < (numberOfStimuli + 1); i++)
        {
            GameObject browser = Instantiate(broswerPrefab) as GameObject;
            browser.SetActive(true);

            browser.GetComponent<BrowserButton>().SetText("Stimuli #" + i);
            browser.transform.SetParent(broswerPrefab.transform.parent, false);



        }
    }
}
