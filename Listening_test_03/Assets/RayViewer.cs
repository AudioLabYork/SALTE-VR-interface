using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour {

    public float range = 50f;
    public float printFreq = 0.2f;
    private Camera fpsCam;
    private float timeCount = 0.0f;
    private float counter = 0.0f;
    private float azi = 0.0f;
    private float ele = 0.0f;

    

	// Use this for initialization
	void Start () {
        //fpsCam = GetComponentInParent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
        //Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Vector3 forward = transform.TransformDirection(Vector3.forward) * range;
        Debug.DrawRay(transform.position, forward, Color.green);
        //Debug.DrawRay(lineOrigin, fpsCam.transform.forward * range, Color.green);


        if (timeCount > printFreq)
        {
            //print(counter);
            if (transform.eulerAngles.x <= 90)
            {
                ele = transform.eulerAngles.x * -1;
            }
            else
            {
                ele = (transform.eulerAngles.x - 360) * -1;
            }

            azi = transform.eulerAngles.y;


            print("azi: " + azi + " | ele: " + ele);
            timeCount = 0.0f;
            counter += 1;
            
        }
         
     timeCount = timeCount + Time.deltaTime;
    }


  
}
