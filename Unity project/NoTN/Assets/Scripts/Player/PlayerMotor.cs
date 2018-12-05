using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;
    public Camera cam;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
        PerformCameraRotation();
    }

    //gets a movement vector.
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
    //gets rotational vector.
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void CameraRotate(Vector3 _cameraRotation)
    {
        cameraRotation = _cameraRotation;
    }


    private void PerformMovement()
    {
        if(velocity != Vector3.zero)
        {
            rb.MovePosition(transform.position + velocity * Time.fixedDeltaTime);
        }
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
    }
    private void PerformCameraRotation()
    {
       if(cam != null)
        {
            cam.transform.Rotate(-cameraRotation);
        }
    }

  
}
