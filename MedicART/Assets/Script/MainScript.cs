// API Key: SmvExKbpTZ9D_lSWBX9WEEo_nEboqR6q
// https://api.mlab.com/api/1/databases/medicart/collections/simulations?apiKey=SmvExKbpTZ9D_lSWBX9WEEo_nEboqR6q

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.XR;

public class MainScript : MonoBehaviour {
	
    public static MainScript instance;
    private bool isStarted;
    public delegate void StopTracking ();
	public static event StopTracking stopTrack;
	public delegate void SetTarget ();
	public static event SetTarget targetSet;
	private List<TrackingInfo> toolTrackingList;
    private List<TrackingInfo> headTrackingList;
	private TrackingInfo targetInfo;
	private SimulationData simData;
	private SimulationHeader simHeader;
	public Text text;
	public Text text2;
    public bool timeout;
    public bool sendTrack;
    private GameObject target;
    private Transform origin;
    public TrackableBehaviour head;
    public TrackableBehaviour tool;
	private float startTime;
	public GameObject cameraGo;
    private float officialStart;
    private GameObject LoginManager;
	public bool inSkull = false;
    private Vector3 vPath = new Vector3();
    public Text sendText;
    public GameObject refCube;
    private TrackingInfo cube;
	private string user;

	private void Awake()
    {
        instance = this;

		XRSettings.enabled = true;

		toolTrackingList = new List<TrackingInfo> ();
        headTrackingList = new List<TrackingInfo>();
        LoginManager = GameObject.FindGameObjectWithTag("LoginManager");
    }

    void Start () {
        isStarted = false;
        timeout = false;
        sendTrack = false;
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        officialStart = 100000f;
        sendText.enabled = false;
		if (LoginManager.GetComponent <LoginScript> ().debugging) {
			text.enabled = true;
			text.transform.GetChild(0).gameObject.SetActive(true);
			text2.enabled = true;
			text2.transform.GetChild(0).gameObject.SetActive(true);
			text.text = "Head";
			text2.text = "Tool";
		} else {
			text.enabled = false;
			text.transform.GetChild(0).gameObject.SetActive(false);
			text2.enabled = false;
			text2.transform.GetChild(0).gameObject.SetActive(false);
		}

		user = LoginManager.GetComponent<LoginScript> ().getUser ();
	}

    public void Begin()
    {
        isStarted = true;
		toolTrackingList.Clear ();
        headTrackingList.Clear();
    }

    public bool Started()
    {
        return isStarted;
    }

    public void End() {
        isStarted = false;
    }

    public void callTrackingDelegate()
    {
        stopTrack();
        if (sendTrack) {

			Debug.Log ("<color=green>Sending Track</color>");

			string date = DateTime.Now.ToString ();
            sendText.enabled = true;
			simData = new SimulationData (date, user, targetInfo,cube, vPath, headTrackingList.ToArray(), toolTrackingList.ToArray(), timeout, LoginManager.GetComponent<LoginScript>().debugging);
			string url = "https://api.mlab.com/api/1/databases/medicart/collections/simulations?apiKey=SmvExKbpTZ9D_lSWBX9WEEo_nEboqR6q";
			string json = JsonUtility.ToJson (simData);
			WWW request = POST (json, url);

			simHeader = new SimulationHeader (user, date, toolTrackingList [toolTrackingList.Count-1].time - toolTrackingList [0].time);
			string url2 = "https://api.mlab.com/api/1/databases/medicart/collections/simheaders?apiKey=SmvExKbpTZ9D_lSWBX9WEEo_nEboqR6q";
			string json2 = JsonUtility.ToJson(simHeader);
			WWW request2 = POST (json2, url2);
            
        }
        sendText.enabled = false;
        sendTrack = false;
        timeout = false;
        vPath = Vector3.zero;
        officialStart = 100000f;
	}

	public void callTargetDelegate(){
		targetSet ();
        target = GameObject.FindGameObjectWithTag ("Target");
        origin = target.transform;
	}

	public void TargetPosition(GameObject go){
		startTime = Time.time;
        float tempTime = Time.time - startTime;
		targetInfo = new TrackingInfo (go.transform.position, go.transform.rotation, tempTime, true);

        cube = new TrackingInfo(refCube.transform.position, refCube.transform.rotation, tempTime,true);

        Vector3 firstKid = go.transform.GetChild(0).transform.position - go.transform.position;
        Vector3 secondKid = go.transform.GetChild(1).transform.position - go.transform.position;
        Vector3 pathVector = secondKid - firstKid;

        vPath = pathVector;
    }

    public void ToolTracking(GameObject go)
    {
		float timestamp = Time.time - startTime;
        if(timestamp < officialStart)
        {
            officialStart = timestamp;
        }
        
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //TOOL
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        float zPos = go.GetComponent<Transform>().position.z - 0.5F;
        float yPos = go.GetComponent<Transform>().position.y;
        float xPos = go.GetComponent<Transform>().position.x;
        Vector3 temp = new Vector3(xPos, yPos, zPos);
		Quaternion tempQ = go.GetComponent<Transform> ().rotation * Quaternion.Inverse (origin.rotation);
		toolTrackingList.Add(new TrackingInfo((temp - origin.position), tempQ, timestamp, inSkull));

        /////////////////////////////////////////////////////////////////////////////////////////////////////
        //HEAD
        /////////////////////////////////////////////////////////////////////////////////////////////////////
        Vector3 delta = Camera.main.transform.position - origin.position;
        Quaternion deltaQ = Camera.main.transform.rotation * Quaternion.Inverse(origin.rotation);
        headTrackingList.Add(new TrackingInfo(delta, deltaQ, timestamp, false));
        
        ///////////////////////////////////////////////////////////////////////////////////////////////////
		//OUTPUT if debugging
		///////////////////////////////////////////////////////////////////////////////////////////////////
		if (LoginManager.GetComponent <LoginScript> ().debugging) {
			string output = "Head:\n" + deltaQ.ToString () + "\n" + delta.ToString ("F4") +"\nIdealPath: " + vPath.ToString();
			text.text = output;
			output = "Tool:\n" + tempQ.ToString () + "\n" + temp.ToString ("F4")+"\nInSkull: "+inSkull;
			text2.text = output;
		}

    }

	private WWW POST(string jsonStr, string url)
	{
		WWW www;
		Hashtable postHeader = new Hashtable();
		postHeader.Add("Content-Type", "application/json");

		var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

		www = new WWW(url, formData, postHeader);
		while (!www.isDone) {
			//Debug.Log (www.progress);
		}
		return www;
	}

}
