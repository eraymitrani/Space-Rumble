using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public int stocks = 3;
    public TextMeshProUGUI winText;
    public float delayBeforeChange = 2f;
    private bool[] alive;

    int num_scenes = 4;

    int[] playerScores;

    PlayerSpawner playerSpawner;
    int numPlayers = PlayerControllers.numOfPlayers();
    bool[] playersDead;

    // Use this for initialization
    void Start ()
    {
        playerScores = new int[numPlayers];
        playersDead = new bool[numPlayers];
        winText.text = "";
        playerSpawner = GetComponent<PlayerSpawner>();
    }
	
    public void addScore(int player, int score)
    {
        playerScores[player - 1] += score;
        playerSpawner.updateStock(player - 1, stocks + playerScores[player - 1]);
        if (playerScores[player - 1] + stocks <= 0)
        {
            playersDead[player - 1] = true;
            alive = Array.FindAll(playersDead, a => a == false);
            if (alive.Length <= 1)
                announceWinner();
            return;
        }
        //assuming this means they died
        StartCoroutine(RespawnPlayer(player));
    }

    public bool[] getDead()
    {
        return playersDead;
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
        //SceneManager.LoadScene("MainMenu");
		int next_scene = SceneManager.GetActiveScene().buildIndex + 1;
		if (next_scene < num_scenes) {
			SceneManager.LoadScene (next_scene);
		} else {
			SceneManager.LoadScene ("MainMenu");
		}
    }

    IEnumerator RespawnPlayer(int i)
    {
        yield return new WaitForSeconds(2);
        playerSpawner.respawnPlayer(i - 1);
    }
}
