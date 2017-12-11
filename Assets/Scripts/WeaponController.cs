using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class WeaponController : MonoBehaviour
{

    public LayerMask toHitMask;
    private Transform fireLoc;
	private Vector3 fireLocVec;
	//public GameObject wind_square;

	public float fuel = 100;
	int consume_rate = 90;		//higher is faster
	int recharge_rate = 70;		//lower is slower
	int air_charge_rate = 70, ground_charge_rate = 150;

	Rigidbody2D rb;
	float x, y;
	bool is_playing = false;

	public bool is_powered_up = false;
	float powered_up_timer = 0;
	float powered_up_time = 3;

	float wait_time = 0.2f, wait_timer = 0f;
	bool waiting = false;

	bool do_burst = false, do_shoot = false;

	//InputDevice controller;

//	void Start(){
//		controller = GetComponentInParent<ArmRotation>().controller;
//	}

	void Start(){
		
	}

    public float power = 800f;
	public float self_power = 30f;
	// Use this for initialization
	void Awake ()
	{
	    fireLoc = transform.Find("Hose");
		fireLocVec = transform.Find ("Hose").transform.position;
	    if (fireLoc == null)
	    {
	        Debug.LogError("bad");
	    }
	//	Debug.Log (fireLoc.position);
	//	Debug.Log (fireLocVec);
	    
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.parent.parent.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().is_grounded()) {
			recharge_rate = ground_charge_rate;
		} else {
			recharge_rate = air_charge_rate;
		}

		if (GetComponentInParent<ArmRotation> ().controller == null || GetComponentInParent<Animator>().GetBool("Dead") == true) {
			return;
		}
			
		//if (Input.GetKeyDown("joystick 1 button 1") || Input.GetKeyDown("s"))

		if ((GetComponentInParent<ArmRotation> ().controller.LeftTrigger.WasPressed  && fuel > 33) || 
			GetComponentInParent<ArmRotation> ().controller.RightTrigger.WasReleased) {
			waiting = true;
		}
		if (waiting) {
			wait_timer += Time.deltaTime;
			if (wait_timer > wait_time) {
				wait_timer = 0;
				waiting = false;
			}
		}

		if (GetComponentInParent<ArmRotation>().controller.LeftTrigger.WasPressed){
			//Debug.Log ("Burst");
			do_burst = true;
			//Burst ();
		} else if (GetComponentInParent<ArmRotation>().controller.RightTrigger.IsPressed){
            //Debug.Log("Shoot");
			do_shoot = true;
	        //Shoot();
		} else {
			//refuel
			if (is_powered_up) {
				fuel = 100;
			}

			else if (fuel > 100) {
				fuel = 100;
			} else if (!waiting){
				fuel += Time.deltaTime * recharge_rate;
			}

			GetComponent<AudioSource> ().Stop ();
			is_playing = false;
		}

		if (is_powered_up) {
			powered_up_timer += Time.deltaTime;
			if (powered_up_timer >= powered_up_time) {
				is_powered_up = false;
				powered_up_timer = 0;
			}
		}
	}

	void FixedUpdate(){
		if (do_burst) {
			Burst ();
			do_burst = false;
		} else if (do_shoot) {
			Shoot ();
			do_shoot = false;
		}
	}

    void Shoot()
    {
		bool hit_wall_first = false;

		do_shoot = false;
		if (!is_powered_up) {
			fuel -= Time.deltaTime * consume_rate;
		} else {
			//Debug.Log ("powered up!");
			fuel = 100;
		}

		if (fuel < 0) {
			fuel = 0;
			return;
		}
		//Debug.Log (fuel.ToString());

		if (!is_playing) {
			GetComponent<AudioSource> ().Play ();
			is_playing = true;
		}

		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
		Vector2 angle = new Vector2(transform.position.x,transform.position.y);
		angle.Normalize();

		//angle = GetComponentInParent<ArmRotation> ().angle_vec;
		angle = new Vector2(Mathf.Cos(GetComponentInParent<ArmRotation>().angle), Mathf.Sin(GetComponentInParent<ArmRotation>().angle));
		angle = GetComponentInParent<ArmRotation> ().angle_vec;

		rb = transform.parent.parent.GetComponent<Rigidbody2D> ();

		//aiming up while on the ground
		if (angle.y > -0.4f && transform.parent.parent.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().is_grounded ()) {
			//don't knock back
		} else { //not grounded, aiming down
			//do knock back
			rb.AddForce(angle * -45, ForceMode2D.Force);
		}

		RaycastHit2D[] test_hits = Physics2D.RaycastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), angle, 7f, toHitMask);
		Debug.DrawRay (fireLoc.position, 7 * angle);

		foreach (var test in test_hits) {
			if (test.collider.gameObject != gameObject && test.collider.gameObject != transform.parent.parent.gameObject) {
				if (test.collider.tag == "wall") {
					//Debug.Log (test.collider.tag);
					//return;
					hit_wall_first = true;
					break;
				} else if (test.collider.tag == "Player" || test.collider.tag == "crate") {
					//Debug.Log (test.collider.tag);
					hit_wall_first = false;
					break;
				}
			}
		}

		RaycastHit2D[] hits = Physics2D.CircleCastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), 2f,  angle , 5f, toHitMask);
		Debug.DrawRay (fireLoc.position, 5 * angle);

		foreach (var hit in hits) {
			Vector2 dist = new Vector2 (hit.point.x - fireLoc.position.x, hit.point.y - fireLoc.position.y);

			if (hit.collider.gameObject.tag == "mid_wall") {
				return;
			}

			// Stop when colliding with walls and ground
			if (hit.collider.gameObject.tag != "wall") {
				
				if (hit.rigidbody != null && dist.magnitude > 0) {
					if (hit.collider.gameObject != gameObject && hit.collider.gameObject != transform.parent.parent.gameObject) {
						//hit.collider.attachedRigidbody.AddForce (dist.normalized * (power * 10 / (2 * dist.magnitude)));

						if (hit.collider.gameObject.GetComponent<Inventory> () == null) {
							hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 300, dist.normalized.y * 50));
						}

						if (hit.collider.gameObject.GetComponent<Inventory> () != null && !hit.collider.gameObject.GetComponent<Inventory> ().isImmovable) {
							hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 300, dist.normalized.y * 50));
							//Debug.Log (hit.collider.tag);
						}
					}
				}
			} 
