using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;
using InControl;

public class ArmRotation : MonoBehaviour
{

    //public int rotationOffset = 180;
    float rotZ, x, y, old_rotZ;

    public Vector2 angle_vec;
	public float angle;
    public InputDevice controller;

    private bool isRight;
    private bool wasRight = true;
    private PlatformerCharacter2D pc;

    // Update is called once per frame
    void Awake()
    {
        pc = GetComponentInParent<PlatformerCharacter2D>();


    }

    void Start()
    {
        controller = GetComponentInParent<Platformer2DUserControl>().controller;
    }

    void Update()
    {

        if (controller == null)
        {
			controller = GetComponentInParent<Platformer2DUserControl> ().controller;
			return;
		}

        x = controller.RightStickX;
        y = controller.RightStickY;

		if (Mathf.Abs (controller.LeftStickX) < 0.01f) {
			isRight = wasRight;
		} else if (controller.LeftStickX > 0) {
			isRight = true;
		} else {
			isRight = false;
		}



        if (Mathf.Abs(x) < 0.01 && Mathf.Abs(y) < 0.01)
        {
            if (isRight != wasRight)
            {
                old_rotZ += 180;
                //Debug.Log ("flipping");
            }
            rotZ = old_rotZ;
        }
        else
        {
            if (isRight)
            {
                rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                //angle_vec = new Vector2 (x, y);
            }
            else
            {
                rotZ = Mathf.Atan2(-y, -x) * Mathf.Rad2Deg;
                //angle_vec = new Vector2 (-x, -y);
            }
            old_rotZ = rotZ;
        }

		angle = rotZ;
		if (!isRight) {
			angle += 180;
		}

        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
		if (!isRight) {
			rotZ += 180;
		}
		angle_vec = new Vector2 (Mathf.Cos (Mathf.Deg2Rad * rotZ), Mathf.Sin (Mathf.Deg2Rad * rotZ));
        wasRight = isRight;
        
    }
}
//        }

//			controller = GetComponentInParent<Platformer2DUserControl> ().controller;
//			return;
//		}

//        isRight = pc.dir();
//		controller = GetComponentInParent<Platformer2DUserControl> ().controller;
//		x = controller.RightStickX;
//		y = controller.RightStickY;

//		if (isRight) {
//			rotZ = Mathf.Atan2 (y, x) * Mathf.Rad2Deg;
//			angle_vec = new Vector2 (x, y);
//		} else {
//			rotZ = Mathf.Atan2 (-y, -x) * Mathf.Rad2Deg;
//			angle_vec = new Vector2 (-x, -y);
//		}
//		transform.rotation = Quaternion.Euler(0f, 0f, rotZ);


//    }
//}
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

