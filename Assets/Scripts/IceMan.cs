using UnityEngine;
using System.Collections;
/*
 * 130803 : Refactoring.
 * 130801 : File generation.
 * */
public class IceMan : MonoBehaviour {
	enum IceCondition{IDLE, JUMP_UP, ATTACK, JUMP_DOWN};
	IceCondition mCond = IceCondition.IDLE;
	const float LeftWallBoundary = 100f;
	const float RightWallBoundary = 620f;
	const float LeftRightSpeed = 250f;
	const float LeftRightSpeedWhileJump = 100f;
	const float jumpPrimitive = -10f;
	const float jumpInit = 700f;
	const float RaySize = 50f;
	float inputTranslation = 0f;
	float jumpSpeed = 700f;
	const float interpolateHeight = 22f + 45f;
	void Start()
	{
		Vector3 initPosition = transform.position;
		initPosition.y = 129;
		transform.position = initPosition;
	}
    	void Update() 
	{
		switch(mCond)
		{
		case IceCondition.IDLE:
			IdleAction();
			break;
		case IceCondition.JUMP_UP:
			JumpUp();
			break;
		case IceCondition.JUMP_DOWN:
			JumpUpdate();
			break;
		default:
			break;
		}
    	}
	bool JumpKeyPressed()
	{
		if(Input.GetKeyDown(KeyCode.Z))
		{
			mCond = IceCondition.JUMP_UP;
			jumpSpeed = jumpInit;
			return true;
		}
		return false;
		
	}
	void MoveLeftRight(float speedParam)
	{
		float translation = Input.GetAxis("Horizontal") * speedParam;
		inputTranslation = translation;
		translation *= Time.deltaTime;
		transform.Translate(translation, 0, 0);
	
	}
	bool isEmptyBottom()
	{
	
		Ray ray = new Ray(transform.position, new Vector3(0f, -1f, 0f));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 50f))
		{
			return false;
		}
		else
		{
			return true;
		}
	
	}
	void CheckWall()
	{
		Vector3 pos = transform.position;
		if(pos.x < LeftWallBoundary)
		{
			pos.x = LeftWallBoundary;
			transform.position = pos;
		}
		if(pos.x > RightWallBoundary)
		{
			pos.x = RightWallBoundary;
			transform.position = pos;
		}
	}
	void IdleAction()
	{
		if(!JumpKeyPressed())
		{
			MoveLeftRight(LeftRightSpeed);
			CheckWall();
			if(isEmptyBottom())
				mCond = IceCondition.JUMP_DOWN;
		}
	}
	float JumpUpdate()
	{
		MoveLeftRight(LeftRightSpeedWhileJump);
		
		float transX = inputTranslation * Time.deltaTime;
		jumpSpeed += jumpPrimitive;
		float jumpTranslation = jumpSpeed * Time.deltaTime;
		transform.Translate(transX, jumpTranslation, 0);
		return jumpTranslation;
	}
	void JumpUp()
	{
		float deltaY = JumpUpdate();
		
		if(jumpSpeed < 0)
			mCond = IceCondition.JUMP_DOWN;
		else
		{
			Ray ray = new Ray(transform.position, Vector3.up);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, RaySize))
			{
				hit.collider.gameObject.GetComponent<Brick>().StartFalling();
				mCond = IceCondition.JUMP_DOWN;
				jumpSpeed = 0;
				transform.Translate(0, -deltaY, 0);
			}
		}

	}
	void OnTriggerEnter(Collider other) 
	{
		if(mCond == IceCondition.JUMP_DOWN)
		{
			mCond = IceCondition.IDLE;
			Vector3 newPos = transform.position;
			newPos.y = other.gameObject.transform.position.y + interpolateHeight;
			transform.position = newPos;
		}
    	}
	void OnDrawGizmosSelected() 
	{
        	Gizmos.color = Color.red;
        	Vector3 directionDown = new Vector3(0f, -1f, 0f) * RaySize;
		Vector3 directionUp = new Vector3(0f, 1f, 0f) * RaySize;
        	Gizmos.DrawRay(transform.position, directionDown);
        	Gizmos.DrawRay(transform.position, directionUp);
    	}
}
