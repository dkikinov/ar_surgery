using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct TrackingInfo {
	
    public float[] position;
    public float[] orientation;
    public float time;
    public bool inSkull;


    public TrackingInfo(Vector3 v, Quaternion q,  float timeIn, bool skull){
		position = new float[3];
		position[0] = v.x;
		position[1] = v.y;
		position[2] = v.z;

		orientation = new float[4];
		orientation[0] = q.w;
		orientation[1] = q.x;
		orientation[2] = q.y;
		orientation[3] = q.z;

        time = timeIn;

        inSkull = skull;
	}

	public void setPosition(Vector3 v){
		position = new float[3];
		position[0] = v.x;
		position[1] = v.y;
		position[2] = v.z;

	}

	public void setOrientation(Quaternion q){
		orientation = new float[4];
		orientation[0] = q.w;
		orientation[1] = q.x;
		orientation[2] = q.y;
		orientation[3] = q.z;
	}

	public float[] getPosition(){
		
		return position;
	}

	public float[] getOrientation(){

        return orientation;
	}
    
}
