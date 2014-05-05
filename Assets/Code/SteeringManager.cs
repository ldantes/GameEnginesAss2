using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BGE.Scenarios;

namespace BGE
{
    

    public class SteeringManager : MonoBehaviour
    {
        List<Scenario> scenarios = new List<Scenario>();
        
        public Scenario currentScenario;
        StringBuilder message = new StringBuilder();
        
        public GameObject camFighter;

        public GameObject boidPrefab;
        public GameObject leaderPrefab;
		public GameObject mothershipPrefab;
		public GameObject missilePrefab;
		public GameObject alienJetTrailPrefab;
		public GameObject jetTrailPrefab;
		public GameObject missileTrailPrefab;
		public GameObject mothershipLazerPrefab;
		public GameObject closingCredits;
		public GameObject openingCredits;
		public AudioClip  explosionSoundPrefab;
		public AudioClip rnfSoundPrefab;
		public AudioClip mis1SoundPrefab;
		public AudioClip fireAtWillSoundPrefab;
		public AudioClip s3SoundPrefab;
		public AudioClip s4SoundPrefab;
		public AudioClip s5SoundPrefab;
		public AudioClip s6SoundPrefab;
		public AudioClip s7SoundPrefab;
		public AudioClip s8SoundPrefab;
		public AudioClip s9SoundPrefab;
		
		public int scenarioNum;
		
		
		
		
        static SteeringManager instance;
        // Use this for initialization
        GUIStyle style = new GUIStyle();

        public bool camFollowing = true;
        
        void Awake()
        {
            DontDestroyOnLoad(this);
        }

        void Start()
        {

            Vector3[] points = new Vector3[3];
            points[0] = new Vector3(1, 1, 1);
            points[1] = new Vector3(1, 2, 0);
            points[2] = new Vector3(-1, 2, 1);
            Plane p = new Plane(points[0], points[1], points[2]);

            instance = this;
            Screen.showCursor = false;

            style.fontSize = 18;
            style.normal.textColor = Color.white;
			
			/*GameObject audio = new GameObject();
			audio.AddComponent<AudioSource>();
			audio.GetComponent<AudioSource>().maxDistance=2000;
			audio.GetComponent<AudioSource>().clip = SteeringManager.Instance().BackgroundSoundPrefab;
			audio.GetComponent<AudioSource>().playOnAwake =true;
			audio.GetComponent<AudioSource>().volume =1;
			audio.GetComponent<AudioSource>().loop = true;*/
			//audio.transform.position = GameObject.FindGameObjectWithTag("MainCamera").transform.position;
			
			scenarioNum=0;
			scenarios.Add(new OpeningCredits());
			scenarios.Add(new scene1());
			scenarios.Add(new scene2());
			scenarios.Add(new scene3());
			scenarios.Add(new scene4());
			scenarios.Add(new scene5());
			scenarios.Add(new scene6());
			scenarios.Add(new scene7());
			scenarios.Add(new scene8());
			scenarios.Add(new scene9());
			scenarios.Add(new scene10());
			scenarios.Add(new ClosingCredits());
			Invoke("nextScene",0);
			Invoke("nextScene",5);
			Invoke("nextScene",10);
			Invoke("nextScene",15);
			Invoke("nextScene",24);
			Invoke("nextScene",37);
			Invoke("nextScene",50);
			Invoke("nextScene",55);
			Invoke("nextScene",65);
			Invoke("nextScene",75);
			Invoke("nextScene",90);
			Invoke("nextScene",105);
			
			
        }

        public static SteeringManager Instance()
        {
            return instance;
        }

        

        void OnGUI()
        {
            if (Params.showMessages)
            {
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "" + message, style);
				GUI.color = Color.blue;
            }
            /*if (Event.current.type == EventType.Repaint)
            {
                message.Length = 0;
            }*/

            if (Event.current.type == EventType.KeyDown)
            {
                /*if (Event.current.keyCode == KeyCode.F1)
                {
                    camFollowing = !camFollowing;
                }

                for (int i = 0; i < scenarios.Count; i++)
                {
                    if (Event.current.keyCode == KeyCode.Alpha0 + i)
                    {
                        currentScenario.TearDown();
                        currentScenario = scenarios[i];
                        currentScenario.Start();
                    }
                }

                float timeModRate = 0.01f;
                if (Event.current.keyCode == KeyCode.F2)
                {
                    Params.timeModifier += Time.deltaTime * timeModRate;
                }

                if (Event.current.keyCode == KeyCode.F3)
                {
                    Params.timeModifier -= Time.deltaTime * timeModRate;
                }

                if (Event.current.keyCode == KeyCode.F4)
                {
                    Params.showMessages = !Params.showMessages;
                }

                if (Event.current.keyCode == KeyCode.F5)
                {
                    Params.drawVectors = !Params.drawVectors;
                }

                if (Event.current.keyCode == KeyCode.F6)
                {
                    Params.drawDebugLines = !Params.drawDebugLines;
                }

                if (Event.current.keyCode == KeyCode.F7)
                {
                    GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                    camera.transform.up = Vector3.up;
                }*/

                if (Event.current.keyCode == KeyCode.Escape)
                {
                    Application.Quit();
                }                
            }
        }

        public static void PrintMessage(string message)
        {
            if (instance != null)
            {
                //Instance().message.Append(message + "\n");
				
            }
        }

        public static void PrintFloat(string message, float f)
        {
            if (instance != null)
            {
               // Instance().message.Append(message + ": " + f + "\n");
            }
        }

        public static void PrintVector(string message, Vector3 v)
        {
            if (instance != null)
            {
                //Instance().message.Append(message + ": (" + v.x + ", " + v.y + ", " + v.z + ")\n");
            }
        }
		
		public void nextScene()
        {
			
			//currentScenario.DestroyObjectsWithTag("mothership");
			//currentScenario.DestroyObjectsWithTag("leader");
			currentScenario = scenarios[scenarioNum];
			scenarioNum++;
            currentScenario.Start();
			
            
        }
        // Update is called once per frame
        void Update()
        {
          /*  PrintMessage("Press F1 to toggle cam following");
            PrintMessage("Press F2 to slow down");
            PrintMessage("Press F3 to speed up");
            PrintMessage("Press F4 to toggle messages");
            PrintMessage("Press F5 to toggle vector drawing");
            PrintMessage("Press F6 to toggle debug drawing");
            PrintMessage("Press F7 to level camera");
            int fps = (int)(1.0f / Time.deltaTime);
            PrintFloat("FPS: ", fps);*/
            PrintMessage("Current scenario: " + currentScenario.Description());
           /* for (int i = 0; i < scenarios.Count; i++)
            {
                PrintMessage("Press " + i + " for " + scenarios[i].Description());
            }*/

            if (camFollowing)
            {
                GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
                camera.transform.position = camFighter.transform.position;
                camera.transform.rotation = camFighter.transform.rotation;
            }
			
      		
        }
    }
}
