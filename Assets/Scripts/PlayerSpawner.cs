using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour {

    public GameObject Player;
    public GameObject[] Spawns = new GameObject[4];

    ScoreManager scoreManager;

	void Start ()
    {
        scoreManager = GetComponent<ScoreManager>();

        int players = PlayerControllers.numOfPlayers();

        for(int i = 0; i < players; ++i)
        {
            GameObject player = Instantiate(Player, Spawns[i].transform);
            player.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().player_num = i + 1;
            player.GetComponent<Inventory>().scoreManager = scoreManager;
        }
	}
}
