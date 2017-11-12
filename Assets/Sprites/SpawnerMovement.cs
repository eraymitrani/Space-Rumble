using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawnerMovement : MonoBehaviour
{
    
    public float speed = 1.0f, distanceZ, distaneX, distanceY, startIn, endIn;
    private Transform t;
    private Vector3 pos1, pos2;
    private bool start;
    void Start()
    {
        t = GetComponent<Transform>();
        pos1 = t.position;
        pos2 = t.position;
        pos2.z += distanceZ;
        pos2.y += distanceY;
        pos2.x += distaneX;
        Invoke("Begin", startIn);
        Invoke("End", endIn);
    }
    void Update()
    {
        if (start)
        {
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1.0f));
        }
    }

    void Begin()
    {
        start = true;
    }

    void End()
    {
        start = false;
    }

    public bool isMoving()
    {
        return start;
    }
}