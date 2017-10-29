using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour {

    public enum Facing
    {
        Up,
        Right,
        Down,
        Left
    }

    private bool canJump = true;
    private Facing direction;
    private Rigidbody2D rb;
    public float moveSpeed = 2.0f, jumpForce = 300f;
	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float input = Input.GetAxisRaw("Horizontal");
	    if (input > 0)
	    {
	        direction = Facing.Right;
	    }
	    else
	    {
	        direction = Facing.Left;
	    }
        rb.velocity = new Vector2(input * moveSpeed, 0);
	    if (Input.GetKeyDown(KeyCode.Space) && canJump) 
	    {
	        canJump = false;
	        rb.velocity += new Vector2(0, jumpForce);
	    }
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.collider.tag == "graound")
        //{
            
        //}
        canJump = true;
    }
 
}
