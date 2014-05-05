/* Leslie Ducray, C10327999 - April 2014
 * 
 * Scene 1: Creates a missile which is projecting towards the alien mothership.
 * The alien mothership has a box collider and a Rigidbody. The missile has a capsule collider.
 * 
 * A class 'OnCollideExplode' was created which will trigger when a collider object 'collides' with the mothership solidobject.
 * resulting in an explosion.
 * 
 * audio was added.
 * 
 * The camera is placed behind the fired missile and watches the contact/explosion.
 * 
*/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BGE.Scenarios;


namespace BGE.Scenarios
{
    public class scene1 : Scenario
    {
		
		
        public override string Description()
        {
            return "Scene 1: Missile towards mothership";
        }
		
        public override void Start()
        {
			SteeringManager.Instance().currentScenario.TearDown();
			
			mothership = CreateBoid(new Vector3(620, 300,0), mothershipPrefab);
			mothership.AddComponent<BoxCollider>();
			mothership.AddComponent<Rigidbody>();
			mothership.GetComponent<Rigidbody>().isKinematic= true;
			mothership.GetComponent<BoxCollider>().isTrigger = true;
			mothership.GetComponent<BoxCollider>().size =new Vector3(1250,1200,1200);
			mothership.AddComponent<OnCollideExplode>();
			mothership.AddComponent<AudioSource>();
			mothership.GetComponent<AudioSource>().maxDistance=1500;
			mothership.GetComponent<OnCollideExplode>().destroyStruckObject = false;
			
			GameObject missile = CreateBoid(new Vector3(-50, 400, 0),missilePrefab);			
			missile.GetComponent<SteeringBehaviours>().SeekEnabled = true;
			missile.GetComponent<SteeringBehaviours>().seekTargetPos = new Vector3(620, 400,0);
			
			GameObject asource = new GameObject();
			asource.AddComponent<AudioSource>();
			asource.GetComponent<AudioSource>().maxDistance=2000;
			asource.GetComponent<AudioSource>().playOnAwake =true;
			asource.GetComponent<AudioSource>().volume =1;
			asource.transform.position = new Vector3(100,300,0);
			AudioClip sound =   SteeringManager.Instance().mis1SoundPrefab;
			asource.audio.PlayOneShot(sound);
			
			
			GameObject missileTrail = CreateBoid(missile.transform.position- new Vector3(-2,0,0),missileTrailPrefab);
			missileTrail.AddComponent<SmokeTrail>();
			missileTrail.GetComponent<SmokeTrail>().target =missile.transform;
			missile.tag ="missile";
			missile.AddComponent<CapsuleCollider>();
			missile.GetComponent<CapsuleCollider>().isTrigger = true;
			missile.GetComponent<CapsuleCollider>().radius = 10;
			missile.GetComponent<CapsuleCollider>().height=10;
			missile.GetComponent<SteeringBehaviours>().maxSpeed =1000;
			
			
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = missile.transform.position+ new Vector3(-75,0,-10);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = missile.transform;
			
						
        }

    }
}