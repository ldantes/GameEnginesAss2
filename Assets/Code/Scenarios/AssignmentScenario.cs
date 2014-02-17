using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.Scenarios
{
    public class AssignmentScenario : Scenario
    {
		private PathFinder pathFinder;
		Vector3 targetPos;
        Vector3 startPos;
        public override string Description()
        {
            return "Assignment Demo";
        }

        public override void Start()
        {
            Params.Load("default.txt");

           leader = CreateBoid(new Vector3(0, 0, 0), leaderPrefab);
            leader.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.evade);
			leader.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.seek);
			leader.transform.Rotate(new Vector3(0, 180, 0));
			pathFinder = new PathFinder();
			targetPos = new Vector3(20, 200, 250);
            startPos = new Vector3(10, 0, -20);
            Path path = pathFinder.FindPath(startPos, targetPos);
            path.Looped = true;
            path.draw = true;
            leader.GetComponent<SteeringBehaviours>().path = path;
            leader.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.follow_path);
			
			 // Create the boids
           /* GameObject boid = null;
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(-20-(i*10), 20, 20+(i*10));
                boid = CreateBoid(pos, boidPrefab);
                boid.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.separation);
                boid.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.cohesion);
                boid.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.alignment);
                boid.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.wander);
                boid.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.sphere_constrain);
                boid.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.obstacle_avoidance);
            }*/
			
			GameObject alienCrafts = null;
            for (int i = 0; i < 3; i++)
            {
                Vector3 pos = new Vector3(20-(i*10), 200, 250+(i*10));
                alienCrafts = CreateBoid(pos, boidPrefab);
				leader.transform.Rotate(new Vector3(0, 180, 0));
				alienCrafts.GetComponent<SteeringBehaviours>().leader =alienCrafts;
				alienCrafts.GetComponent<SteeringBehaviours>().target =leader;
              
                alienCrafts.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.seek);
				alienCrafts.GetComponent<SteeringBehaviours>().turnOn(SteeringBehaviours.behaviour_type.separation);
				alienCrafts.GetComponent<SteeringBehaviours>().seekTargetPos = leader.transform.position;
				//alienCrafts.transform.localScale(0.01, 0.01, 0.01);
				
            }
			leader.GetComponent<SteeringBehaviours>().target = alienCrafts;
           
            CreateCamFollower(leader, new Vector3(0, 5, -10));
			
			mothership = CreateBoid(new Vector3(20, 200, 250), mothershipPrefab);

            GroundEnabled(false);
        }
    }
}