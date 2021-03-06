using UnityEngine;
using System.Collections;
using BGE;
 
public class OnCollideExplode : MonoBehaviour 
{

	public GameObject explosion;// Use this for initialization
	public bool destroyStruckObject =true;
	public AudioClip sound =   SteeringManager.Instance().explosionSoundPrefab;
	
	void Start () 
	{
		explosion = new GameObject();
		explosion.AddComponent<Detonator>();
		explosion.GetComponent<Detonator>().explodeOnStart =false;
		explosion.GetComponent<Detonator>().size =5.0f;
		explosion.GetComponent<Detonator>().duration =2.0f;
		
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		explosion.transform.position =transform.position;
	}

	
	void OnTriggerEnter(Collider missile)
	{
		if(missile.tag == "missile")
		{
			explosion.GetComponent<Detonator>().Explode();
			explosion = new GameObject();
			explosion.AddComponent<Detonator>();
			explosion.GetComponent<Detonator>().transform.position=missile.transform.position;
			
			audio.PlayOneShot(sound);
			//AudioSource().clip = sound;
			Destroy (missile);
			if(destroyStruckObject==true)
			{
				gameObject.SetActive(false);
				
			}
		}
	}
 
} 
 
