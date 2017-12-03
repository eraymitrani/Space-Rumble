using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyDeath : MonoBehaviour
{

    public GameObject a;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("HERE");

        if (other.gameObject.tag == "spike")
        {
            Debug.Log(other.gameObject.tag);

            Instantiate(a, transform.position, Quaternion.identity);
        }
    }
}
