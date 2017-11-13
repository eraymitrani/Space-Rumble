using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityStandardAssets._2D;

public class PlayerSpawner : MonoBehaviour {

    public GameObject Player;
    public GameObject[] Spawns = new GameObject[4];
    public Material[] Colors = new Material[4];
    public GameObject[] StockPanels = new GameObject[4];
    public string stockText = "<sprite=0>";

    ScoreManager scoreManager;

	void Start ()
    {
        scoreManager = GetComponent<ScoreManager>();

        int players = PlayerControllers.numOfPlayers();

        for (int i = 0; i < players; ++i)
        {
            GameObject player = Instantiate(Player, Spawns[i].transform);
            Platformer2DUserControl uc = player.GetComponent<Platformer2DUserControl>();
            uc.player_num = i + 1;
            uc.enabled = false;
            player.GetComponent<Inventory>().scoreManager = scoreManager;
            StockPanels[i].SetActive(true);
            Color stockBg = Color.black;
            foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
            {
                switch (i)
                {
                    case 0: sr.material = Colors[i];
                        Colors[i].color = PlayerControllers.Color1;
                        stockBg = PlayerControllers.Color1;
                        break;
                    case 1:
                        sr.material = Colors[i];
                        Colors[i].color = PlayerControllers.Color2;
                        stockBg = PlayerControllers.Color2;
                        break;
                    case 2:
                        sr.material = Colors[i];
                        Colors[i].color = PlayerControllers.Color3;
                        stockBg = PlayerControllers.Color3;
                        break;
                    case 3:
                        sr.material = Colors[i];
                        Colors[i].color = PlayerControllers.Color4;
                        stockBg = PlayerControllers.Color4;
                        break;
                }
            }
            stockBg.a = 0.5f;
            StockPanels[i].GetComponent<Image>().color = stockBg;
        }
	}

    public void respawnPlayer(int i)
    {
        GameObject player = Instantiate(Player, Spawns[i].transform);
        player.GetComponent<Platformer2DUserControl>().player_num = i + 1;
        player.GetComponent<Inventory>().scoreManager = scoreManager;

		player.GetComponent<Inventory> ().isImmovable = true;

        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.material = Colors[i];
        }
    }
    
    public void updateStock(int player, int num)
    {
        TextMeshProUGUI stock = StockPanels[player].GetComponentInChildren<TextMeshProUGUI>();
        stock.text = "";
        for (int i = 0; i < num; ++i)
            stock.text += stockText;
    }

    public void enablePlayers()
    {
        foreach(GameObject spawn in Spawns)
        {
            if (spawn == null)
                continue;
            if (spawn.GetComponentInChildren<Platformer2DUserControl>() == null)
                continue;
            spawn.GetComponentInChildren<Platformer2DUserControl>().enabled = true;
        }
    }
}
