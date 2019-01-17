using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraControll : NetworkBehaviour
{
    public Transform PlayerCameraPosition;
    private bool _laserPointerOn;
    private AudioSource _beepSound;
    private LineRenderer _cameraLaser;

    // Use this for initialization
    void Start()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Camera.main.transform.parent = PlayerCameraPosition;
        Camera.main.transform.position = PlayerCameraPosition.position;
        _laserPointerOn = false;
        _beepSound = GetComponent<AudioSource>();
        _cameraLaser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Vector3 _verticalTurn = new Vector3(-Input.GetAxis("Vertical"), 0, 0);
        transform.Rotate(_verticalTurn);
        Vector3 _horizontalTurn = new Vector3(0, Input.GetAxis("Horizontal"), 0);
        transform.Rotate(_horizontalTurn, Space.World);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CmdMakeSound();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            CmdToggleLaserPointer();
        }
    }

    [Command]
    void CmdMakeSound()
    {
        if (!_beepSound.isPlaying)
            _beepSound.Play();
    }

    [Command]
    void CmdToggleLaserPointer()
    {
        _laserPointerOn = !_laserPointerOn;
        _cameraLaser.enabled = _laserPointerOn;
    }
}
