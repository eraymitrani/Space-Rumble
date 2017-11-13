using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindEmitter : MonoBehaviour {

	GameObject wind_square;
	float ang;
	GameObject[] clones = new GameObject[10];


	// Use this for initialization
	void Start () {
		wind_square = GameObject.Find ("wind_pixel");
	}
	
	// Update is called once per frame
	void Update () {
	    if (GetComponentInParent<Animator>().GetBool("Dead") == true)
	    {
	        return;
	    }
        if (GetComponentInParent<ArmRotation> ().controller.RightTrigger.IsPressed && GetComponentInParent<WeaponController>().fuel > 0) {
			ang = GetComponentInParent<ArmRotation> ().angle * Mathf.Deg2Rad;
			ang += Random.Range (-0.3f, 0.3f);

			GameObject clone = Instantiate (wind_square, transform.position, Quaternion.identity);
			clone.GetComponent<Rigidbody2D> ().velocity = 10 * new Vector2 (Mathf.Cos (ang), Mathf.Sin (ang));
			clone.GetComponent<WindLifetime> ().is_alive = true;

			//XXX comment this out to keep white spray
//			clone.GetComponent<SpriteRenderer> ().material = 
//				GameObject.Find ("GameStateController").GetComponent<PlayerSpawner> ().Colors [
//					GetComponentInParent<UnityStandardAssets._2D.Platformer2DUserControl>().player_num - 1];
			}

		if (GetComponentInParent<ArmRotation> ().controller.LeftTrigger.WasPressed && GetComponentInParent<WeaponController> ().fuel >= 0) {
			
				
		}


	}

	public void BurstParticles(){
		Debug.Log (GetComponentInParent<WeaponController> ().fuel);
		clones [0] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [1] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [2] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [3] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [4] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [5] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [6] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [7] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [8] = Instantiate (wind_square, transform.position, Quaternion.identity);
		clones [9] = Instantiate (wind_square, transform.position, Quaternion.identity);

		float inc = -0.2f;

		foreach (var clone in clones) {
			ang = GetComponentInParent<ArmRotation> ().angle * Mathf.Deg2Rad;
			ang += inc;
			inc += 0.04f;

			clone.GetComponent<Rigidbody2D> ().velocity = 30 * new Vector2 (Mathf.Cos (ang), Mathf.Sin (ang));
			clone.GetComponent<WindLifetime> ().is_alive = true;

			//XXX comment this out to keep white spray
//			clone.GetComponent<SpriteRenderer> ().material = 
//				GameObject.Find ("GameStateController").GetComponent<PlayerSpawner> ().Colors [
//					GetComponentInParent<UnityStandardAssets._2D.Platformer2DUserControl>().player_num - 1];
		}
	}
}
