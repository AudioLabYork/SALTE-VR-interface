using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Enable : MonoBehaviour
{
    public GameObject panel;
    public bool active = false;

    public Animator anim;

    public void setPanel()
    {
        if (!active)
        {
          
            if(anim != null)
            {

                anim.SetBool("Open", true);
                active = true;
            }

         

        } else
        {
           
            if (anim != null)
            {

                anim.SetBool("Open", false);
                active = false;
            }
        }
    }
}
