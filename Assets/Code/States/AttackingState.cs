using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE.States
{
    class AttackingState:State
    {
        float timeShot = 0.25f;
        

        public override string Description()
        {
            return "Attacking State";
        }

        public AttackingState(GameObject entity):base(entity)
        {
        }

        public override void Enter()
        {
            entity.GetComponent<SteeringBehaviours>().turnOffAll();
            entity.GetComponent<SteeringBehaviours>().OffsetPursuitEnabled = true;
			//entity.GetComponent<SteeringBehaviours>().WanderEnabled = true;
            entity.GetComponent<SteeringBehaviours>().SeparationEnabled = true;
            //    	entity.GetComponent<SteeringBehaviours>().CohesionEnabled = true;
            //    	entity.GetComponent<SteeringBehaviours>().AlignmentEnabled = true;
        }

        public override void Exit()
        {
        }

        public override void Update()
        {
            float range = 150.0f;
            timeShot += Time.deltaTime;
            float fov = Mathf.PI / 4.0f;
            // Can I see the leader?
           // GameObject leader = GameObject.FindGameObjectWithTag("Player");
			//GameObject[] leaders = GameObject.FindGameObjectsWithTag("Player");
			//for(int i=0; i< leaders.GetLength(0);i++ )
			GameObject leader = entity.GetComponent<SteeringBehaviours>().leader;
	            if ((leader.transform.position - entity.transform.position).magnitude > range  )
	            {
	                entity.GetComponent<StateMachine>().SwicthState(new IdleState(entity));
	            }
	            else
	            {
	                float angle;
	                Vector3 toEnemy = (leader.transform.position - entity.transform.position);
	                toEnemy.Normalize();
	                angle = (float) Math.Acos(Vector3.Dot(toEnemy, entity.transform.forward));
	                if (angle < fov)
	                {
	                    if (timeShot > 2.0f)
	                    {
	                        GameObject lazer = new GameObject();
	                        lazer.AddComponent<Lazer>();
							if(entity.tag== "Player")
							{
								lazer = (GameObject) GameObject.Instantiate(SteeringManager.Instance().missilePrefab,entity.transform.position + new Vector3(15,0,0), Quaternion.identity) ;
								lazer.AddComponent<SteeringBehaviours>();
								lazer.GetComponent<SteeringBehaviours>().turnOffAll();
								lazer.GetComponent<SteeringBehaviours>().PursuitEnabled = true;
								lazer.GetComponent<SteeringBehaviours>().leader = leader;
								lazer.GetComponent<SteeringBehaviours>().maxSpeed =10000.0f;
								lazer.tag ="missile";
								lazer.AddComponent<CapsuleCollider>();
								lazer.GetComponent<CapsuleCollider>().isTrigger = true;
								lazer.GetComponent<CapsuleCollider>().radius = 10;
								lazer.GetComponent<CapsuleCollider>().height=10;
								lazer.GetComponent<SteeringBehaviours>().maxSpeed =1000;
								lazer.AddComponent<Detonator>();
								lazer.GetComponent<Detonator>().explodeOnStart = false;
								lazer.GetComponent<Detonator>().destroyTime=0.5f;
								lazer.GetComponent<Detonator>().Invoke("Explode",5);
								GameObject missileTrail = (GameObject) GameObject.Instantiate(SteeringManager.Instance().missileTrailPrefab,lazer.transform.position -new Vector3(2,0,0), Quaternion.identity) ;
								missileTrail.AddComponent<SmokeTrail>();
								missileTrail.GetComponent<SmokeTrail>().target =lazer.transform;
							}
							else
							{
								lazer.GetComponent<Lazer>().color = Color.green;
								lazer.transform.position = entity.transform.position + new Vector3(0,0,3);
	                       		lazer.transform.forward = toEnemy; //entity.transform.forward;
								lazer.tag ="missile";
								lazer.AddComponent<CapsuleCollider>();
								lazer.GetComponent<CapsuleCollider>().isTrigger = true;
								lazer.GetComponent<CapsuleCollider>().radius = 0.001f;
								lazer.GetComponent<CapsuleCollider>().height=0.0001f;
								
								lazer.GetComponent<Lazer>().color = Color.green;
								lazer.transform.position = entity.transform.position + new Vector3(0,0,-3);
	                       		lazer.transform.forward = toEnemy; //entity.transform.forward;
								lazer.tag ="missile";
								lazer.AddComponent<CapsuleCollider>();
								lazer.GetComponent<CapsuleCollider>().isTrigger = true;
								lazer.GetComponent<CapsuleCollider>().radius = 0.001f;
								lazer.GetComponent<CapsuleCollider>().height=0.0001f;
							}
	                       
							timeShot = 0.0f;
	                    }
	                }
	            }
				if (leader.activeInHierarchy != true)
	            {
	                entity.GetComponent<StateMachine>().SwicthState(new IdleState(entity));
	            }
			
        }
		
		

    }
}
