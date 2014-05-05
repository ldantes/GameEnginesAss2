/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 10: amongst the chaos , a single fighter jet flys into the core of the mothership, this explodes the entire mothership.
 * The explosions are done by timing, not collision.
 * 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.Scenarios
{
    public class scene10 : Scenario
    {

        public override string Description()
        {
            return "Scene 10: Redkneck destruction";
        }

        public override void Start()
        {
			//SteeringManager.Instance().currentScenario.TearDown();
			DestroyObjectsWithTag("boid");
			SteeringManager.Instance().camFollowing=false;
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			mothership.AddComponent<Detonator>();
			mothership.GetComponent<Detonator>().explodeOnStart = false;
			mothership.GetComponent<Detonator>().Invoke("Explode",7);
			mothership.GetComponent<Detonator>().size=1000;
			mothership.GetComponent<Detonator>().destroyTime =5.0f;
			
			
			GameObject explosion =new GameObject();
			explosion.transform.position =mothership.transform.position;
			explosion.AddComponent<Detonator>();
			explosion.GetComponent<Detonator>().explodeOnStart = false;
			explosion.GetComponent<Detonator>().Invoke("Explode",8);
			explosion.GetComponent<Detonator>().Invoke("Explode",9);
			explosion.GetComponent<Detonator>().autoCreateFireball =true;
			//mothership.transform.localScale = new Vector3(0,0,0);
			explosion.GetComponent<Detonator>().duration = 15.0f;
			explosion.GetComponent<Detonator>().size=1000;
			explosion.GetComponent<Detonator>().destroyTime =10.0f;
			
			
			leader = InstantiateBoid(new Vector3(650, 300, -50), leaderPrefab, Quaternion.Euler(0, 0, 0) );
			leader.AddComponent<Detonator>();	
			leader.GetComponent<Detonator>().explodeOnStart = false;
			leader.GetComponent<Detonator>().Invoke("Explode",8);
			leader.GetComponent<Detonator>().destroyTime =0.5f;
            leader.GetComponent<SteeringBehaviours>().turnOffAll();
            leader.GetComponent<SteeringBehaviours>().SeekEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = mothership.transform.position+new Vector3(0, 100,0) ;
			leader.tag = "Player";
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(100,300,0);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = leader.transform;
			
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().rnfSoundPrefab;
			asource.audio.PlayOneShot(sound);
			
			GameObject leaderTrail = CreateBoid(leader.transform.position,jetTrailPrefab);
			leaderTrail.AddComponent<SmokeTrail>();
			leaderTrail.GetComponent<SmokeTrail>().target =leader.transform;
			
			GameObject mothershipLazer = CreateBoid(new Vector3(620, 300,0), mothershipLazerPrefab);
			mothershipLazer.SetActive(true);
			mothershipLazer.light.range=50;
			mothershipLazer.light.intensity=8;
			
			
			
			
			
        }
		
	}
}