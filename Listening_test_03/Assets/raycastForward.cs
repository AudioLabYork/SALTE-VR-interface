using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycastForward : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        float theDistance;
        // Debug Raycast in the Editor - So we can see the raycast
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, forward, Color.blue);

        //if (Physics.Raycast(transform.position, (forward), out hit))
        //{
        //    theDistance = hit.distance;
            //print (OVRPlayerController.trans)
        //}
		
	}
}
