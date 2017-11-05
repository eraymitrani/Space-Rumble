using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEmitter : MonoBehaviour {

	GameObject wind_square;
	float ang;



	// Use this for initialization
	void Start () {
		wind_square = GameObject.Find ("wind_pixel");
	}
	
	// Update is called once per frame
	void Update () {
		
		if (GetComponentInParent<ArmRotation> ().controller.RightTrigger.IsPressed && GetComponentInParent<WeaponController>().fuel > 0) {
			ang = GetComponentInParent<ArmRotation> ().angle * Mathf.Deg2Rad;
			ang += Random.Range (-0.3f, 0.3f);

			GameObject clone = Instantiate (wind_square, transform.position, Quaternion.identity);
			clone.GetComponent<Rigidbody2D> ().velocity = 10 * new Vector2 (Mathf.Cos (ang), Mathf.Sin (ang));
			clone.GetComponent<WindLifetime> ().is_alive = true;
		}

	}
}
