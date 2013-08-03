using UnityEngine;
using System.Collections;

/*
 * 130801 : File generation.
 * */
public class GameManager : MonoBehaviour {
	
	public GameObject block;
	void Start () {
		for(int i = 0 ; i < 25 ; i++)
		{
			Instantiate(block, new Vector3(99f + 22 * i, 62f, 0f), Quaternion.identity); 
		}
		for(int i = 0 ; i < 10 ; i++)
		{
			Instantiate(block, new Vector3(99f + 22 * i, 331f, 0f), Quaternion.identity); 
		}
	}
	void Update () {
	
	}
}
