using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    LineRenderer line;
    // Use this for initialization
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.enabled = false;

        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetKey(KeyCode.C));
        if (Input.GetKeyDown(KeyCode.C))
        {
            StopCoroutine("FireLaser");
            StartCoroutine("FireLaser");
        }
    }

    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetKey(KeyCode.C))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            List<Mirror> mirrorList = new List<Mirror>();

            line.SetPosition(0, ray.origin);
            // If the laser hits a surface (Collider) We let the laser end at the collision point. If not we let it go to the max range (100)

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                line.SetPosition(1, hit.point);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Mirror"))
                {
                    if (mirrorList.Contains(hit.transform.GetComponent<Mirror>()))
                    {
                        line.positionCount = 1 + mirrorList.Count;

                    }
                    Debug.Log(line.positionCount);
                    Vector3 pos = Vector3.Reflect(hit.point - this.transform.position, hit.normal);
                    line.SetPosition(2, pos);
                }
                else
                {
                    line.positionCount = 2;
                }
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