//			} else if(hit_wall_first){
//				return;
//			}
		} 
			
    }

	void Burst(){
		bool hit_wall_first = false;

		do_burst = false;
		if (fuel < 33) {
			return;
		} else {
			fuel -= 33;
			GetComponent<WindEmitter> ().BurstParticles ();
		}


		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
		Vector2 angle = GetComponentInParent<ArmRotation> ().angle_vec;
		rb = transform.parent.parent.GetComponent<Rigidbody2D> ();
			
		//aiming up while on the ground
		if (angle.y > -0.4f && transform.parent.parent.GetComponent<UnityStandardAssets._2D.PlatformerCharacter2D> ().is_grounded ()) {
			//don't knock back
		} else { //not grounded, aiming down
			//do knock back
			rb.AddForce (angle * -800);
		}

		RaycastHit2D[] test_hits = Physics2D.RaycastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), angle, 7f, toHitMask);
		Debug.DrawRay (fireLoc.position, 7 * angle);

		foreach (var test in test_hits) {
			if (test.collider.gameObject != gameObject && test.collider.gameObject != transform.parent.parent.gameObject) {
				if (test.collider.tag == "wall") {
					//Debug.Log (test.collider.tag);
					//return;
					hit_wall_first = true;
					break;
				} else if (test.collider.tag == "Player" || test.collider.tag == "crate") {
					Debug.Log (test.collider.tag);
					//hit_wall_first = false;
					break;
				}
			}
		}


		RaycastHit2D[] hits = Physics2D.CircleCastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), 2f,  angle , 5f, toHitMask);
		//Debug.DrawRay (fireLoc.position, 5 * angle);
		//Debug.Log ("fireloc is here");
		//Debug.Log (fireLoc.position);

		foreach (var hit in hits) {
			Vector2 dist = new Vector2 (hit.point.x - fireLoc.position.x, hit.point.y - fireLoc.position.y);

			if (hit.collider.gameObject.tag == "mid_wall") {
				return;
			}

			if (hit.rigidbody != null && dist.magnitude > 0) {
				if (hit.collider.gameObject != gameObject && hit.collider.gameObject != transform.parent.parent.gameObject 
					&& hit.collider.gameObject != transform.parent.gameObject) {
					//hit.collider.attachedRigidbody.AddForce (dist.normalized * (power * 10 / (2 * dist.magnitude)));
					if (hit.collider.gameObject.GetComponent<Inventory> () == null) {
						hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 5000, dist.normalized.y * 500));
					}

					if (hit.collider.gameObject.GetComponent<Inventory> () != null && !hit.collider.gameObject.GetComponent<Inventory> ().isImmovable) {
						hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 5000, dist.normalized.y * 500));
					}
				}
			}
		}

	}
}
