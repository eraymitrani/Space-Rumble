using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class WeaponController : MonoBehaviour
{

    public LayerMask toHitMask;
    private Transform fireLoc;
	//public GameObject wind_square;

	public float fuel = 100;
	int consume_rate = 75;		//higher is faster
	int recharge_rate = 25;		//lower is slower

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
	    if (fireLoc == null)
	    {
	        Debug.LogError("bad");
	    }
	    
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponentInParent<ArmRotation> ().controller == null) {
			return;
		}

		//if (Input.GetKeyDown("joystick 1 button 1") || Input.GetKeyDown("s"))
		if (GetComponentInParent<ArmRotation>().controller.RightTrigger.IsPressed){
            Debug.Log("Shoot");
	        Shoot();
		} else {
			//refuel
			if (fuel > 100) {
				fuel = 100;
			} else {
				fuel += Time.deltaTime * recharge_rate;
			}
		}
	}

    void Shoot()
    {
		fuel -= Time.deltaTime * consume_rate;
		if (fuel < 0) {
			fuel = 0;
			return;
		}
		Debug.Log (fuel.ToString());

		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
		Vector2 angle = new Vector2(transform.position.x,transform.position.y);
		angle.Normalize();

		//angle = GetComponentInParent<ArmRotation> ().angle_vec;
		angle = new Vector2(Mathf.Cos(GetComponentInParent<ArmRotation>().angle), Mathf.Sin(GetComponentInParent<ArmRotation>().angle));
		angle = GetComponentInParent<ArmRotation> ().angle_vec;

		RaycastHit2D[] hits = Physics2D.CircleCastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), 2f,  angle , 10f, toHitMask);

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
				Debug.Log("hit point is " + (hit.point.x));
				Debug.Log("fire loc is " + (fireLoc.position.x));
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

				hit.collider.attachedRigidbody.AddForce (dist.normalized * (power / ( 2 * dist.magnitude)));
            }
        }

		Debug.Log (angle.x.ToString() + " " + angle.y.ToString());
		transform.parent.parent.GetComponent<Rigidbody2D> ().AddForce(angle * -self_power);

    }
}
