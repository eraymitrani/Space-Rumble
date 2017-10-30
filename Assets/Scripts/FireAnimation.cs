using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAnimation : MonoBehaviour
{

    public Sprite[] sprites;
    private SpriteRenderer sr;
    public float animationDelay = 0.5f;

	// Use this for initialization
    //void Awake()
    //{
    //    sprites = Resources.LoadAll<Sprite>("Resources/Sprites/fire");
    //    Debug.Log(sprites.Length);

    //}
    void Start ()
	{
	    sr = GetComponent<SpriteRenderer>();
	    StartCoroutine(Animate());

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Animate()
    {
        while (true)
        {
            sr.sprite = sprites[0];
            yield return new WaitForSeconds(animationDelay);
            sr.sprite = sprites[1];
            yield return new WaitForSeconds(animationDelay);
            sr.sprite = sprites[2];
            yield return new WaitForSeconds(animationDelay);
            sr.sprite = sprites[3];
            yield return new WaitForSeconds(animationDelay);
            sr.sprite = sprites[4];
            yield return new WaitForSeconds(animationDelay);
            sr.sprite = sprites[5];
            yield return new WaitForSeconds(animationDelay);

        }

    }
}
