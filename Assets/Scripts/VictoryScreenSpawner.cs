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
    public TextMeshProUGUI winText;
    private int winner, winnerStock = 0;
    void Start()
    {
        StartCoroutine(respawnPlayer());
        //for (int i = 1; i < 5; i++)
        //{
        //    if (TotalPlayerStocks.getPlayerStocks(i) > winnerStock)
        //    {
        //        winner = i;
        //        winnerStock = TotalPlayerStocks.getPlayerStocks(i);
        //    }
        //}
        //winText.text = "Player " + winner.ToString() + " won with " + winnerStock.ToString() + " stocks";
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
        //yield return new WaitForSeconds(spawnDelay);
        int players = PlayerControllers.numOfPlayers();
        for (int i = 0; i < players; ++i)
        {
            GameObject player = Instantiate(Player, Spawns[i].transform);
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
}
