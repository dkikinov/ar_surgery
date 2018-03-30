using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingRotation : MonoBehaviour {
	
    public RawImage portait;
    public RawImage landscape;

	void Start () {
        landscape.enabled = false;
        Invoke("Swap", 5f);
	}

	void Swap()
    {
        landscape.enabled = true;
        portait.enabled = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Invoke("Load", 5f);
    }

  	void Load()
    {
        SceneManager.LoadScene("Capstone");
    }

}
