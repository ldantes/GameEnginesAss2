using UnityEngine;
using System.Collections;
 
public class SmokeTrail : MonoBehaviour {
    //public GameObject smoketrail = new GameObject();
	public Transform target;
 
	void start()
	{
		//smoketrail = GameObject.FindGameObjectWithTag("smoke");
		//smoketrail.SetActive(true);
		
	}
    void Update()
	{
		transform.position =target.position;
	}
}

