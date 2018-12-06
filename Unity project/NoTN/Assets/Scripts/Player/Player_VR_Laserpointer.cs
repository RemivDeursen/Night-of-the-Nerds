using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Player_VR_Laserpointer : MonoBehaviour {
	private SteamVR_TrackedObject trackedObj;

	void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
