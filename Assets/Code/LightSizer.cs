using UnityEngine;
using System.Collections;

namespace BGE
{
    public class LightSizer : MonoBehaviour
    {
		public float range;
		public float intensity;
		float timeShot = 0.25f;

        // Use this for initialization
        void Start()
        {
			
        }

        // Update is called once per frame
        void Update()
        {
			timeShot += Time.deltaTime;
			if (timeShot > 0.5f)
	                    {
				if(light.range < range)
				{
	            	light.range +=2 ;
				}
						
				if(light.intensity != intensity)
				{
					light.intensity+=1;
				}
				timeShot = 0.0f;
			}
        }
    }
}