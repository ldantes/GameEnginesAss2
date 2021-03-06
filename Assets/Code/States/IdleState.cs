﻿using System;
using System.Collections.Generic;
using UnityEngine;
using BGE;

namespace BGE.States
{
    public class IdleState:State
    {
       

        public override string Description()
        {
            return "Idle State";
        }

        public IdleState(GameObject entity):base(entity)
        {
        }

        public override void Enter()
        {
           
           // entity.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos);
          //  entity.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos + new Vector3(-50, 0, 80));
          //  entity.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos + new Vector3(0, 0, 160));
          //  entity.GetComponent<SteeringBehaviours>().path.Waypoints.Add(initialPos + new Vector3(50, 0, 80));
          //  entity.GetComponent<SteeringBehaviours>().path.Looped = true;            
         //   entity.GetComponent<SteeringBehaviours>().path.draw = true;
         	entity.GetComponent<SteeringBehaviours>().turnOffAll();
         	entity.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
            entity.GetComponent<SteeringBehaviours>().CohesionEnabled = true;
            entity.GetComponent<SteeringBehaviours>().AlignmentEnabled = true;
			entity.GetComponent<SteeringBehaviours>().RandomWalkEnabled = true;
        }
        public override void Exit()
        {
          //  entity.GetComponent<SteeringBehaviours>().path.Waypoints.Clear();
        }

        public override void Update()
        {
            float range = 100.0f;           
            // Can I see the leader?
            GameObject[] leaders = GameObject.FindGameObjectsWithTag(entity.GetComponent<SteeringBehaviours>().attackTag);
			foreach (GameObject leader in leaders) 
			
			{
	            if ((leader.transform.position - entity.transform.position).magnitude < range)
	            {
	                // Is the leader inside my FOV
					entity.GetComponent<SteeringBehaviours>().leader =leader;
	                entity.GetComponent<StateMachine>().SwicthState(new AttackingState(entity));
	            }
			}
        }
    }
}
