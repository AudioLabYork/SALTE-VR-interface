using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node_Select : MonoBehaviour
{
    public Material[] selected;

    public bool objectPlaced = false;


    private void OnMouseEnter()
    {
      //  gameObject.GetComponent<MeshRenderer>().material = new Material(selected[0]); 
    }

    private void OnMouseExit()
    {
      //  gameObject.GetComponent<MeshRenderer>().material = new Material(selected[1]);
    }

}
