using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Vector3[] vertices = mesh.vertices;
        int i = 0;
        while (i < vertices.Length)
        {
            Debug.Log(vertices[i].ToString());
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
