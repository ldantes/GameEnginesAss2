using UnityEngine;
using System.Collections;

namespace BGE
{
    public class MoveStuff : MonoBehaviour
    {
		public Vector3 dest = new Vector3(20, 5, 20);

        // Use this for initialization
        void Start()
        {
			transform.Rotate(0,0,0);
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 toDest = dest - transform.position;
            if (toDest.magnitude > 0.1f)
            {
                toDest.Normalize();
                float speed = 5.0f;
                transform.position += toDest * speed * Time.deltaTime;
                transform.forward = toDest;
            }
        }
    }
}