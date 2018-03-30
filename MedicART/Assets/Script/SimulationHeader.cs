using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct SimulationHeader {

	public string uName;
	public string date;
	public float simulaionDuration;

	public SimulationHeader(string u, string d, float sd){
		uName = u;
		date = d;
		simulaionDuration = sd;
	}
}
