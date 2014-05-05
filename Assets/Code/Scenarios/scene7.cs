/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 7: a ground view up of the Mothership's primary lazer powering up.
 * 
 * The previous scenario objects are not removed, they add as background chaos
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
    public class scene7 : Scenario
    {
		
		static Vector3 destinationPos = Vector3.zero;
        public override string Description()
        {
            return "Ground view of moving mothership";
        }
		
        public override void Start()
        {
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);			
			destinationPos =	new Vector3(1000, 0, 0)	;
			
						
			GameObject mothershipLazer = CreateBoid(new Vector3(620, 300,0), mothershipLazerPrefab);
			
			mothershipLazer.SetActive(true);
			mothershipLazer.AddComponent<LightSizer>();//LightSizer, with each update, the range and intensity increments.
			mothershipLazer.GetComponent<LightSizer>().range=50;
			mothershipLazer.GetComponent<LightSizer>().intensity=8;
			
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().s7SoundPrefab;
			asource.audio.PlayOneShot(sound);
			
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = new Vector3(200,5,-200);
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = mothership.transform;
		
        }
		
    }
}

