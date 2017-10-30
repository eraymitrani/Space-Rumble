using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public LayerMask toHitMask;
    private Transform fireLoc;

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
		if (Input.GetKeyDown("joystick 1 button 1") || Input.GetKeyDown("s"))
	    {
           // Debug.Log("Shoot");
	        Shoot();
	    }
	}

    void Shoot()
    {
		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
        Vector2 angle = new Vector2(transform.position.x,transform.position.y);
        angle.Normalize();
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
                Debug.Log(hit.point.x);
                Debug.Log(fireLoc.position.x);
                if (hit.point.x < fireLoc.position.x )
                {
                    hit.collider.attachedRigidbody.AddForce(angle * (power / dist.magnitude));

                }
                else
                {
                    hit.collider.attachedRigidbody.AddForce(-1*angle * (power / dist.magnitude));

                }
            }
        }

    }
}
