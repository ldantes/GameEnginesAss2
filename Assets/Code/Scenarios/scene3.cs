/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 3: Watch the 3 jets each fire rocket at mothership, rockets explode.
 *
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


namespace BGE.Scenarios
{
    public class scene3 : Scenario
    {
		
		static Vector3 initialPos = Vector3.zero;
        public override string Description()
        {
            return "Scene 3: All jets fire at mothership";
        }
		
        public override void Start()
        {
			//previous scenario objects are destroyed
			SteeringManager.Instance().currentScenario.TearDown();
			
			//mothership is created
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			mothership.AddComponent<BoxCollider>();// boxcollider is added
			mothership.AddComponent<Rigidbody>();//rigid body is added
			mothership.GetComponent<Rigidbody>().isKinematic= true;
			mothership.GetComponent<BoxCollider>().isTrigger = true;
			mothership.GetComponent<BoxCollider>().size =new Vector3(1200,1200,1200);
			mothership.AddComponent<OnCollideExplode>();// when collision is trigger, objects explode
			mothership.GetComponent<OnCollideExplode>().destroyStruckObject = false;//mothership will not explode when missile collides
			mothership.AddComponent<AudioSource>();
			mothership.GetComponent<AudioSource>().maxDistance=1500;
			
			// three jets and missiles are created
			
			GameObject missile = null;
			for(int i =0; i<3; i++)
			{
				if(i==1)
				{
				leader = CreateBoid(new Vector3(-70, 430, -25-(i*10)), leaderPrefab);	//leader (middle) jet is created in front of others
				}
				else
				{
					leader = CreateBoid(new Vector3(-90, 430, -25-(i*10)), leaderPrefab);
				}
				//Jets each follow the same path, but are seperated
	            Path path = leader.GetComponent<SteeringBehaviours>().path;
	            leader.GetComponent<SteeringBehaviours>().path.Waypoints.Add(leader.transform.position);
	            path.Waypoints.Add(new Vector3(-40,430,-25-(i*10)));
				path.Waypoints.Add(new Vector3(-10,550,-25-(i*10)));
	            
	            
	            path.draw = false;
	            leader.GetComponent<SteeringBehaviours>().turnOffAll();
	            leader.GetComponent<SteeringBehaviours>().FollowPathEnabled = true;
	           	leader.tag ="Player";
                
				// jets have collision boxes applied and rigidbodies to register lazer/missile collission
				leader.AddComponent<BoxCollider>();
				leader.AddComponent<Rigidbody>();
				leader.GetComponent<Rigidbody>().isKinematic= true;
				leader.GetComponent<BoxCollider>().isTrigger = true;
				leader.GetComponent<BoxCollider>().size =new Vector3(5,5,5);
				leader.AddComponent<OnCollideExplode>();
				leader.AddComponent<AudioSource>();
				leader.GetComponent<AudioSource>().maxDistance=1500;
				GameObject leaderTrail = CreateBoid(leader.transform.position,jetTrailPrefab);
				leaderTrail.AddComponent<SmokeTrail>();
				leaderTrail.GetComponent<SmokeTrail>().target =leader.transform;	
					
				//missiles are created and are projected towards the mothership, where they will explode on collision
				missile = CreateBoid(leader.transform.position + new Vector3(50,-2,0), missilePrefab);
					
				missile.GetComponent<SteeringBehaviours>().SeekEnabled = true;
				missile.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(620,440,-25-(i*10));
				missile.GetComponent<SteeringBehaviours>().maxSpeed =10000.0f;
				
				missile.tag ="missile";
				missile.AddComponent<CapsuleCollider>();
				missile.GetComponent<CapsuleCollider>().isTrigger = true;
				missile.GetComponent<CapsuleCollider>().radius = 10;
				missile.GetComponent<CapsuleCollider>().height=10;
				missile.GetComponent<SteeringBehaviours>().maxSpeed =1000;
				GameObject missileTrail = CreateBoid(missile.transform.position -new Vector3(2,0,0),missileTrailPrefab);
				missileTrail.AddComponent<SmokeTrail>();
				missileTrail.GetComponent<SmokeTrail>().target =missile.transform;		
				
			}	
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().s3SoundPrefab;
			asource.audio.PlayOneShot(sound);
			
			//camera watches jets and missiles head towards mothership, from behind and to the side
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(-100,450,-100);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = missile.transform;
			
			
        }
		
    }
}