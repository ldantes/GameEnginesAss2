using UnityEngine;
using System.Collections;

public class MissileCollision : MonoBehaviour 
{
	public bool explode = false;
	GameObject explosion;// Use this for initialization
	
	void Start () 
	{
		explosion.AddComponent<Detonator>();
		explosion.GetComponent<Detonator>().explodeOnStart =false;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	
	void OnTriggerEnter(Collider theEnterer)
	{
		if(theEnterer.tag == "missile")
		{
			gameObject.SetActive(false);
			explosion.GetComponent<Detonator>().Explode();
		}
	}
}
