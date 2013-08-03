using UnityEngine;
using System.Collections;

public class IceManPhysics : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Z))
		{
			rigidbody.AddForce(new Vector3(0f, 1f, 0f) * 500f , ForceMode.VelocityChange);
		}
		float speed = Input.GetAxis("Horizontal");
		rigidbody.AddForce(new Vector3(speed, 0f, 0f) * 2000f, ForceMode.Force);
	}
}
