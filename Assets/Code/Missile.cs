using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BGE
{
	class Missile:MonoBehaviour
	{
		public Color color;
        public void Update()
        {
            float speed = 10.0f;
            float width = 500;
            float height = 500;
			

            transform.position += transform.forward * speed;
            
        }
	}
}
