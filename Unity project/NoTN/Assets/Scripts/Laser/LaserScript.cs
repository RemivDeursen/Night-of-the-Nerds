using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour {
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

    IEnumerator FireLaser () {
        line.enabled = true;
        MirrorScript mirrorHolder = null;

        while (!levelComplete) {
            Ray ray = new Ray (transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition (0, ray.origin);
            // If the laser hits a surface (Collider) We let the laser end at the collision point. If not we let it go to the max range (100)

            if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Mirror")) {
                    hit.transform.GetComponentInChildren<MirrorScript> ().TriggerReflection (this.transform, hit);
                    mirrorHolder = hit.transform.GetComponentInChildren<MirrorScript> ();
                } else {
                    Debug.Log (mirrorHolder);
                    if (mirrorHolder != null)
                        mirrorHolder.line.enabled = false;
                }
                line.SetPosition (1, hit.point);
            } else {
                if (mirrorHolder != null)
                    mirrorHolder.line.enabled = false;
                line.SetPosition (1, ray.GetPoint (100));
            }

            yield return null;
        }

        Debug.Log (mirrorHolder);
        if (mirrorHolder != null) {
            mirrorHolder.line.enabled = false;
            mirrorHolder = null;
        }

        line.enabled = false;
    }
}