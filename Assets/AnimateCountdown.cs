using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCountdown : MonoBehaviour
{

    private Animator an;
	// Use this for initialization
	void Start ()
	{
	    an = GetComponent<Animator>();
	    StartCoroutine(startEnumerator());
	}

    IEnumerator startEnumerator()
    {
        yield return new WaitForSeconds(1.95f);
        an.SetBool("StartMove", true);
    }
}
