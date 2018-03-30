using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class LoginScript : MonoBehaviour {
	
    public InputField userName;
    public Toggle lefthandedtoggle;
    public Toggle debugmodetoggle;
	public Image loginImage;
    public bool lefthanded;
    public bool debugging;
    string user;

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

		XRSettings.enabled = false;
    }

    void Start () {
        debugging = false;
        lefthanded = false;
	}

    public void Verify()
    {
		string url = "https://api.mlab.com/api/1/databases/medicart/collections/usernames?apiKey=SmvExKbpTZ9D_lSWBX9WEEo_nEboqR6q";

		WWW www = new WWW (url);

		while (!www.isDone) {
		}

		string returnString = www.text;

		returnString = "{\"Items\":" + returnString + "}";

		LoginInfo[] loginInfo;
		loginInfo = JsonHelper.FromJson<LoginInfo> (returnString);

		bool userFound = false;

		for (int i = 0; i < loginInfo.Length; i++) {
			if (userName.text == loginInfo [i].userName) {
				userFound = true;
				i = loginInfo.Length;
			}
		}


		if (userFound) {
			user = userName.text;
			SceneManager.LoadScene ("Loading");
			loginImage.color = Color.green;
		} else {
			loginImage.color = Color.red;
		}
    }

    public string getUser()
    {
        return user;
    }

    public void changeDebugging()
    {
        debugging = debugmodetoggle.isOn;
        Debug.Log(debugging);
    }

    public void changeLeftHanded()
    {
        lefthanded = lefthandedtoggle.isOn;
        Debug.Log(lefthanded);
    }

}
