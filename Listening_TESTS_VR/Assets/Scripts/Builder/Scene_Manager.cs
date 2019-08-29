using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Manager : MonoBehaviour
{


    public GameObject[] turnOn;

    // Start is called before the first frame update
    void Start()
    {
        if (turnOn != null)
        {
            foreach (GameObject go in turnOn)
            {
                go.SetActive(true);
            }
        }
        else
            return;


    }

   public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
