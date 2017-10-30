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
	    fireLoc = transform.FindChild("Hose");
	    if (fireLoc == null)
	    {
	        Debug.LogError("bad");
	    }
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown("joystick 1 button 1"))
	    {
            Debug.Log("Shoot");
	        Shoot();
	    }
	}

    void Shoot()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(new Vector2(fireLoc.position.x, fireLoc.position.y), 5f, new Vector2(transform.forward.x, transform.forward.y), toHitMask);
        
        foreach (var hit in hits)
        {
            Debug.Log(hit.collider.tag);
            Debug.Log(hit.distance);
            if (hit.rigidbody != null && hit.distance != 0)
            {
                Debug.Log(hit.distance);
                hit.collider.attachedRigidbody.AddForce(Vector2.one * (power / hit.distance));
            }
        }
    }
}
