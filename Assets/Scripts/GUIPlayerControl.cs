using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GUIPlayerControl : MonoBehaviour {

    float rotZ, x, y, old_rotZ;

    public Vector2 angle_vec;
    public float angle;
    public int playerNum;
    public bool hitTarget = false;

    InputDevice controller;
    Transform arm;
    Transform fireLoc;
    Vector3 fireLocVec;
    GameObject windSquare;
    GameObject[] clones = new GameObject[10];

    void Awake()
    {
        controller = PlayerControllers.getPlayerController(playerNum);
        arm = transform.Find("Arm");
        fireLoc = arm.Find("Hose");
        fireLocVec = arm.Find("Hose").transform.position;
        windSquare = GameObject.Find("wind_pixel");
    }

    void Update()
    {
        if (controller.RightTrigger.IsPressed)
        {
            Shoot();
        }
		if (controller.LeftTrigger.WasPressed) {
			Burst ();
		}

        x = controller.RightStickX;
        y = controller.RightStickY;
        rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
        old_rotZ = rotZ;
        angle = rotZ;

        arm.rotation = Quaternion.Euler(0f, 0f, rotZ);
        angle_vec = new Vector2(Mathf.Cos(Mathf.Deg2Rad * rotZ), Mathf.Sin(Mathf.Deg2Rad * rotZ));
    }

    void Shoot()
    {
        float ang;
        ang = angle * Mathf.Deg2Rad;
        ang += Random.Range(-0.3f, 0.3f);

        GameObject clone = Instantiate(windSquare, fireLoc.position, Quaternion.identity);
        clone.GetComponent<Rigidbody2D>().velocity = 100 * new Vector2(Mathf.Cos(ang), Mathf.Sin(ang));
        clone.GetComponent<WindLifetime>().is_alive = true;

        if(Mathf.Abs(angle - 90) < 20)
        {
            hitTarget = true;
        }
    }

	void Burst(){
		float ang;
		ang = angle * Mathf.Deg2Rad;
		ang += Random.Range(-0.3f, 0.3f);

		if(Mathf.Abs(angle - 90) < 20)
		{
			hitTarget = true;
		}


		clones [0] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [1] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [2] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [3] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [4] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [5] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [6] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [7] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [8] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);
		clones [9] = Instantiate (windSquare, fireLoc.position, Quaternion.identity);

		float inc = -0.2f;

		foreach (var clone in clones) {
			ang = angle * Mathf.Deg2Rad;
			ang += inc;
			inc += 0.04f;

			clone.GetComponent<Rigidbody2D> ().velocity = 100 * new Vector2 (Mathf.Cos (ang), Mathf.Sin (ang));
			clone.GetComponent<WindLifetime> ().is_alive = true;
		}
	}
}
