using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{	
	private Vector3 cameraTarget;		// The camera's target. In this case the main character - NULL.
	private Transform target;			// This is the transform that holds NULL's position. Used in conjunction with cameraTarget.
	
	void Start () 
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;	// Find the player object.
	}
	
	void Update () 
	{
		cameraTarget = new Vector3(target.position.x ,transform.position.y ,target.position.z );	// Set the camera to the player's position, minus 10 on the z axis.
		transform.position = Vector3.Lerp(transform.position,cameraTarget,Time.deltaTime * 10.0f);		// Move the camera gracefully.
	}
}
