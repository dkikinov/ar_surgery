using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Collision Enter");
			MainScript.instance.inSkull = true;
		}
	}

	void OnTriggerExit(Collider other){
		if (other.gameObject.tag == "Player") {
			MainScript.instance.inSkull = false;
		}
	}

}
