/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 6: camera follows three fight jets which emerge from the battle, they are heading below the mothership.
  * 
  * 
 * The full battle is generated, enemy drones and american jets are spawned randomly and have state machines assigned to engage them in combat if criteria is met
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
    public class scene6 : Scenario
    {
		private PathFinder pathFinder;
		Vector3 targetPos;
        Vector3 startPos;
        public override string Description()
        {
            return "scene 6: fighter jets emerge from conflict";
        }

        public override void Start()
        {
				SteeringManager.Instance().currentScenario.TearDown();
			
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			
            leader = InstantiateBoid(new Vector3(-70,200, -10), leaderPrefab, Quaternion.Euler(0, 0, 0) );
					       
            leader.GetComponent<SteeringBehaviours>().turnOffAll();
            leader.GetComponent<SteeringBehaviours>().SeekEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
			leader.GetComponent<SteeringBehaviours>().flock = "boid";
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = mothership.transform.position ;
			leader.tag = "boid";
			GameObject leaderTrail = CreateBoid(leader.transform.position,jetTrailPrefab);
			leaderTrail.AddComponent<SmokeTrail>();
			leaderTrail.GetComponent<SmokeTrail>().target =leader.transform;
			
			GameObject aly =null;
			for (int i = 0; i < 2; i++)
            {
                 
				aly = InstantiateBoid(new Vector3(-80, 200, -25*i), leaderPrefab, Quaternion.Euler(0, 0, 0) );
				aly.GetComponent<SteeringBehaviours>().leader = leader;
				aly.GetComponent<SteeringBehaviours>().offset = new Vector3(-5, 0, 10);
				aly.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;	
				aly.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
                aly.tag ="Player";
                leader.GetComponent<SteeringBehaviours>().flock = "Player";
				GameObject alyTrail = CreateBoid(aly.transform.position,jetTrailPrefab);
				
				alyTrail.AddComponent<SmokeTrail>();
				alyTrail.GetComponent<SmokeTrail>().target =aly.transform;
                
            }
			
			GameObject.FindGameObjectWithTag("MainCamera").transform.position =  new Vector3(-50,200,20);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = leader.transform;
			
			int fleetSize = 10;
           	GameObject alienCrafts =null;
			float xOff = -5;
            float zOff = -10;
			for (int i = 0; i < fleetSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float z = (i - 1) * zOff;
                    Vector3 offset = new Vector3(-15,15,-15);
					
					leader = CreateBoid(new Vector3(Utilities.RandomClamped(0,1000), 100, Utilities.RandomClamped(-500,500)) , leaderPrefab);
					leader.GetComponent<SteeringBehaviours>().turnOffAll();
					leader.GetComponent<SteeringBehaviours>().offset = offset;
					leader.AddComponent<StateMachine>();
           			leader.GetComponent<StateMachine>().SwicthState(new IdleState(leader));
					
					leader.tag="Player";
					leader.GetComponent<SteeringBehaviours>().flock ="Player";
					leader.GetComponent<SteeringBehaviours>().attackTag="enemy";
					leader.AddComponent<BoxCollider>();
					leader.AddComponent<Rigidbody>();
					leader.GetComponent<Rigidbody>().isKinematic= true;
					leader.GetComponent<BoxCollider>().isTrigger = true;
					leader.GetComponent<BoxCollider>().size =new Vector3(5,2,5);
					leader.AddComponent<OnCollideExplode>();
					leader.AddComponent<AudioSource>();
					leader.GetComponent<AudioSource>().maxDistance=1500;
					leaderTrail = CreateBoid(leader.transform.position,jetTrailPrefab);
					leaderTrail.AddComponent<SmokeTrail>();
					leaderTrail.GetComponent<SmokeTrail>().target =leader.transform;
					
					alienCrafts = CreateBoid(new Vector3(Utilities.RandomClamped(0,1000), 100, Utilities.RandomClamped(-500,500)) , boidPrefab);
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
					alienCrafts.GetComponent<BoxCollider>().size =new Vector3(2,2,2);
					alienCrafts.AddComponent<OnCollideExplode>();
					GameObject aleinTrail = CreateBoid(alienCrafts.transform.position,alienJetTrailPrefab);
					aleinTrail.AddComponent<SmokeTrail>();
					aleinTrail.GetComponent<SmokeTrail>().target =alienCrafts.transform;
					alienCrafts.AddComponent<AudioSource>();
					alienCrafts.GetComponent<AudioSource>().maxDistance=1500;
                }
            }
			
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().s6SoundPrefab;
			asource.audio.PlayOneShot(sound);
			
            GroundEnabled(false);
        }
		
    }
}