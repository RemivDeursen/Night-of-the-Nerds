using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCollectionPuzzle : MonoBehaviour {

    static List<bool> mugs = new List<bool>();
    public bool nextPuzzle = false;
	void Start () {
		
	}
    private void Update()
    {
        if(mugs.Count == 5 && nextPuzzle == false)
        {
            nextPuzzle = true;
            Debug.Log("5 mugs on the table. NEXT PUZZLE IS ON.");
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            mugs.Add(true);
            Debug.Log("enter");
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 12)
        {
            mugs.Remove(true);
            Debug.Log("exit");
        }
    }
}
