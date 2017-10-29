using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour
{

    //public int rotationOffset = 180;

    // Update is called once per frame
    void Update()
    {
        // subtracting the position of the player from the mouse position
        float x = Input.GetAxisRaw("Horizontal_R1");
        float y = -1 * Input.GetAxisRaw("Vertical_R1");

        if (x != 0f|| y != 0f)
        {
            float rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // find the angle in degrees
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ );


        }
    }
}
