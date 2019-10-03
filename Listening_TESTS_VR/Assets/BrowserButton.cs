using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SimpleFileBrowser;

public class BrowserButton : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI uiText;
    [SerializeField]
    TextMeshProUGUI placeHolderText;


    [SerializeField]
    trialUpdate data;

    public SingleLoadManager _loadManager;

    
    public void SetText(string text)
    {
        uiText.text = text;
    }

    public void OnCLick()
    {
        StartCoroutine(ShowLoadDialogCoroutine());
    }


    // Warning: paths returned by FileBrowser dialogs do not contain a trailing '\' character
    // Warning: FileBrowser can only show 1 dialog at a time

    void Start()
    {

        _loadManager = GameObject.Find("LoadManager").GetComponent<SingleLoadManager>();

        
        FileBrowser.SetFilters(true, new FileBrowser.Filter("Sound File", ".wav", ".mp3"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

       
        FileBrowser.SetDefaultFilter(".wav");

        
        FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

      
        FileBrowser.AddQuickLink("Users", "C:\\Users", null);

       

      
       
    }

    IEnumerator ShowLoadDialogCoroutine()
    {

        yield return FileBrowser.WaitForLoadDialog(false, null, "Load File", "Load");

        placeHolderText.text = FileBrowser.Result;
        Debug.Log(FileBrowser.Success + " " + FileBrowser.Result);
        if (data != null)
        {
            data.location = FileBrowser.Result;
        }
       
         
            PlayerPrefs.SetString("_loadTemplate", FileBrowser.Result);

     
       
       

       

    }




}
