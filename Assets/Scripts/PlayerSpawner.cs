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

        for (int i = 0; i < players; ++i)
        {
            GameObject player = Instantiate(Player, Spawns[i].transform);
            player.GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>().player_num = i + 1;
            player.GetComponent<Inventory>().scoreManager = scoreManager;
            switch (i)
            {
                case 0: player.GetComponentInChildren<SpriteRenderer>().color = PlayerControllers.Color1; break;
                case 1: player.GetComponentInChildren<SpriteRenderer>().color = PlayerControllers.Color2; break;
                case 2: player.GetComponentInChildren<SpriteRenderer>().color = PlayerControllers.Color3; break;
                case 3: player.GetComponentInChildren<SpriteRenderer>().color = PlayerControllers.Color4; break;
            }
        }
	}
}
