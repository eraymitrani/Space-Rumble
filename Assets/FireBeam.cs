using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    public LayerMask mask;
    private float fireRateMax = 5f;
    private LineRenderer lr;
    private float fireRateMin = 2.5f;

    private bool laserOn = true;
	// Use this for initialization
	void Start ()
	{
	    lr = GetComponent<LineRenderer>();
	    StartCoroutine(FireLaser());
	}
	
	// Update is called once per frame
	void Update () {
	    lr.SetPosition(0, transform.position);

    }
    IEnumerator FireLaser()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(fireRateMin, fireRateMax));
            GetComponent<Animator>().enabled = false;
            yield return new WaitForSeconds(0.8f);
            lr.enabled = true;
            Ray2D ray = new Ray2D(transform.position, transform.up * -1);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, Vector2.down, Mathf.Infinity, mask);
            if (hit.collider.tag == "Player")
            {
                hit.collider.GetComponent<Inventory>().Damage(100);
            }


            Invoke("Disable", 0.1f);
            
            lr.SetPosition(1, hit.point);
            


            yield return null;
        }
        
        
    }

    void Disable()
    {
        lr.enabled = false;
        GetComponent<Animator>().enabled = true;

    }
}
