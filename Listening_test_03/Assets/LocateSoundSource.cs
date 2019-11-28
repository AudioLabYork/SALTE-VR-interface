using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using System.IO;

// show sound source in editor window (but not in the game)
public class LocateSoundSource : MonoBehaviour {

    //declare variables
    private float azi_rad; // azimuth angle in radian
    private float ele_rad; // elevation angel in radian
    public float azi_rela;
    public float ele_rela;

    [HideInInspector]
    GameObject CenterEyeAnchor;
    OSCData player;  // call the OSCData script (to get the angle of the sound source)
    [HideInInspector]
    GameObject OVRPlayerController;
    Transform playerPosition;  // call the transform component in OVRPlayerController (to get the player position)

    // Use this for initialization
    void Start () {
        // find ODC Data script
        CenterEyeAnchor = GameObject.Find("CenterEyeAnchor");
        player = CenterEyeAnchor.GetComponent<OSCData>();
        // find transform component in OVRPlayerContoller
        OVRPlayerController = GameObject.Find("OVRPlayerController");
        playerPosition = OVRPlayerController.GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {

        azi_rela = player.targetAzimuth; // declare azi_rela as sound source azimuth angle
        ele_rela = player.targetElevation; // declare ele_rela as sound source elevation angle
        // change the azimuth angle form between -180 and 180, to between 0 -360
        if (azi_rela >= 0)
        {
            azi_rela = azi_rela;
        }
        else if (azi_rela < 0)
        {
            azi_rela = azi_rela + 360;
        }
        // add the anchor angle (anchor is the player start position when trigger a sample so the start postion is always 0)
        azi_rela = (azi_rela + player.azi_anchor) % 360;
        ele_rela = (ele_rela + player.ele_anchor) % 360;

        // convert angle form degree to radian
        azi_rad = azi_rela * Mathf.Deg2Rad;
        ele_rad = ele_rela * Mathf.Deg2Rad;
        // convert angle to cartiesian vector
        Vector3 Cart = SphericalToCartesian(5, azi_rad, ele_rad);
        // add the sound source cartiesian vector to the player current position, which will be the sound source position in the game
        transform.position = playerPosition.transform.position + Cart;
    }

    // convery angle (in radian) to cartiesian vector
    public Vector3 SphericalToCartesian(float radius, float polar, float elevation)
    {
        float a = radius * Mathf.Cos(elevation);
        float z = a * Mathf.Cos(polar);
        float y = radius * Mathf.Sin(elevation);
        float x = a * Mathf.Sin(polar);
        return new Vector3(x, y, z);
    }

}
