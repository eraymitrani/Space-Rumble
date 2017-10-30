using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour
{

    //public int rotationOffset = 180;

    // Update is called once per frame
    void Update()
    {
        float rotZ = 90f;
        // subtracting the position of the player from the mouse position
        float x = Input.GetAxisRaw("joystick 1 analog 3");
        float y = -1 * Input.GetAxisRaw("joystick 1 analog 4");

        if (x != 0f|| y != 0f)
        {
            rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // find the angle in degrees
            x = 0;
            y = 0;


        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

    }
}
