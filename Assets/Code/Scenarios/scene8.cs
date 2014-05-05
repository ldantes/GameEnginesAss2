/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 8: The three jets are seen from a front on view, flying underneath the mothership, they all veer left.
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
    public class scene8 : Scenario
    {
		
        public override string Description()
        {
            return "Scene 8: Three jets fly underneath mothership";
        }

        public override void Start()
        {
			
			DestroyObjectsWithTag("boid");
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			GameObject pos = new GameObject();
			pos.transform.position =new Vector3(400, 300, -20);
            GameObject x = null;
			for(int i =0; i<3; i++)
			{
				x = new GameObject();
				x = InstantiateBoid(new Vector3(400, 300, 0-(i*10)), leaderPrefab, new Quaternion(0,0,0,0) );	
				x.AddComponent<SteeringBehaviours>();
				//path following is used, the path waypoints get closer towards the mothership centre.
				Path path = x.GetComponent<SteeringBehaviours>().path;
	            path.Waypoints.Add(new Vector3(400, 300, 0-(i*10)));
				path.Waypoints.Add(new Vector3(470,300,0-(i*10)));
	            path.Waypoints.Add(new Vector3(500,300,0-(i*10)));
				path.Waypoints.Add(new Vector3(550,270,80-(i*10)));//then decend and veer left
	            path.draw = false;
	            x.GetComponent<SteeringBehaviours>().turnOffAll();
	            x.GetComponent<SteeringBehaviours>().FollowPathEnabled = true;
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
			AudioClip sound =   SteeringManager.Instance().s8SoundPrefab;
			asource.audio.PlayOneShot(sound);
			
			//Camera is fixed,set up in front of approaching jets
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(540,300,-20);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = pos.transform;
			
        }
    }
}