using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject _target;

    // Use this for initialization
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Camera");
    }


    // Update is called once per frame
    void Update()
    {
        if(_target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime);
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
