using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sliderSelect : MonoBehaviour
{
    Vector3 scaleMin;
    Vector3 scaleMax; 

    public float speed = 15;

    BoxCollider collider;

    public bool scaleTest;

  public  Outline outline;

    private void Start()
    {
       scaleMin = new Vector3(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
       scaleMax = new Vector3((this.transform.localScale.x + 0.01f), this.transform.localScale.y, this.transform.localScale.z);

        collider = GetComponent<BoxCollider>();

        outline = GetComponentInChildren<Outline>();

     outline.enabled = false;
    }


    private void Update()
    {
        if (scaleTest)
            scaleUp();

        else
            scaleDown();


        
           
    }

    private void OnTriggerEnter(Collider other)
    {
        scaleTest = true;
        Debug.Log("Hit");
        outline.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        scaleTest = false;
        Debug.Log("Leave");
        outline.enabled = false;
    }



    public void scaleUp()
    {
   //     if (transform.localScale == scaleMax)
      //      return;
     

            StartCoroutine("UpScale");
  

    }

    IEnumerator UpScale()
    {
        Vector3 current = new Vector3( this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

        transform.localScale = Vector3.Lerp(current, scaleMax, Time.deltaTime * speed);
       yield break;
    }


    public void scaleDown()
    {

     //   if (transform.localScale == scaleMin)
        //    return;

        StartCoroutine("DownScale");

    }

    IEnumerator DownScale()
    {
        Vector3 current = new Vector3( this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);

        transform.localScale = Vector3.Lerp(current, scaleMin, Time.deltaTime * speed);

        yield break;
    }
  

}
