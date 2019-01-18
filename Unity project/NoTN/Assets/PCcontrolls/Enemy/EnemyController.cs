using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject _target;
    public GameObject[] _cameras;

    // Use this for initialization
    void Start()
    {
        _cameras = GameObject.FindGameObjectsWithTag("Camera");
    }


    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            transform.LookAt(_target.transform);
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime/5);
        }
        else
        {
            SelectTarget();
        }
    }

    public void SelectTarget()
    {
        foreach(GameObject camera in _cameras)
        {
            if(camera.GetComponent<CameraManager>().CameraID == camera.transform.parent.GetComponent<CameraControll>().CurrentControlledCamera)
            {
                _target = camera;
            }
        }
    }


    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
        else if (collision.gameObject.tag == "Camera")
        {
            //Destroy camera
            Debug.Log("Camera hit!");
        }
    }
}
