using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public LayerMask toHitMask;
    private Transform fireLoc;

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
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(fireLoc.position, transform.forward);
        if (Physics.Raycast(ray, out hit, 200))
        {
            Debug.DrawLine(ray.origin, hit.point);
            Debug.Log("Hello");
        }
    }
}
