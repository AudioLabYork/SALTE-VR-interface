using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class debugPointer : MonoBehaviour
{
    public float defaultLength = 50.0f;
    public GameObject dot;
   

    private LineRenderer _lineRenderer;


    public LayerMask _layer;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }


    private void Update()
    {
        UpdateLine();
    }


    private void UpdateLine()
    {

        // Use default distance 

        float targetLength = defaultLength;

        // raycast 
        RaycastHit hit = CreateRayCast(targetLength, _layer);

        // default 
        Vector3 endPosition = transform.position + (transform.forward * targetLength);

        if(hit.collider.gameObject.tag == "ignore")
        {
            Physics.IgnoreCollision(hit.collider, hit.collider.gameObject.GetComponent<Collider>());
        }

        // or based on hit 
        if (hit.collider != null)
       
        endPosition = hit.point;

        // set position of dot 
        dot.transform.position = endPosition;

        // set position of line renderer
        _lineRenderer.SetPosition(0, transform.position);
        _lineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRayCast (float length, int layer)
    {

        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, defaultLength, layer);

        return hit;
    }



}
