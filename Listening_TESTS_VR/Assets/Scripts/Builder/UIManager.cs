using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameObject uiMenu;

    public bool mouseOverUIElement;

    private Vector3 startPosition;
    private Vector3 endPostion;

    public float lerpTime = 1f;
    public float timeStartedLerping;

    private static UIManager instance = null;

    bool isLerping;

    private void Awake()
    {
        instance = this;

    }

    public static UIManager GetInstance()
    {
        return instance;
    }

    public Animator animator;


    public List<GameObject> testObjects = new List<GameObject>();

    public bool uiOpen;

    public void MenuOpen()
    {
        if(uiOpen)
        {
            uiOpen = false;
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Open");

                animator.SetBool("Open", uiOpen);
            }
        }  else if (!uiOpen)
        {
            uiOpen = true;
            if (animator != null)
            {
                bool isOpen = animator.GetBool("Open");

                animator.SetBool("Open", uiOpen);
            }
        }
    }

 
    public void MouseEnter()
    {
        mouseOverUIElement = true;
        if(animator != null)
        {
            bool isOpen = animator.GetBool("Open");

            animator.SetBool("Open", mouseOverUIElement);
        }
    }

    public void MouseExit()
    {
        mouseOverUIElement = false;

        if (animator != null)
        {
            bool isOpen = animator.GetBool("Open");

            animator.SetBool("Open", mouseOverUIElement);
        }
    }


}
