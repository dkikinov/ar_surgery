using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct SimulationData {

	public string date;
	public string user;
	public TrackingInfo target;
    public TrackingInfo refCube;
    public float[] truePath;
	public TrackingInfo[] headPoints;
	public TrackingInfo[] toolPoints;
	public bool timeOut;
    public bool debugged;

    public SimulationData(string t, string u, TrackingInfo targ, TrackingInfo cube, Vector3 vectorPath, TrackingInfo[] hPoints, TrackingInfo[] tPoints, bool tOut, bool debugging){
		date = t;
		user = u;
		target = targ;
        refCube = cube;
        truePath = new float[3];
        truePath[0] = vectorPath.x;
        truePath[1] = vectorPath.y;
        truePath[2] = vectorPath.z;
        headPoints = hPoints;
		toolPoints = tPoints;
		timeOut = tOut;
        debugged = debugging;
	}

}
