using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSlide : MonoBehaviour {
	
    InputField username;
    GameObject panel;
    Vector3 slideUp;
    Vector3 originalPlace;

	void Start () {
        username = transform.GetComponent<InputField>();
        panel = GameObject.FindGameObjectWithTag("Panel");
        originalPlace = panel.transform.position;
        slideUp = new Vector3(originalPlace.x, originalPlace.y + 4, originalPlace.z);
	}
	
	void Update () {
        if (username.isFocused)
        {
            panel.transform.position = slideUp;
        }
        else
        {
            panel.transform.position = originalPlace;
        }
	}

}
