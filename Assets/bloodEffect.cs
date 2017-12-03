using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bloodEffect : MonoBehaviour
{
    private ParticleSystem ps;
	// Use this for initialization
	void Awake()
	{
	    ps = GetComponent<ParticleSystem>();
        ps.Stop();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Player")
        {
            ps.Play();
            Invoke("CD", 0.2f);
        }
    }

    void CD()
    {
        ps.Stop();
    }
}
