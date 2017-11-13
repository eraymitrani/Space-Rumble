using System.Collections;
using System;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.SceneManagement;

public class VictoryScreenSpawner : MonoBehaviour
{

    public GameObject Player;
    public GameObject[] Spawns = new GameObject[4];
    public Material[] Colors = new Material[4];
    public float spawnDelay = 3;

    void Start()
    {
        StartCoroutine(respawnPlayer());
    }

    IEnumerator respawnPlayer()
    {
        yield return new WaitForSeconds(spawnDelay);
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

        yield return new WaitForSeconds(spawnDelay);
        SceneManager.LoadScene("MainMenu");
    }
}
