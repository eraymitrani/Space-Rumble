using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public int numPlayers = 2;
    public TextMeshProUGUI winText;
    public float delayBeforeChange = 2f;

    int[] playerScores;

	// Use this for initialization
	void Start ()
    {
        playerScores = new int[numPlayers];
        winText.text = "";

    }
	
    public void addScore(int player, int score)
    {
        playerScores[player - 1] += score;
    }

    public void announceWinner()
    {
        int[] sortedScores = new int[numPlayers];
        Array.Copy(playerScores, sortedScores, numPlayers);
        Array.Sort(sortedScores);
        Array.Reverse(sortedScores);
        winText.text = "Player " + (Array.FindIndex(playerScores, a => a == sortedScores[0]) + 1).ToString() + " Won!";
        StartCoroutine(delayReset());
    }

    IEnumerator delayReset()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        SceneManager.LoadScene("MainMenu");
    }
}
