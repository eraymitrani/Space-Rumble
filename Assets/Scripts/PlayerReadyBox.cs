using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReadyBox : MonoBehaviour {

    public bool ready = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Hit");
        if(col.gameObject.name.Equals("CratePink"))
        {
            ready = true;
        }
    }
}
