using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class WeaponController : MonoBehaviour
{

    public LayerMask toHitMask;
    private Transform fireLoc;

	//InputDevice controller;

//	void Start(){
//		controller = GetComponentInParent<ArmRotation>().controller;
//	}

    public float power = 800f;
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
	    }
	}

    void Shoot()
    {
		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
        Vector2 angle = new Vector2(transform.position.x,transform.position.y);
		angle.Normalize();

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

    }
}
