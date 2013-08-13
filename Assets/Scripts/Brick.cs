using UnityEngine;
using System.Collections;

/*
 * 130813 : code review test.
 * 130803 : Refactoring.
 * 130801 : File generation. Fallig function Add.
 * */

public class Brick : MonoBehaviour {
	const float RotationSpeed = 20f;
	const float FallingDepth = -8f;
	const float ForceUpVector = 30f;
	const float TimeUntilDestory = 3f;
	void Start () {
	
	}

	void Update () {
		if(rigidbody.useGravity)
		{
			transform.Rotate(new Vector3(0f, 0f, RotationSpeed));
		}
	}
	
	public void StartFalling()
	{
		//Move to Depth.
		Vector3 currentPosition = transform.position;
		currentPosition.z = FallingDepth;
		transform.position = currentPosition;
		//Gravity Enable.
		rigidbody.useGravity = true;
		rigidbody.AddForce(Vector3.up * ForceUpVector, ForceMode.Impulse);
		Destroy(gameObject, TimeUntilDestory);
	}
	
}
