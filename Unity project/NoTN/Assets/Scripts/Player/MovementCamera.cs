using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCamera : MonoBehaviour {

    public GameObject securityCamera;
    private bool noLeft=false;
    private bool noRight = false;
    private bool noUp = false;
    private bool noDown = false;
    // Update is called once per frame
    void Update () {
        
        if (noLeft == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                securityCamera.transform.Rotate(0, -1, 0,Space.World);
            }
        }
        if (noRight == false)
        {
            if (Input.GetKey(KeyCode.D))
            {
                securityCamera.transform.Rotate(0, 1, 0,Space.World);
            }
        }
       
        if (securityCamera.transform.rotation.eulerAngles.y < 97)
        {
            noLeft = true;
        }
        else
        {
            noLeft = false;
        }
        if (securityCamera.transform.rotation.eulerAngles.y > 170)
        {
            noRight = true;
        }
        else
        {
            noRight = false;
        }
       

    }
}
