﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets._2D;

public class ArmRotation : MonoBehaviour
{

    //public int rotationOffset = 180;
    float rotZ, x, y;

    private bool isRight;
    private PlatformerCharacter2D pc;
    // Update is called once per frame
    void Awake()
    {
        pc = GetComponentInParent<PlatformerCharacter2D>();
    }
    void Update()
    {
        isRight = pc.dir();
        // subtracting the position of the player from the mouse position
        if (isRight)
        {
             x = Input.GetAxisRaw("joystick 1 analog 3");
             y = -1 * Input.GetAxisRaw("joystick 1 analog 4");
        }
        else
        {
             x = -1 * Input.GetAxisRaw("joystick 1 analog 3");
             y = Input.GetAxisRaw("joystick 1 analog 4");
        }
    

        if (Mathf.Abs(x) >  0.2f|| Mathf.Abs(y) > 0.2f)
        {
            rotZ = Mathf.Atan2(y, x) * Mathf.Rad2Deg; // find the angle in degrees


        }
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

    }
}