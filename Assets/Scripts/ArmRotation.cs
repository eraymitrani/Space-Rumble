using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using InControl;

public class ArmRotation : MonoBehaviour
{

    //public int rotationOffset = 180;
    float rotZ, x, y, old_rotZ;
	public Vector2 angle_vec;
	public InputDevice controller;

    private bool isRight;
	private bool wasRight = true;
    private PlatformerCharacter2D pc;

    // Update is called once per frame
    void Awake()
    {
        pc = GetComponentInParent<PlatformerCharacter2D>();


    }
	void Start(){
		controller = GetComponentInParent<Platformer2DUserControl> ().controller;
	}
    void Update()
    {
		
		if (controller == null) {
			controller = GetComponentInParent<Platformer2DUserControl> ().controller;
			return;
		}

        isRight = pc.dir();
//        // subtracting the position of the player from the mouse position
//        if (isRight)
//        {
//             x = Input.GetAxisRaw("joystick 1 analog 3");
//             y = -1 * Input.GetAxisRaw("joystick 1 analog 4");
//        }
//        else
//        {
//             x = -1 * Input.GetAxisRaw("joystick 1 analog 3");
//             y = Input.GetAxisRaw("joystick 1 analog 4");
//        }
//    
//
//        if (Mathf.Abs(x) >  0.2f|| Mathf.Abs(y) > 0.2f)
//        {
//            rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // find the angle in degrees
//
//
//        }

		controller = GetComponentInParent<Platformer2DUserControl> ().controller;
		x = controller.RightStickX;
		y = controller.RightStickY;



		if (Mathf.Abs (x) < 0.01 && Mathf.Abs (y) < 0.01) {
			if (isRight != wasRight) {
				old_rotZ += 180;
				//Debug.Log ("flipping");
			}
			rotZ = old_rotZ;
		} else {
			if (isRight) {
				rotZ = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
				//angle_vec = new Vector2 (x, y);
			} else {
				rotZ = Mathf.Atan2 (-y, -x) * Mathf.Rad2Deg;
				//angle_vec = new Vector2 (-x, -y);
			}
			old_rotZ = rotZ;
		}
				
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
		wasRight = isRight;
    }
}
