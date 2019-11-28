using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OscJack;

public class headTracker_Export : MonoBehaviour
{
	public string IPAddress = "127.0.0.1";
	public int MainOutPort = 9001;
	float sendFrequency = 0.01f;
	public float range = 50f; // ray length

	bool quat, standard, RollPitchYaw;

	OscClient client;

	// Start is called before the first frame update
	void Start()
	{
		quat = false;
		standard = false;
		RollPitchYaw = true;

		// Finds and loads in OSC settings
		client = new OscClient(IPAddress, MainOutPort);
		// Sends the head tracking data to SALTE audio renderer

		StartCoroutine(sendHTdata());

		Vector3 forward = transform.TransformDirection(Vector3.forward) * range;
		Debug.DrawRay(transform.position, forward, Color.green);
	}


	private void OnDestroy()
	{
		StopCoroutine(sendHTdata());
	}

	IEnumerator sendHTdata()
	{
		while (true)
		{
			if (quat)
			{
				// the quaternion output represents the rotation in the world's space, not the object's one - can't be used for ht
				client.Send("/rendering/quaternions", transform.rotation.w, transform.rotation.x, transform.rotation.y, transform.rotation.z);
			}

			if (standard)
			{
				client.Send("/rendering/htrpy", transform.localEulerAngles.z, transform.eulerAngles.x, transform.eulerAngles.y);
			}

			if (RollPitchYaw)
			{
				float roll = convertDegree(transform.localEulerAngles.z) * -1;
				float pitch = convertDegree(transform.localEulerAngles.x) * -1;
				float yaw = convertDegree(transform.localEulerAngles.y);

				client.Send("/rendering/htrpy", roll, pitch, yaw);
			}

			// wait before sending another OSC message
			yield return new WaitForSeconds(sendFrequency);
		}
	}


	private float convertDegree(float deg)
	{
		float angle = deg;

		if (deg > 180)
			angle -= 360;

		return angle;
	}
}
