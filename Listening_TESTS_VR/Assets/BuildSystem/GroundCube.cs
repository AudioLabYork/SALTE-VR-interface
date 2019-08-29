using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCube : MonoBehaviour
{

    public Color highlightColor;
    private Color normalColor;
    private Color currentColor;

    private MeshRenderer myRend;
    private bool isSelected;

    private BuildSystem buildSystem;



    private void Start()
    {
        myRend = GetComponent<MeshRenderer>();
        normalColor = myRend.material.color;//getting the color of the ground cube (green color)
        currentColor = normalColor;
    }

    public void SetBuildSystem(BuildSystem _build)//setting a ref to the build system that was passed in by the GridSpawner...this 
                                                    //saves us from having to do a GameObject.Find("Whatever").Getcomponent<BuildSystem>()
    {
        buildSystem = _build;
    }

    public void OnMouseEnter()//when the mouse moves over this ground cube this method will get called
    {
        if (!buildSystem.GetIsBuilding())//GetIsBuilding() returns a bool (true/false) from the buildSystem
        {
            HandleSelection();
        }
    }

    public void OnMouseExit()//when the mouse moves off of this ground cube this method will get called
    {
        if (!buildSystem.GetIsBuilding())
        {
            HandleSelection();
        }
    }

    public void HandleSelection()//a better name would be ToggleSelection()
    {
        isSelected = !isSelected;

        if (isSelected)
        {
            currentColor = highlightColor;
        }
        else
        {
            currentColor = normalColor;
        }

        myRend.material.color = currentColor;//setting the MeshRenderer's material color to whatever the currentColor is
    }

}
