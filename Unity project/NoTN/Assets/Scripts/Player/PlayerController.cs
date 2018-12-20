using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    private PlayerMotor motor;
    public float xMove;
    public float zMove;
    public float mouseSensitivity = 3f;
    // Use this for initialization
    void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        xMove = Input.GetAxisRaw("Horizontal");
        zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveHorizontal = transform.right * xMove;
        Vector3 moveVertical = transform.forward * zMove;

        //final movement vector
        Vector3 velocity = (moveHorizontal + moveVertical).normalized * speed;

        motor.Move(velocity);


        //calculate rotation as a 3d vector
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 rotation = new Vector3(0, yRotation, 0) * mouseSensitivity;

        motor.Rotate(rotation);


        //calculate camera Rotation.
        float xRotation = Input.GetAxisRaw("Mouse Y");
        Vector3 cameraRotation = new Vector3(xRotation, 0, 0) * mouseSensitivity;

        motor.CameraRotate(cameraRotation);
    }



   
}
