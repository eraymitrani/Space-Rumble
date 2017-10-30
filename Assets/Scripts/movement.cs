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

    public bool canJump = true;
    private Facing direction;
    private Rigidbody rb;
    private Inventory inv;
    public float moveSpeed = 2.0f, jumpForce = 300f;
	// Use this for initialization
	void Start ()
	{
	    rb = GetComponent<Rigidbody>();
	    inv = GetComponent<Inventory>();
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
	    if (Input.GetKeyDown("joystick 1 button 0") && canJump) 
	    {
	        rb.velocity += new Vector3(0, jumpForce, 0);
	        canJump = false;

	    }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "ground")
        {
            canJump = true;

        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hazard")
        {
            inv.Damage(1);
        }
    }
 
}
