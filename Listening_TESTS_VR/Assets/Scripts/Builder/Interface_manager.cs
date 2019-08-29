using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interface_manager : MonoBehaviour
{
    public bool mouseOverUIElement;

    private static Interface_manager instance = null;

    private void Awake()
    {
        instance = this;

    }

    public static Interface_manager GetInstance()
    {
        return instance;
    }

    public void MouseEnter()
    {
        mouseOverUIElement = true;
    }

    public void MouseExit()
    {
        mouseOverUIElement = false;
    }
}
