using System.Collections;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour {

    public int stocks = 3;
    public TextMeshProUGUI winText;
    public float delayBeforeChange = 2f;

	int num_scenes = 5;

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
            bool[] alive = Array.FindAll(playersDead, a => a == false);
            if (alive.Length <= 1)
                announceWinner();
            return;
        }
        //assuming this means they died
        StartCoroutine(RespawnPlayer(player));
    }

    //Returns -1 for non-existant player
    public int getStocks(int player)
    {
        if (player > numPlayers)
            return -1;
        return stocks + playerScores[player - 1];
    }

    public void announceWinner()
    {
        int[] sortedScores = new int[numPlayers];
        Array.Copy(playerScores, sortedScores, numPlayers);
        Array.Sort(sortedScores);
        Array.Reverse(sortedScores);

        int wonPlayer = Array.FindIndex(playerScores, a => a == sortedScores[0]) + 1;
        string color = "";
        switch (wonPlayer)
        {
            case 1: winText.color = PlayerControllers.Color1; color = PlayerControllers.Color1Name; break;
            case 2: winText.color = PlayerControllers.Color2; color = PlayerControllers.Color2Name; break;
            case 3: winText.color = PlayerControllers.Color3; color = PlayerControllers.Color3Name; break;
            case 4: winText.color = PlayerControllers.Color4; color = PlayerControllers.Color4Name; break;
        }
        winText.text = color + " Won!";
        StartCoroutine(delayReset());

        for (int i = 0; i < numPlayers; i++)
        {
            TotalPlayerStocks.addStocks(i + 1, stocks + playerScores[i]);
        }
    }

    IEnumerator delayReset()
    {
        yield return new WaitForSeconds(delayBeforeChange);
        //SceneManager.LoadScene("MainMenu");
		int next_scene = SceneManager.GetActiveScene().buildIndex + 1;
		if (next_scene < num_scenes)
		{
		    StartCoroutine(LoadSceneWithFade(next_scene));
		} else
		{
		    StartCoroutine(LoadSceneWithFade(0));
		}
    }
    IEnumerator LoadSceneWithFade(int toLoad)
    {
        float fadeTime = GameObject.Find("Fader").GetComponentInChildren<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(toLoad);
        yield return null;
    }
    IEnumerator RespawnPlayer(int i)
    {
        yield return new WaitForSeconds(2);
        playerSpawner.respawnPlayer(i - 1);
    }
}
