/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 4: Enemy drones emerge from the explosion smoke and attach the aly jets
 * 
 *
*/

using System;
using System.Collections.Generic;
using System.Linq;
using BGE.States;
using System.Text;
using UnityEngine;

namespace BGE.Scenarios
{
    public class scene4 : Scenario
    {
		static Vector3 initialPos = Vector3.zero;
        public override string Description()
        {
            return "Scene 4: Enemey drone ships emerge from mothership";
        }

        public override void Start()
        {
			//scene 3 game objects remain
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(-120,410,-80);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject camerapos = new GameObject();
            camerapos.transform.position =GameObject.FindGameObjectWithTag("MainCamera").transform.position;
			
			//enemy fleet is created
			int fleetSize = 5;
           	GameObject alienCrafts =null;
			float xOff = 10;
            float zOff = -25;
			for (int i = 2; i < fleetSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float z = (i - 1) * zOff;
                    Vector3 offset = new Vector3(5,5,5);
					//explosions are created on mothership, from which the enemy crafts emerge from the smoke
					GameObject explosion = new GameObject();
					explosion.AddComponent<Detonator>();
					explosion.transform.position = new Vector3(0, 440, i*zOff);
					explosion.GetComponent<Detonator>().size =20.0f;
					explosion.GetComponent<Detonator>().duration =5.0f;
					alienCrafts = CreateBoid(new Vector3(i*xOff, 450+(i*j), i*zOff) , boidPrefab);
                    
					
                    camerapos.tag="Player"; // an object is created near the camera tagged as Player, which is a targeted enemy tag for the enemy drones.
					alienCrafts.GetComponent<SteeringBehaviours>().turnOffAll();
                    alienCrafts.GetComponent<SteeringBehaviours>().offset = offset;
					alienCrafts.GetComponent<SteeringBehaviours>().RandomWalkEnabled =true;
					alienCrafts.AddComponent<StateMachine>(); // state machine component added to enemy drones
           			alienCrafts.GetComponent<StateMachine>().SwicthState(new IdleState(alienCrafts));//idle is assigned (only idle and attack are used)
					alienCrafts.tag="enemy";
					alienCrafts.GetComponent<SteeringBehaviours>().attackTag="Player";// enemy attack tag ="Player"
					alienCrafts.GetComponent<SteeringBehaviours>().flock ="enemy";// flocks with all gameobjects tagged = "enemy"
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
			AudioClip sound =   SteeringManager.Instance().s4SoundPrefab;
			asource.audio.PlayOneShot(sound);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = alienCrafts.transform;
            
        }
		
    }
}