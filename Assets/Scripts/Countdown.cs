using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour {

    public GameObject explosion;
    public int countDownFrom = 3;

    TextMeshProUGUI countdownText;
    GameObject robot;
    PlayerSpawner playerSpawner;
	
	void Start () {
        robot = transform.Find("Countdown").gameObject;
        countdownText = GetComponentInChildren<TextMeshProUGUI>();
        playerSpawner = GetComponent<PlayerSpawner>();
        StartCoroutine(countdown());
	}
	
	IEnumerator countdown()
    {
        int counter = countDownFrom;
        while (counter > 0)
        {
            countdownText.text = counter.ToString();
            yield return new WaitForSeconds(1);
            counter--;
        }
        Instantiate(explosion, robot.transform.position, Quaternion.identity);
        robot.SetActive(false);
        playerSpawner.enablePlayers();
    }
}
