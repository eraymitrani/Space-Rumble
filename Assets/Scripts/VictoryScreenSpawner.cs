using System.Collections;
using System;
using TMPro;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryScreenSpawner : MonoBehaviour
{

    public GameObject Player;
    public GameObject[] Spawns = new GameObject[4];
    public Material[] Colors = new Material[4];
    public float spawnDelay = 10f;
    public float timeToTop = 3f;
    public TextMeshProUGUI winText;
    int winner, winnerStock = 0;
    string winString;

    void Start()
    {
        StartCoroutine(respawnPlayer());

        for (int i = 1; i < 5; i++)
        {
            if (TotalPlayerStocks.getPlayerStocks(i) > winnerStock)
            {
                winner = i;
                winnerStock = TotalPlayerStocks.getPlayerStocks(i);
            }
        }
        string color = "";
        switch (winner)
        {
            case 1: winText.color = PlayerControllers.Color1; color = PlayerControllers.Color1Name; break;
            case 2: winText.color = PlayerControllers.Color2; color = PlayerControllers.Color2Name; break;
            case 3: winText.color = PlayerControllers.Color3; color = PlayerControllers.Color3Name; break;
            case 4: winText.color = PlayerControllers.Color4; color = PlayerControllers.Color4Name; break;
        }
        winString = color + " won with " + winnerStock.ToString() + " lives left!";
        StartCoroutine(winTextDisp());

        foreach(DisplayScore ds in transform.parent.GetComponentsInChildren<DisplayScore>())
        {
            ds.maxStocksLeft = winnerStock;
        }
    }
    IEnumerator LoadSceneWithFade(int toLoad)
    {
        float fadeTime = GameObject.Find("Fader").GetComponentInChildren<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(toLoad);
        yield return null;
    }
    IEnumerator respawnPlayer()
    {
        int players = PlayerControllers.numOfPlayers();
        for (int i = 0; i < players; ++i)
        {
            GameObject player = Instantiate(Player, Spawns[i].transform);
            Spawns[i].GetComponentInChildren<TextMeshProUGUI>().color = Colors[i].color;
            Platformer2DUserControl uc = player.GetComponent<Platformer2DUserControl>();
            uc.player_num = i + 1;
            uc.enabled = false;
            foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.material = Colors[i];
            }
        }
        winText.enabled = true;
        yield return new WaitForSeconds(spawnDelay);
        StartCoroutine(LoadSceneWithFade(0));
    }
    IEnumerator winTextDisp()
    {
        yield return new WaitForSeconds(timeToTop);
        winText.text = winString;
    }
}
