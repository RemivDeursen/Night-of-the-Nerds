using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    public GameObject LightSource;
    private bool _lightOn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _lightOn = !_lightOn;
            if (_lightOn)
            {
                LightSource.SetActive(true);
            }
            else
            {
                LightSource.SetActive(false);
            }
        }


	}
}
