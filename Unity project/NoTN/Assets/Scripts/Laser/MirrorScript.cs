using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour {
	public LineRenderer line;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () { }

	public void TriggerReflection (Transform main, RaycastHit hit) {
		Debug.Log("Trigger reflect");
		StopCoroutine ("FireLaser");
		StartCoroutine (FireLaser (main, hit));

	}

	IEnumerator FireLaser (Transform main, RaycastHit hit) {
		line.enabled = true;
		MirrorScript mirrorHolder = null;
		Vector3 pos = Vector3.Reflect (hit.point - main.position, hit.normal);
		Ray ray = new Ray (hit.point, pos);

		line.SetPosition (0, ray.origin);
		// If the laser hits a surface (Collider) We let the laser end at the collision point. If not we let it go to the max range (100)

		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Mirror")) {
				Debug.Log("Second Mirror");
				hit.transform.gameObject.GetComponent<MirrorScript> ().TriggerReflection (this.transform, hit);
				mirrorHolder = hit.transform.gameObject.GetComponent<MirrorScript> ();
			} else {
				Debug.Log (mirrorHolder);
				if (mirrorHolder != null)
					mirrorHolder.line.enabled = false;
			}
			line.SetPosition (1, hit.point);
		} else {
			if (mirrorHolder != null)
				mirrorHolder.line.enabled = false;
			line.SetPosition (1, ray.GetPoint (1000));
		}

		yield return null;

		Debug.Log (mirrorHolder);
		if (mirrorHolder != null) {
			mirrorHolder.line.enabled = false;
			mirrorHolder = null;
		}

		line.enabled = false;
	}
}