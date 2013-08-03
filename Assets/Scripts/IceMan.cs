using UnityEngine;
using System.Collections;

public class IceMan : MonoBehaviour {
	
	enum IceCondition{IDLE, JUMP_UP, ATTACK, JUMP_DOWN};
	const float LeftWallBoundary = 100f;
	const float RightWallBoundary = 620f;

	IceCondition mCond = IceCondition.IDLE;
	const float LeftRightSpeed = 250f;
	const float LeftRightSpeedWhileJump = 100f;
	float jumpSpeed = 700f;
	const float jumpPrimitive = -10f;
	const float jumpInit = 700f;
	float inputTranslation = 0f;
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
			JumpDown();
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

	void JumpDown()
	{
		MoveLeftRight(LeftRightSpeedWhileJump);
		
		float transX = inputTranslation * Time.deltaTime;
		jumpSpeed += jumpPrimitive;
		float jumpTranslation = jumpSpeed * Time.deltaTime;
		transform.Translate(transX, jumpTranslation, 0);
	}

	void JumpUp()
	{
		MoveLeftRight(LeftRightSpeedWhileJump);
		
		float transX = inputTranslation * Time.deltaTime;
		jumpSpeed += jumpPrimitive;
		float jumpTranslation = jumpSpeed * Time.deltaTime;
		transform.Translate(transX, jumpTranslation, 0);
		
		if(jumpTranslation < 0)
			mCond = IceCondition.JUMP_DOWN;
		
		Ray ray = new Ray(transform.position, new Vector3(0f, 1f, 0f));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 50f))
		{
			hit.collider.gameObject.GetComponent<Brick>().StartFalling();
			mCond = IceCondition.JUMP_DOWN;
			jumpSpeed = 0;
		}

	}
	
	void OnTriggerEnter(Collider other) {
		print("onTriggerEnter");
		if(mCond == IceCondition.JUMP_DOWN)
		{
			mCond = IceCondition.IDLE;
			Vector3 newPos = transform.position;
			newPos.y = other.gameObject.transform.position.y + 67;
			transform.position = newPos;
		}
    }
	void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Vector3 directionDown = new Vector3(0f, -1f, 0f) * 50f;
		Vector3 directionUp = new Vector3(0f, 1f, 0f) * 50f;
        Gizmos.DrawRay(transform.position, directionDown);
        Gizmos.DrawRay(transform.position, directionUp);
    }
}
