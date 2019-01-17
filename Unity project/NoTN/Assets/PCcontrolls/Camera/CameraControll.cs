using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraControll : NetworkBehaviour
{
    public GameObject[] Cameras;
    public int CurrentControlledCamera;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        CurrentControlledCamera = 3;
        ControlNextCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Vector3 _verticalTurn = new Vector3(Input.GetAxis("Vertical"), 0, 0);
        Cameras[CurrentControlledCamera].gameObject.transform.Rotate(_verticalTurn, Space.Self);
        Vector3 _horizontalTurn = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        Cameras[CurrentControlledCamera].gameObject.transform.Rotate(_horizontalTurn, Space.World);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CmdMakeSound();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdToggleLaserPointer();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ControlNextCamera();
        }
    }

    public void ControlNextCamera()
    {
        if (Cameras[CurrentControlledCamera].GetComponent<CameraManager>()._laserPointerOn)
            CmdToggleLaserPointer();
        CurrentControlledCamera++;
        if (CurrentControlledCamera > Cameras.Length - 1)
            CurrentControlledCamera = 0;
        Camera.main.transform.rotation = Quaternion.identity;
        Camera.main.transform.parent = Cameras[CurrentControlledCamera].GetComponent<CameraManager>().PlayerCameraPosition;
        Camera.main.transform.position = Cameras[CurrentControlledCamera].GetComponent<CameraManager>().PlayerCameraPosition.position;
        Camera.main.transform.rotation = Cameras[CurrentControlledCamera].GetComponent<CameraManager>().PlayerCameraPosition.rotation;
    }

    [Command]
    void CmdMakeSound()
    {
        Cameras[CurrentControlledCamera].GetComponent<CameraManager>().MakeSound();
    }

    [Command]
    void CmdToggleLaserPointer()
    {
        Cameras[CurrentControlledCamera].GetComponent<CameraManager>().ToggleLaserPointer();
    }
}
