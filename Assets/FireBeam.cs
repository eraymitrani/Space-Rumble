using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBeam : MonoBehaviour
{
    public LayerMask mask;
    private AudioSource aus;
    public AudioClip ac;
    private float fireRateMax = 5f;
    private LineRenderer lr;
    private float fireRateMin = 2.5f;
    private Animator an;
    private bool laserOn = true;
	// Use this for initialization
	void Start ()
	{
	    an = GetComponent<Animator>();
	    an.enabled = false;
	    lr = GetComponent<LineRenderer>();
	    StartCoroutine(FireLaser());
	    aus = Camera.main.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

    }
    IEnumerator FireLaser()
    {
        yield return new WaitForSeconds(2.2f);
        lr.SetPosition(0, new Vector2(transform.position.x, transform.position.y - 0.5f));
        yield return new WaitForSeconds(0.7f);
        aus.PlayOneShot(ac);
        yield return new WaitForSeconds(0.1f);
        lr.enabled = true;
        lr.SetPosition(1, new Vector2(transform.position.x, transform.position.y - 12f));
        Invoke("Disable", 0.1f);

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(fireRateMin, fireRateMax));
            GetComponent<Animator>().enabled = false;
            lr.SetPosition(0, new Vector2(transform.position.x, transform.position.y - 0.5f));
            yield return new WaitForSeconds(0.7f);
            aus.PlayOneShot(ac);
            yield return new WaitForSeconds(0.1f);
            lr.enabled = true;
            Ray2D ray = new Ray2D(transform.position, Vector2.down);
            RaycastHit2D hit;
            hit = Physics2D.Raycast(ray.origin, Vector2.down, Mathf.Infinity, mask);
            if (hit)
            {
                lr.SetPosition(1, hit.point);
                if (hit.collider.tag == "Player")
                {
                    hit.collider.GetComponent<Inventory>().Damage(100);
					//Debug.Log ("HIT A PLAYER");
					//Debug.Log (hit.collider.GetComponent<Inventory> ().Get_Hp ());
                }
            }
            else lr.SetPosition(1, new Vector2(transform.position.x, transform.position.y - 12f));


            Invoke("Disable", 0.1f);
            
            


            yield return null;
        }
        
        
    }

    void Disable()
    {
        lr.enabled = false;
        GetComponent<Animator>().enabled = true;

    }
}
