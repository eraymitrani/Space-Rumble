﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private List<GameObject> Avaliable;
    private GameObject[] targetPool;
    private ScoreManager sc;
    private GameObject target;
    // Use this for initialization
    void Start ()
    {
        sc = GetComponent<ScoreManager>();
        Avaliable = new List<GameObject>();
        an = GetComponent<Animator>();
	    an.enabled = false;
	    lr = GetComponent<LineRenderer>();
	    StartCoroutine(FireLaser());
	    aus = Camera.main.GetComponent<AudioSource>();
       // Invoke("BeginWithDelay", 0.2f);
	}
	
	// Update is called once per frame
    //void BeginWithDelay()
    //{
    //    targetPool = GameObject.FindGameObjectsWithTag("Player");
    //    foreach (GameObject t in targetPool)
    //    {
    //        Avaliable.Add(t);
    //    }
    //}
	void Update () {
	    //targetPool = GameObject.FindGameObjectsWithTag("Player");
     //   Debug.Log(targetPool.Length);
	    //try
	    //{
	    //    target = targetPool[Random.Range(0, targetPool.Length)];
	    //}
	    //catch (IndexOutOfRangeException)
	    //{
	       
	    //}

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
            //targetPool = new GameObject[0];
            //bool[] dead = sc.getDead();
            //for (int i = 0; i < targetPool.Length; i++)
            //{
            //    if (dead[i])
            //    {
            //        Avaliable.Remove(targetPool[i]);
            //    }
            //}
            //target = Avaliable[Random.Range(0, Avaliable.Count)];
            if (target == null)
            {
                target = GameObject.FindWithTag("Player");
            }
            target.GetComponent<SpriteRenderer>().color = Color.red;
            target.GetComponent<SpriteRenderer>().enabled = true;
            yield return new WaitForSeconds(Random.Range(fireRateMin, fireRateMax));
            GetComponent<Animator>().enabled = false;
            lr.SetPosition(0, new Vector2(transform.position.x, transform.position.y - 0.5f));
            yield return new WaitForSeconds(0.7f);
            aus.PlayOneShot(ac);
            yield return new WaitForSeconds(0.1f);
            target.GetComponent<SpriteRenderer>().color = Color.white;
            target.GetComponent<SpriteRenderer>().enabled = false;
            lr.enabled = true;
            Vector2 targetLoc = new Vector2(target.transform.position.x, target.transform.position.y);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetLoc, Mathf.Infinity, mask); ;
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
