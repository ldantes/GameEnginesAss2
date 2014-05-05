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
			
			mothership = CreateBoid(new Vector3(620, 200,0), mothershipPrefab);
			mothership.AddComponent<ParticleSystem>();
            Params.Load("default.txt");
			
			leader = InstantiateBoid(new Vector3(-400, 250, -1000), leaderPrefab, Quaternion.Euler(180, 180, 180) );
			
			//leader.transform.rotation = new Quaternion (0,180,0,0);
			//leaderPrefab.transform.Rotate(0, 180.0f, 0);
			Vector3 initialPos = new Vector3(-400, 250, -1000);
     		Path path = leader.GetComponent<SteeringBehaviours>().path;
            leader.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos);
            path.Waypoints.Add(new Vector3(620, 100, -1000));
            
            path.Waypoints.Add(mothership.transform.position);
            //path.Looped = true;
          
            leader.GetComponent<SteeringBehaviours>().turnOffAll();
            leader.GetComponent<SteeringBehaviours>().FollowPathEnabled = true;
			leader.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
			leader.GetComponent<SteeringBehaviours>().maxSpeed = 150.0f;
			//leader.GetComponent<SteeringBehaviours>().EvadeEnabled = true;
			
			int fleetSize = 3;
            float xOff = 12;
            float zOff = -12;
            for (int i = 2; i < fleetSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float z = (i - 1) * zOff;
                    Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), 0, z);
                    GameObject fleet = CreateBoid(leader.transform.position + offset, leaderPrefab);
                    
                    fleet.GetComponent<SteeringBehaviours>().offset = offset;
                    fleet.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
                    fleet.GetComponent<SteeringBehaviours>().WanderEnabled = true;
                    fleet.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
                    fleet.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
                    fleet.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
                }
            }
			
			fleetSize =10;
			
			GameObject alienCrafts =null;
			for (int i = 2; i < fleetSize; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    float z = (i - 1) * zOff;
                    Vector3 offset = new Vector3((xOff * (-i / 2.0f)) + (j * xOff), 0, z);
                    alienCrafts = CreateBoid(mothership.transform.position -new Vector3(0, -150, 0) + offset , boidPrefab);
                    alienCrafts.GetComponent<SteeringBehaviours>().leader = leader;
                    alienCrafts.GetComponent<SteeringBehaviours>().offset = offset;
                    alienCrafts.GetComponent<SteeringBehaviours>().ObstacleAvoidanceEnabled = true;
                    
                    alienCrafts.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
                    alienCrafts.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
                    alienCrafts.GetComponent<SteeringBehaviours>().PlaneAvoidanceEnabled = true;
                }
            }
			
			CreateCamFollower(leader, new Vector3(-50, 5, -10));
			
			

            GroundEnabled(false);
        }
    }
}