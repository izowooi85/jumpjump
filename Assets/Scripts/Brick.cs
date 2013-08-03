using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(rigidbody.useGravity)
		{
			transform.Rotate(new Vector3(0f, 0f, 20f));
		}
	}
	public void StartFalling()
	{
		Vector3 nowPos = transform.position;
		nowPos.z = -8;
		transform.position = nowPos;
		rigidbody.useGravity = true;
		rigidbody.AddForce(Vector3.up * 30f, ForceMode.Impulse);
	}
	
}
