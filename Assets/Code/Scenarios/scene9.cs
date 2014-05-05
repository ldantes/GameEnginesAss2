/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 9: The three jets are followed from behind, they are heading towards the core of the mothership.
 * An enemy droid ship takes out two of the ships.
 * 
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using BGE.States;

namespace BGE.Scenarios
{
    public class scene9 : Scenario
    {

        public override string Description()
        {
            return "Scene 9: 3 Fighters fly to destroy";
        }

        public override void Start()
        {
			DestroyObjectsWithTag("boid");// deletes previous instantiation of mothership.
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			GameObject alienCrafts = CreateBoid(new Vector3(620, 300,0) , boidPrefab);
             
			GameObject mothershipLazer = CreateBoid(new Vector3(620, 300,0), mothershipLazerPrefab);
			mothershipLazer.SetActive(true);
			mothershipLazer.light.range=50;
			mothershipLazer.light.intensity=8;
                    
            alienCrafts.GetComponent<SteeringBehaviours>().turnOffAll();
            
			alienCrafts.AddComponent<StateMachine>();
           	alienCrafts.GetComponent<StateMachine>().SwicthState(new IdleState(alienCrafts));
			alienCrafts.tag="enemy";
			alienCrafts.GetComponent<SteeringBehaviours>().attackTag="Player";
			alienCrafts.GetComponent<SteeringBehaviours>().flock ="enemy";
			alienCrafts.AddComponent<BoxCollider>();
			alienCrafts.AddComponent<Rigidbody>();
			alienCrafts.GetComponent<Rigidbody>().isKinematic= true;
			alienCrafts.GetComponent<BoxCollider>().isTrigger = true;
			alienCrafts.GetComponent<BoxCollider>().size =new Vector3(2,2,2);
			alienCrafts.AddComponent<OnCollideExplode>();
			alienCrafts.AddComponent<AudioSource>();
			alienCrafts.GetComponent<AudioSource>().maxDistance=1500;
			GameObject aleinTrail = CreateBoid(alienCrafts.transform.position,alienJetTrailPrefab);
			aleinTrail.AddComponent<SmokeTrail>();
			aleinTrail.GetComponent<SmokeTrail>().target =alienCrafts.transform;
			
			GameObject x = null;
			for(int i =0; i<3; i++)
			{
				x = new GameObject();
				x = InstantiateBoid(new Vector3(370, 300, 0-(i*10)), leaderPrefab, new Quaternion(0,0,0,0) );	
				x.AddComponent<SteeringBehaviours>();
				Path path = x.GetComponent<SteeringBehaviours>().path;
	            path.Waypoints.Add(new Vector3(370, 300, 0-(i*10)));
				path.Waypoints.Add(new Vector3(470,300,0-(i*10)));
				path.Waypoints.Add(new Vector3(500,300,0-(i*10)));
	            path.Waypoints.Add(new Vector3(550,350,0-(i*10)));
				
				x.tag = "Player";
				if (i==1)// central aly jet can not be destroyed
				{
					x.tag = "boid";
					CreateCamFollower(x,new Vector3(-10,1,-10));
					GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
					GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = x.transform;
				}
	            else
				{
					x.AddComponent<BoxCollider>();
					x.AddComponent<Rigidbody>();
					x.GetComponent<Rigidbody>().isKinematic= true;
					x.GetComponent<BoxCollider>().isTrigger = true;
					x.GetComponent<BoxCollider>().size =new Vector3(10,10,10);
					x.AddComponent<OnCollideExplode>();
					
				}
	            
	            path.draw = false;
	            x.GetComponent<SteeringBehaviours>().turnOffAll();
	            x.GetComponent<SteeringBehaviours>().FollowPathEnabled = true;
				
				x.AddComponent<AudioSource>();
				x.GetComponent<AudioSource>().maxDistance=1500;
	           	GameObject leaderTrail = CreateBoid(x.transform.position,jetTrailPrefab);
				leaderTrail.AddComponent<SmokeTrail>();
				leaderTrail.GetComponent<SmokeTrail>().target =x.transform;	
					
				
			}
			
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().s9SoundPrefab;
			asource.audio.PlayOneShot(sound);
        }
		
    }
}