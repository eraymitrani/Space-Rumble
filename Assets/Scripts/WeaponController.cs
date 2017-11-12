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
	int consume_rate = 75;		//higher is faster
	int recharge_rate = 50;		//lower is slower

	Rigidbody2D rb;
	float x, y;
	bool is_playing = false;

	public bool is_powered_up = false;
	float powered_up_timer = 0;
	float powered_up_time = 3;

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
		Debug.Log (fireLoc.position);
		Debug.Log (fireLocVec);
	    
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponentInParent<ArmRotation> ().controller == null) {
			return;
		}

		//if (Input.GetKeyDown("joystick 1 button 1") || Input.GetKeyDown("s"))

		if (GetComponentInParent<ArmRotation>().controller.LeftTrigger.WasPressed){
			Debug.Log ("Burst");
			Burst ();
		} else if (GetComponentInParent<ArmRotation>().controller.RightTrigger.IsPressed){
            //Debug.Log("Shoot");
	        Shoot();
		} else {
			//refuel
			if (is_powered_up) {
				fuel = 100;
			}

			if (fuel > 100) {
				fuel = 100;
			} else {
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

    void Shoot()
    {
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

		RaycastHit2D[] hits = Physics2D.CircleCastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), 2f,  angle , 5f, toHitMask);
		Debug.DrawRay (fireLoc.position, 5 * angle);
		Debug.Log ("fireloc is here");
		Debug.Log (fireLoc.position);

        foreach (var hit in hits)
        {
			//if (hit.collider.gameObject.tag == "wall") {
			Vector2 dist = new Vector2 (hit.point.x - fireLoc.position.x, hit.point.y - fireLoc.position.y);
			//	Debug.Log (hit.distance);
			//}

            //            Debug.Log(hit.collider.tag);
            //            Debug.Log(hit.distance);
            if (hit.rigidbody != null && dist.magnitude > 0)
            {
				//Debug.Log("hit point is " + (hit.point.x));
				//Debug.Log("fire loc is " + (fireLoc.position.x));
//                if (hit.point.x < fireLoc.position.x )
//                {
//
//                    hit.collider.attachedRigidbody.AddForce(angle * (power / dist.magnitude));
//
//                }
//                else
//                {
//                    hit.collider.attachedRigidbody.AddForce(-1*angle * (power / dist.magnitude));
//
//                }

				if (hit.collider.gameObject != gameObject && hit.collider.gameObject != transform.parent.parent.gameObject) {
					//hit.collider.attachedRigidbody.AddForce (dist.normalized * (power * 10 / (2 * dist.magnitude)));

					if (hit.collider.gameObject.GetComponent<Inventory> () == null) {
						hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 500, dist.normalized.y * 50));
					}

					if (!hit.collider.gameObject.GetComponent<Inventory> ().isImmovable && hit.collider.gameObject.GetComponent<Inventory> () != null) {
						hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 500, dist.normalized.y * 50));
						//Debug.Log (hit.collider.tag);
					}
				}
            }
        }

		//Debug.Log (angle.x.ToString() + " " + angle.y.ToString());
		//transform.parent.parent.GetComponent<Rigidbody2D> ().AddForce(new Vector2(angle.x * 0.5 * -self_power, angle.y * -self_power));

		rb = transform.parent.parent.GetComponent<Rigidbody2D> ();
		y = angle.y * -5 * GetComponentInParent<ArmRotation> ().controller.RightTrigger.Value;

		if (y > 0) {
			rb.velocity = new Vector2 (rb.velocity.x, 3);
		} else {
			//rb.velocity = new Vector2 (rb.velocity.x, -7);
		}

		if (Mathf.Abs(rb.velocity.y) >= 1f) { //in air
			rb.AddForce(angle * -100);
		}
			
    }

	void Burst(){
		if (fuel < 50) {
			return;
		} else {
			fuel -= 50;
			GetComponent<WindEmitter> ().BurstParticles ();
		}


		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
		Vector2 angle = GetComponentInParent<ArmRotation> ().angle_vec;

		RaycastHit2D[] hits = Physics2D.CircleCastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), 2f,  angle , 5f, toHitMask);
		Debug.DrawRay (fireLoc.position, 5 * angle);
		Debug.Log ("fireloc is here");
		Debug.Log (fireLoc.position);

		foreach (var hit in hits) {
			Vector2 dist = new Vector2 (hit.point.x - fireLoc.position.x, hit.point.y - fireLoc.position.y);

			if (hit.rigidbody != null && dist.magnitude > 0) {
				if (hit.collider.gameObject != gameObject && hit.collider.gameObject != transform.parent.parent.gameObject) {
					//hit.collider.attachedRigidbody.AddForce (dist.normalized * (power * 10 / (2 * dist.magnitude)));
					if (hit.collider.gameObject.GetComponent<Inventory> () == null) {
						hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 5000, dist.normalized.y * 500));
					}

					if (!hit.collider.gameObject.GetComponent<Inventory> ().isImmovable) {
						hit.collider.attachedRigidbody.AddForce (new Vector2 (dist.normalized.x * 5000, dist.normalized.y * 500));
					}
				}
			}
		}

		rb = transform.parent.parent.GetComponent<Rigidbody2D> ();
		y = angle.y * -5 * GetComponentInParent<ArmRotation> ().controller.RightTrigger.Value;

		if (y > 0) {
			rb.velocity = new Vector2 (rb.velocity.x, 3);
		} else {
			//rb.velocity = new Vector2 (rb.velocity.x, -7);
		}

//		if (Mathf.Abs(rb.velocity.y) >= 1f) { //in air
//			rb.AddForce(angle * -1000);
//		}

		if (angle.y < -0.5) {
			Debug.Log (angle.y);
			rb.AddForce (angle * -1000);
		}
	}
}
