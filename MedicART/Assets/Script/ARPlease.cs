using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARPlease : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Camera mainCamera = Camera.main ;
        if (mainCamera) {
            if (mainCamera.GetComponent<VuforiaBehaviour>() != null) {
                mainCamera.GetComponent<VuforiaBehaviour>().enabled = true;
            }
            if (mainCamera.GetComponent<VideoBackgroundBehaviour>() != null) {
                mainCamera.GetComponent<VideoBackgroundBehaviour>().enabled = true;
            }
            if (mainCamera.GetComponent<DefaultInitializationErrorHandler>() != null)
            {
                mainCamera.GetComponent<DefaultInitializationErrorHandler>().enabled = true;
            }
        }
    }
}
