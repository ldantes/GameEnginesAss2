/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 5: Big dog fight in the sky
 * 
 * This scene is an overview look of the enmy drones and thew aly ships in combat.
 * 
 * all fleets are assigned a 'flock' tag which represents their aly game objects and an 'Attacktag' which represents the enemy game objects.
  * These tags are used both in the steering behaviours and in the state machine classes.
  * 
  * Each of the gameobjects in combat have colliders attached to them.
  * 
  * 
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
    public class scene5 : Scenario
    {
		private PathFinder pathFinder;
		Vector3 targetPos;
        Vector3 startPos;
        public override string Description()
        {
            return "scene 5: Dog fight";
        }

        public override void Start()
        {
			SteeringManager.Instance().currentScenario.TearDown();
			
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			
			//camera looks down upon enemy drones and aly jets in air combat
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(0,550,80);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
		
			//The fleets are created for the aly and enemy drones
			int fleetSize = 6;
           	GameObject alienCrafts =null;
			float xOff = -30;
            float zOff = -50;
			for (int i = 2; i < fleetSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float z = (i - 1) * zOff;
                    Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), 20, z);
                    GameObject leader = CreateBoid(new Vector3(-200,450,10) + offset, leaderPrefab);
					leader.GetComponent<SteeringBehaviours>().turnOffAll();
					leader.GetComponent<SteeringBehaviours>().offset = offset;
					leader.AddComponent<StateMachine>();
           			leader.GetComponent<StateMachine>().SwicthState(new IdleState(leader));
					leader.tag="Player";
					leader.GetComponent<SteeringBehaviours>().flock ="Player"; //flock tags - used by steering behaviour
					leader.GetComponent<SteeringBehaviours>().attackTag="enemy";//attack tag used by steering behaviour and state machine
					leader.AddComponent<BoxCollider>();
					leader.AddComponent<Rigidbody>();
					leader.GetComponent<Rigidbody>().isKinematic= true;
					leader.GetComponent<BoxCollider>().isTrigger = true;
					leader.GetComponent<BoxCollider>().size =new Vector3(5,2,5);
					leader.AddComponent<OnCollideExplode>();
					leader.AddComponent<AudioSource>();
					leader.GetComponent<AudioSource>().maxDistance=1500;
					GameObject leaderTrail = CreateBoid(leader.transform.position,jetTrailPrefab);
					leaderTrail.AddComponent<SmokeTrail>();
					leaderTrail.GetComponent<SmokeTrail>().target =leader.transform;
					
					
					//create enemy drone ships
					alienCrafts = CreateBoid(new Vector3(i*xOff+offset.x, 450+(i*j)+offset.y, i*zOff+offset.z) , boidPrefab);
                    alienCrafts.GetComponent<SteeringBehaviours>().turnOffAll();
                    alienCrafts.GetComponent<SteeringBehaviours>().offset = offset;
					alienCrafts.AddComponent<StateMachine>();
           			alienCrafts.GetComponent<StateMachine>().SwicthState(new IdleState(alienCrafts));
					alienCrafts.tag="enemy";
					alienCrafts.GetComponent<SteeringBehaviours>().attackTag="Player";
					alienCrafts.GetComponent<SteeringBehaviours>().flock ="enemy";
					alienCrafts.AddComponent<BoxCollider>();
					alienCrafts.AddComponent<Rigidbody>();
					alienCrafts.GetComponent<Rigidbody>().isKinematic= true;
					alienCrafts.GetComponent<BoxCollider>().isTrigger = true;
					alienCrafts.GetComponent<BoxCollider>().size =new Vector3(5,5,5);
					alienCrafts.AddComponent<OnCollideExplode>();
					alienCrafts.AddComponent<AudioSource>();
					alienCrafts.GetComponent<AudioSource>().maxDistance=1500;
					GameObject aleinTrail = CreateBoid(alienCrafts.transform.position,alienJetTrailPrefab);
					aleinTrail.AddComponent<SmokeTrail>();
					aleinTrail.GetComponent<SmokeTrail>().target =alienCrafts.transform;
					
                }
            }
			
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().s5SoundPrefab;
			asource.audio.PlayOneShot(sound);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = alienCrafts.transform;
           
        }
		
    }
}