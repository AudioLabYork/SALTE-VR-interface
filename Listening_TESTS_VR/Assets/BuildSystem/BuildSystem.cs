using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{

    public Camera cam;
    public LayerMask layer;

    public BuildSelector selector;

    private GameObject preview;//this is the preview object that you will be moving around in the scene
    private PreviewObj previewScript;//this is the script that is sitting on that object

    private bool isBuilding = false;



    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isBuilding && previewScript.CanBuild())//pressing LMB, and isBuiding = true, and the Preview Script -> canBuild = true
        {
            BuildIt();//then build the thing
        }

        if (Input.GetMouseButtonDown(1) && isBuilding)//stop build
        {
            StopBuild();
        }

        if (Input.GetKeyDown(KeyCode.R) && isBuilding)//for rotation
        {
            preview.transform.Rotate(0f, 90f, 0f);//spins like a top, in 90 degree turns
        }

        if (isBuilding)
        {
            DoRay();
        }
    }

    public void NewBuild(GameObject _go)//this gets called by one of the buttons 
    {
        preview = Instantiate(_go, Vector3.zero, Quaternion.identity);//set the preview = to something
        previewScript = preview.GetComponent<PreviewObj>();//grab the script that is sitting on the preview
        isBuilding = true;//we can now build
    }

    private void StopBuild()
    {
        Destroy(preview);//get rid of the preview
        preview = null;//not sure if you need this actually
        previewScript = null;//
        isBuilding = false;
        selector.TogglePanel();//toggle the button panel back on
    }

    private void BuildIt()//actually build the thing
    {
        previewScript.Build();//just calls the Build() method on the previewScript
        StopBuild();
    }

    private void DoRay()//simple ray cast from the main camera. Notice there is no range
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layer))//notice there is a layer that we are worried about
        {
            PositionObj(hit.point);
        }
    }

    private void PositionObj(Vector3 _pos)
    {
        int x = Mathf.RoundToInt(_pos.x);//just round the x,y,z values to the nearest int
        //int y = Mathf.RoundToInt(_pos.y);//personal preferance to comment this out. I hard coded in my y value
        int z = Mathf.RoundToInt(_pos.z);

        preview.transform.position = new Vector3(x, 1f, z);//set the previews transform postion to a new Vector3 made up of the x,y,z that you roundedToInt

    }


    public bool GetIsBuilding()//just returns the isBuilding bool, so it cant get changed by another script
    {
        return isBuilding;
    }

}


