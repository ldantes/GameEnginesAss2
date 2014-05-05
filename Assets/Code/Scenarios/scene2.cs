/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 2: Has a side view of two aly jets joining a leader jet.
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
    public class scene2 : Scenario
    {
		private PathFinder pathFinder;
		Vector3 targetPos;
        Vector3 startPos;
        public override string Description()
        {
            return "Scene 2: Side on view of ally jets joining single jet";
        }

        public override void Start()
        {
			//The previous game objects are cleared.
			SteeringManager.Instance().currentScenario.TearDown();
			
			//The mothership is created
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			
			//Leader jet is created
            leader = InstantiateBoid(new Vector3(-300, 400, -25), leaderPrefab, Quaternion.Euler(0, 0, 0) );
			leader.GetComponent<SteeringBehaviours>().turnOffAll();
            leader.GetComponent<SteeringBehaviours>().SeekEnabled = true; // Seek is enabled
			leader.GetComponent<SteeringBehaviours>().SeparationEnabled = true; //an altered seperation is enabled, the distance of seperation has been increased to cater for lager models.
			leader.GetComponent<SteeringBehaviours>().flock = "Player"; // flock attribute represents the gameobjects flocking friends
			leader.GetComponent<SteeringBehaviours>().seekTargetPos = mothership.transform.position + new Vector3(0, 225, 0);
			leader.tag = "Player"; 
			
			// a smoke trail is added to the back gameobject
			GameObject leaderTrail = CreateBoid(leader.transform.position,jetTrailPrefab);
			leaderTrail.AddComponent<SmokeTrail>();
			leaderTrail.GetComponent<SmokeTrail>().target =leader.transform;
			
			//2 aly jets are instantiated 
			GameObject aly =null;
			for (int i = 0; i < 2; i++)
            {
                 
				aly = InstantiateBoid(new Vector3(-300, 410, -25*i), leaderPrefab, Quaternion.Euler(0, 0, 0) );
				aly.GetComponent<SteeringBehaviours>().leader = leader;
				aly.GetComponent<SteeringBehaviours>().offset = new Vector3(-5, 0, 10);
				aly.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;	// off set is enabled, to pursue leader jet
				aly.GetComponent<SteeringBehaviours>().SeparationEnabled = true; // seperation is enabled to keep 'flocking' models seperated
                aly.tag ="Player";
                
				
				aly.GetComponent<SteeringBehaviours>().flock = "Player"; //flocking association tag
				GameObject alyTrail = CreateBoid(aly.transform.position,jetTrailPrefab); // smoke trail is added
				
				alyTrail.AddComponent<SmokeTrail>();
				alyTrail.GetComponent<SmokeTrail>().target =aly.transform;
                
            }
			// movie audio is added
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().fireAtWillSoundPrefab;
			asource.audio.PlayOneShot(sound);
			
			//camera is set view jets from a side view
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = leader.transform.position + new Vector3(50,0,50);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = leader.transform;
			
        }
    }
}