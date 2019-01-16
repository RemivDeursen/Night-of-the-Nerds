using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer line;
    public bool levelComplete = false;
    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = true;
        StartCoroutine("FireLaser");

        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetKey(KeyCode.C));
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (!levelComplete)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);
			// If the laser hits a surface (Collider) We let the laser end at the collision point. If not we let it go to the max range (100)
            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
            }
            else
            {
                line.SetPosition(1, ray.GetPoint(100));
            }

            yield return null;
        }

        line.enabled = false;
    }
}
