using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using Valve.VR;

public class Pointer : MonoBehaviour
{

    public float m_distance = 10f;
    public LineRenderer m_LineRenderer = null;
    public LayerMask m_EverythingMask = 0;
    public LayerMask m_InteractableMask = 0;

    public Transform[] m_controllerPoints = null;
    public Transform  m_CurrentOrigin = null;

    public UnityAction<Vector3, GameObject> OnPointerUpdate = null;
    public GameObject currentObject = null;

    public string tag = null;

    public bool isSlider = false;
    public bool isPlayer = false;

    public GameObject dot;




    public SteamVR_ActionSet actionSet;
    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean buttonPress;
    public SteamVR_Action_Vector2 joystick;

    public TextMeshPro oject;

    float y;
    private void Start()
    { 
        SetLineColour(); // set colour on start

        actionSet.Activate(SteamVR_Input_Sources.Any, 0, true);

      
    }

    private void Update()
    {
       Vector3 hitpoint =  UpdateLine(); // updates the hitpint of the raycast

       currentObject = UpdatePointerStatus(); // returns gameobject the pointer is hitting

        if (currentObject != null)
        {
        //    oject.text = currentObject.name;
            dot.SetActive(true);
            checkTAG(currentObject); //checks the tag of the current object 
        } else
        {
            dot.SetActive(false);
            isSlider = false;
            isPlayer = false;
        }
        if (OnPointerUpdate != null)
            OnPointerUpdate(hitpoint, currentObject);
   VRInput();

    }

    void VRInput()
    {

        Vector2 delta = joystick[SteamVR_Input_Sources.RightHand].delta;
     

        if (currentObject != null) {
            // Trigger input 
            if (buttonPress.GetStateDown(SteamVR_Input_Sources.Any))
            {
                Debug.Log("button pressed");

                // Transport Buttons
                if (currentObject.tag == "Play")
                {
                    var button = currentObject.GetComponent<ButtonSendOSC>();
                    button.sendData("play");
                  
                }
             else if (currentObject.tag == "Stop")
                {
                    var button = currentObject.GetComponent<ButtonSendOSC>();
                    button.sendData("stop");

                }
             else if(currentObject.tag == "Loop_3G")
                {
                    var button = currentObject.GetComponent<ButtonSendOSC>();
                    button.sendData("loop");
                }
                // Slider Snap
            else if (currentObject.tag == "Snap")
                {
                    Vector3 pos = new Vector3(0, 0, 0);
                    pos = currentObject.transform.position;
                    var snap = currentObject.GetComponent<highlightSnap>();
                    snap.snapPostion(pos);
                }
                else if (currentObject.tag == "Next_3G")
                {
                    var next = currentObject.GetComponent<next3G>();
                    next.nextStimulus();
                }


                else if(currentObject.tag == "Button")
                {
                    var button = currentObject.GetComponent<H_button>();
                    button.sendData();
                }

                else
                    return;


            }



           
                if (currentObject.tag == "Slider_Cont3G")
                {
             
                   var slider = currentObject.GetComponent<sliderScale>();
                   slider.scaleAmount();

                var oscSend = currentObject.GetComponent<H_slider>();
                oscSend.updateValue();
               oscSend.sendData();

            }






        }
        else
            return;
    }



    private void Awake()
    {
        PlayerEvents.OnTouchpadDwn += ProcessTouchPadDown;
    }

    private void OnDestroy()
    {
        PlayerEvents.OnTouchpadDwn -= ProcessTouchPadDown; 
    }

   

   

    private Vector3 UpdateLine ()
    {

        // Create the ray 
        RaycastHit hit = CreateRaycast(m_EverythingMask);

        // set the default end of the ray
        Vector3 endPosition = m_CurrentOrigin.position + (m_CurrentOrigin.forward * m_distance); 

        // check hit 
        if(hit.collider != null)
        {
            endPosition = hit.point;
            dot.transform.position = endPosition;
        }

        // set positions of the line renderer
        m_LineRenderer.SetPosition(0, m_CurrentOrigin.position);
        m_LineRenderer.SetPosition(1, endPosition);




            return endPosition;
    }

    public void UpdateOrigin(int hand)
    {

        // Set origin of pointer 
        if (hand == 0)
        {
            m_CurrentOrigin = m_controllerPoints[0];
        }
        else
            m_CurrentOrigin = m_controllerPoints[1];

    }

    private void checkTAG(GameObject obj)
    {

        string tag = obj.tag;

        switch (tag)
        {
            case "Slider":
                isSlider = true;
                sliderSelect scale = currentObject.GetComponent<sliderSelect>();
             
                break;
            case "Player":
                isPlayer = true;
                break;
            default:
                isSlider = false;
                isPlayer = false;
                break;

        }


       
    }

    private GameObject UpdatePointerStatus()
    {
        // Create a Ray 
        RaycastHit hit = CreateRaycast(m_InteractableMask);




        // Check the hit 
        if (hit.collider)
        {
            GameObject obj = hit.collider.gameObject;
       
         


             return hit.collider.gameObject;
        }
        

    
        // Return
        return null;

    }

    private void clearSelectionUI()
    {
        if (currentObject == null)
            return;

        isSlider = false;
        currentObject = null;
    }

    private void processUI(GameObject obj)
    {
        if(currentObject != null)
        {
            if(obj == currentObject)            
                return;

                clearSelectionUI();
        }

        currentObject = obj;

        Interacable inter = currentObject.GetComponent<Interacable>();
        inter.increase(); 
        

    }



    // Creates Raycast for the pinter
    private RaycastHit CreateRaycast(int layer)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_CurrentOrigin.position, m_CurrentOrigin.forward);
        Physics.Raycast(ray, out hit, m_distance, layer);
        return hit;

    }

    // Sets the colour of the pointer
    private void SetLineColour()
    {
        if (!m_LineRenderer)
            return;

        Color endColour = Color.yellow;
        endColour.a = 0.0f;

        m_LineRenderer.endColor = endColour;

    }


   public void ProcessTouchPadDown()
    {
        if (!currentObject)
            return;

        Interacable interacable = currentObject.GetComponent<Interacable>();
        interacable.Pressed();

    }


}
