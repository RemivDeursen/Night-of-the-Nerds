using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectNearObjectsScript : MonoBehaviour {

    public float distance;
    public bool nearObject=false;
    public GameObject spawningPoint;
    public Text PickUpText;
    public Text DropDownText;
    public bool holding = false;
    private RaycastHit hit;
    public GameObject holdingObject;
    public GameObject environment;
    void FixedUpdate ()
    {
        if (holding == false)
        {
            RayCasting();
        }
       if (nearObject==true)
        {
            PickUpText.gameObject.SetActive(true);
            DropDownText.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                holdingObject.transform.SetParent(spawningPoint.transform);
                holdingObject.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                holdingObject.transform.position = spawningPoint.transform.position;
                holding = true;
                
            }
        }
       else
        {
            PickUpText.gameObject.SetActive(false);
        }
       if(holding)
       {
            DropDownText.gameObject.SetActive(true);
            PickUpText.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log("HOLDING OFFASDASDA");
                holding = false;
                holdingObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                holdingObject.transform.SetParent(environment.transform);
                holdingObject = null;
            }
       }
    }
    public void RayCasting()
    {
        
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance))
        {

            if (hit.collider.name == "ColliderForPickUp")
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.yellow);
                holdingObject = hit.transform.gameObject;
                nearObject = true;
                

            }
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.white);
            holdingObject =null;
            nearObject = false;
        }
    }
}

