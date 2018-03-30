using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class vbScript : MonoBehaviour {

	private GameObject vbButtonObject;
	private GameObject startCubeObject;
	private GameObject cubeGo;
	public GameObject debuggingTarget;
	public GameObject target;
	public GameObject ventricule;
	private GameObject LoginManager;
  
	void Awake(){
		LoginManager = GameObject.FindGameObjectWithTag("LoginManager");
	}

	void Start () {
		vbButtonObject = GameObject.Find ("actionButton");

		if (LoginManager.GetComponent <LoginScript> ().lefthanded) {
			startCubeObject = GameObject.Find ("StartTrackingCubeRight");
			startCubeObject.SetActive (false);
			startCubeObject = GameObject.Find ("StartTrackingCubeLeft");
			startCubeObject.SetActive (false);
		} else {
			startCubeObject = GameObject.Find ("StartTrackingCubeLeft");
			startCubeObject.SetActive (false);
			startCubeObject = GameObject.Find ("StartTrackingCubeRight");
			startCubeObject.SetActive (false);
		}

		MainScript.stopTrack += resetSimulation;
	}

	public void OnButtonPressed(){

		if (!MainScript.instance.Started ()) {
			Debug.Log ("Button pressed");
			MainScript.instance.Begin ();
			startCubeObject.SetActive (true);

            float x = 0;
            float y = 0;
            float z = 0;
            
            int randomInt = (int)(Random.Range(0, 3.99f));
            float randomt = Random.Range(0, 1000);
            randomt *= 0.001f;
            switch (randomInt)
            {
                //Right Ventricle Bottom Back
                case 0:
                    x = (3.074f) + (-0.674f * randomt);
                    y = -2.7745f + (0.7745f * randomt);
                    z = 1.963f + (-6.963f * randomt);
                    break;
                 //Right Ventricle Back Top
                case 1:
                    x = (2.4f) + (-0.826f * randomt);
                    y = -2f + (3.963f * randomt);
                    z = -5f + (0.2255f * randomt);
                    break;
                //Left Ventricle Bottom Back
                case 2:
                    x = -3.074f + (0.674f * randomt);
					y = -2.7745f + (0.7745f * randomt);
					z = 1.963f + (-6.963f * randomt);
                    break;
                //Left Ventricle Back Top
				case 3:
					x = -2.4f + (0.576f * randomt);
					y = -2f + (3.963f * randomt);
					z = -5f + (0.2255f * randomt);
                    break;
            }
            GameObject head = GameObject.FindGameObjectWithTag ("Patient's Head" );
			x = x*0.12f;
			y = y*0.12f;
			z = z*0.12f;

			if (LoginManager.GetComponent <LoginScript> ().debugging) {
				cubeGo = Instantiate (debuggingTarget, new Vector3 (ventricule.transform.position.x + x, ventricule.transform.position.y + y, ventricule.transform.position.z + z), head.transform.rotation) as GameObject;
				cubeGo.transform.parent = ventricule.transform;
				cubeGo.transform.Rotate (new Vector3 (Random.Range (-45, 45), Random.Range (-45, 45), Random.Range (-45, 45)));
			} else {
				cubeGo = Instantiate (target, new Vector3 (ventricule.transform.position.x+x, ventricule.transform.position.y+y, ventricule.transform.position.z+z), head.transform.rotation) as GameObject;
				cubeGo.transform.parent = ventricule.transform;
				cubeGo.transform.Rotate(new Vector3(Random.Range(-45, 45), Random.Range(-45, 45), Random.Range(-45, 45)));
			}

            MainScript.instance.callTargetDelegate ();
			vbButtonObject.GetComponentInChildren<TextMesh> ().text = "STOP";
		} else {
			MainScript.instance.callTrackingDelegate ();
            startCubeObject.SetActive(false);
		}

	}

	void resetSimulation(){
		MainScript.instance.End();
		vbButtonObject.GetComponentInChildren<TextMesh> ().text = "START";
	}

}
