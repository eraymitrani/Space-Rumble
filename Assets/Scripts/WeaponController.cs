using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public LayerMask toHitMask;
    private Transform fireLoc;

    public float power = -200f;
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
            Debug.Log("Shoot");
	        Shoot();
	    }
	}

    void Shoot()
    {
		//startpos, radius, direction (change this to be direction of leafblower/right stick), max_distance
		RaycastHit2D[] hits = Physics2D.CircleCastAll (new Vector2 (fireLoc.position.x, fireLoc.position.y), 2f, Vector2.right, 10f); //, toHitMask);

        foreach (var hit in hits)
        {
			//Debug.Log (hit.point);
			if (hit.collider.gameObject.tag == "wall") {
				Vector2 dist = new Vector2 (hit.point.x - fireLoc.position.x, hit.point.y - fireLoc.position.y);
//				Debug.Log (dist.magnitude);
//				Debug.Log (dist.x);
				Debug.Log (hit.distance);
			}

//            Debug.Log(hit.collider.tag);
//            Debug.Log(hit.distance);
//            if (hit.rigidbody != null && hit.distance != 0)
//            {
//                Debug.Log(hit.distance);
//                hit.collider.attachedRigidbody.AddForce(Vector2.one * (power / hit.distance));
//            }
        }

    }
}
