using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObject : MonoBehaviour
{
    public JSON_SaveTest saveData;
  
    public GameObject[] objectPrefab;
    int prefabIndex;

    private Vector3 mousePos;
    private Vector3 previewPos;
    private Vector3 previewRot;
    private Vector3 mOffest;
    private float zCoord;

    GameObject preview = null;

    bool previewActive;

    public GameObject currentObject;

    public LayerMask layer;

    public LayerMask layerMask;

    Renderer[] rend;
 public previewMaterials[] previewMaterials;

    public bool delete;
    private void Start()
    {
        layer = 1 << 12;
       
    }

    public void Delete()
    {
        if (delete)
        {
            delete = false;
        }
        else
            delete = true;
    }


    public void updatePrefab(int index)
    {
        delete = false;
        if (preview == null)
        {

            preview = Instantiate(objectPrefab[index]);
            rend = preview.GetComponentsInChildren<Renderer>();
            previewMaterials = preview.GetComponentsInChildren<previewMaterials>();
            prefabIndex = index;
        } else
        {
            Destroy(preview);
            saveData.testObjects.Remove(preview);

            preview = Instantiate(objectPrefab[index]);
            rend = preview.GetComponentsInChildren<Renderer>();
            previewMaterials = preview.GetComponentsInChildren<previewMaterials>();
            prefabIndex = index;
        }
    }




    private void Update()
    {

        MousePosition();
   
        if(delete)
        {
            layer = 1 << 10;
        } else
        {
            layer = 1 << 12;
        }

    }




    void PreviewPostion(Vector3 pos, Vector3 rot)
    {
       preview.transform.position = pos;


        preview.transform.localEulerAngles =  new Vector3(rot.x + 90,preview.transform.localEulerAngles.y, preview.transform.localEulerAngles.z);
    }

    void RenderPreview(string tag)
    {
        if (tag == "Node" || tag == "Untagged")
        {
            
            
            

            preview.transform.Find("Preview_Cube").GetComponent<previewVIEW>().UpdatePreview(1);

        }
        else
        {
         
        
    
            preview.transform.Find("Preview_Cube").GetComponent<previewVIEW>().UpdatePreview(0);
        }
    }



 




    private void MousePosition()
    {
        Ray ray;
        RaycastHit hit;

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 50, layer))
        {
            currentObject = hit.collider.gameObject;


            if (preview != null)
            {
                PreviewPostion(hit.transform.position, new Vector3(hit.collider.transform.eulerAngles.x, hit.collider.transform.eulerAngles.y, hit.collider.transform.eulerAngles.z));

                Debug.Log(hit.collider.tag);
                RenderPreview(hit.collider.tag);

                if (hit.collider.tag == "Node")
                {
                    PreviewPostion(hit.transform.position, new Vector3(hit.collider.transform.eulerAngles.x, hit.collider.transform.eulerAngles.y, hit.collider.transform.eulerAngles.z));


                    Node_Select node = hit.collider.GetComponent<Node_Select>();

                    GameObject go = null;






                    

                        if (Input.GetMouseButton(0))
                        {
                            go = Instantiate(objectPrefab[prefabIndex]) as GameObject;
                            go.transform.position = hit.collider.transform.position;
                            go.transform.localEulerAngles = new Vector3(hit.collider.transform.rotation.x + 60, hit.collider.transform.rotation.y, hit.collider.transform.rotation.z);
                            go.transform.Find("Preview_Cube").GetComponent<previewVIEW>().UpdatePreview(3);
                            go.transform.Find("Collider").GetComponent<BoxCollider>().enabled = true;
                            node.objectPlaced = true;
                            Destroy(preview);
                            saveData.testObjects.Remove(preview);

                        }
                        if (Input.GetMouseButton(1))
                        {
                            Destroy(preview);
                            saveData.testObjects.Remove(preview);
                        }

                   
                   
                }
                else
                    return;




            }


            if (delete)
            {


                if (currentObject.tag == "Slider" || currentObject.tag == "Button")
                {
                    if (Input.GetMouseButton(0))
                    {
                        Destroy(hit.collider.transform.parent.gameObject);
                        saveData.testObjects.Remove(hit.collider.transform.parent.gameObject);
                    }
                }
                else
                    return;
            }


        }
        else
            return;

    }



}
