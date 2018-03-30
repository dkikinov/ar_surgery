using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HasCollided : MonoBehaviour {

	private GameObject go;
	private GameObject cubeGo;
	private GameObject startCubeGo;
	private bool endFlag;
	private float startTime;
	private int duration = 60;

	void Awake(){
		go = GameObject.FindGameObjectWithTag("Player");
		if (GameObject.FindGameObjectWithTag ("LoginManager").GetComponent<LoginScript> ().lefthanded) {
			startCubeGo = GameObject.Find ("StartTrackingCubeLeft");
		} else {
			startCubeGo = GameObject.Find ("StartTrackingCubeRight");
		}
	}

	void Start(){
		MainScript.stopTrack += CancelInvoke;
		MainScript.stopTrack += RemoveTarget;
		MainScript.targetSet += SetTarget;
	}

    void OnCollisionEnter(Collision collision){

		if(collision.gameObject.tag == "StartCube")
		{
			startCubeGo.SetActive (false);

			startTime = Time.time;

			if(MainScript.instance.Started())
			{
				InvokeRepeating("TrackingFunction", 0.0f, 0.1f);
                MainScript.instance.sendTrack = true;
			}
		}

	}

	void OnCollisionExit(Collision collision){
		go.GetComponent<Renderer> ().material.color = Color.white;
	}

	void TrackingFunction(){
		if ((Time.time - startTime) >= duration) {
            MainScript.instance.timeout = true;
			MainScript.instance.callTrackingDelegate ();
		}
		MainScript.instance.ToolTracking (go);

	}

	void RemoveTarget(){
		GameObject.Destroy (cubeGo);
	}

	void SetTarget(){
		cubeGo = GameObject.FindGameObjectWithTag ("Target");
		MainScript.instance.TargetPosition (cubeGo);
	}

}
