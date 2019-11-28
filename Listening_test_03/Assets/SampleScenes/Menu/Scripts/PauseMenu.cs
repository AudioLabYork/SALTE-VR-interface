using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    private float m_TimeScaleRef = 1f;
    private float m_VolumeRef = 1f;
    private bool m_Paused = false;


    void Awake()
    {
        pausePanel.SetActive(false);
    }


    private void PauseGame()
    {
        m_TimeScaleRef = Time.timeScale;
        Time.timeScale = 0f;
        m_VolumeRef = AudioListener.volume;
        AudioListener.volume = 0f;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = m_TimeScaleRef;
        AudioListener.volume = m_VolumeRef;
        pausePanel.SetActive(false);
        //enable the scripts again
    }


    void Update()
	{
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (!pausePanel.activeInHierarchy)
            {
                PauseGame();
                print("pause");
            }
            if (pausePanel.activeInHierarchy)
            {
                ContinueGame();
                print("play");
            }
        }
	}


}
