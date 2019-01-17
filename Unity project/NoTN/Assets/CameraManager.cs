using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public int CameraID;
    public Transform PlayerCameraPosition;

    private AudioSource _beepSound;
    private LineRenderer _cameraLaser;
    public bool _laserPointerOn;

    // Use this for initialization
    void Start()
    {
        _beepSound = GetComponent<AudioSource>();
        _cameraLaser = GetComponent<LineRenderer>();
        _laserPointerOn = false;
    }

    public void MakeSound()
    {
        if (!_beepSound.isPlaying)
            _beepSound.Play();
    }


    public void ToggleLaserPointer()
    {
        _laserPointerOn = !_laserPointerOn;
        _cameraLaser.enabled = _laserPointerOn;
    }
}
