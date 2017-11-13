using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScore : MonoBehaviour
{
    public float t, timeToTop = 3f;
    public int playerNum;
    private int numStocksLeft = 3;
    private Vector2 start, destination;
	// Use this for initialization
	void Start ()
	{
	    start = transform.position;
        numStocksLeft = TotalPlayerStocks.getPlayerStocks(playerNum);
        destination = new Vector2(transform.position.x, transform.position.y + numStocksLeft);
	}
	
	// Update is called once per frame
	void Update () {
	    t += Time.deltaTime / timeToTop;
	    transform.position = Vector3.Lerp(start, destination, t);
    }
}
