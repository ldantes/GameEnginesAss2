using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BGE.Scenarios;


namespace BGE.Scenarios
{
    public class OpeningCredits : Scenario
    {
		
		
        public override string Description()
        {
            return "Scene x: Closing Credits";
        }
		
        public override void Start()
        {
			SteeringManager.Instance().currentScenario.TearDown();
			GameObject credits = SteeringManager.Instance().openingCredits;
			credits.SetActive(true);
			GameObject.FindGameObjectWithTag("MainCamera").transform.position = credits.transform.position +new Vector3(0,10,0);
			GameObject.FindGameObjectWithTag("MainCamera").AddComponent<FollowObject>();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<FollowObject>().target = credits.transform;
		}
	}
}