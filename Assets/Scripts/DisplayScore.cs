using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayScore : MonoBehaviour
{
    public float t, timeToTop = 3f;
    public int maxStocksLeft = 5;
    public int playerNum;

    int numStocksLeft = 3;
    Vector2 start, destination;
    TextMeshProUGUI stockText;

	// Use this for initialization
	void Start ()
	{
	    start = transform.position;
        numStocksLeft = TotalPlayerStocks.getPlayerStocks(playerNum);
        destination = new Vector2(transform.position.x, 6f * ((float)numStocksLeft / (float)maxStocksLeft) - 3f);
        stockText = transform.GetComponentInChildren<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
	    t += Time.deltaTime / timeToTop;
        t = Mathf.Clamp01(t);
	    transform.position = Vector3.Lerp(start, destination, t);
        stockText.text = Mathf.Floor(numStocksLeft * t).ToString() + " <sprite=0>";
    }
}
