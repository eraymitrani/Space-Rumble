using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerAssign : MonoBehaviour {

    public GameObject[] Player = new GameObject[4];
    public GameObject[] PlayerNonGUI = new GameObject[4];
    public GameObject TrackSelect;
    public float deadZone = 0.5f;
    public Color[] PlayerColors;
    public Material[] PlayerMaterials = new Material[4];
    public bool allowSinglePlayer = false;
    public bool skipSceneSelect = true;

    bool[] p = new bool[4];
    bool[] pickedColor = new bool[4];
    bool[] pReady = new bool[4];
    int[] pColorI = new int[] { -1, -1, -1, -1 };
    bool[] pMoved = new bool[4];
    HashSet<InputDevice> usedControllers = new HashSet<InputDevice>();

    void Awake()
    {
        PlayerControllers.reset();
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            //Picked color, go to ready screen
            if (p[i] && !pickedColor[i] && PlayerControllers.getPlayerController(i+1).Action1.WasPressed)
            {
                pickedColor[i] = true;
                Player[i].transform.Find("Joined").gameObject.SetActive(false);
                Player[i].transform.Find("Ready").gameObject.SetActive(true);
                PlayerNonGUI[i].gameObject.SetActive(true);
            }

            //See if they want to change color
            if(p[i] && pickedColor[i] && !pReady[i] && PlayerControllers.getPlayerController(i + 1).Action2.WasPressed)
            {
                pickedColor[i] = false;
                Player[i].transform.Find("Joined").gameObject.SetActive(true);
                Player[i].transform.Find("Ready").gameObject.SetActive(false);
                PlayerNonGUI[i].gameObject.SetActive(false);
            }

            //Did ready action
            if (p[i] && pickedColor[i])
            {
                if (PlayerNonGUI[i].GetComponentInChildren<PlayerReadyBox>().ready)
                {
                    Player[i].transform.Find("Ready").Find("ReadyText").gameObject.SetActive(true);
                    pReady[i] = true;
                }
            }

            //Go to color picker
            if (p[i] && !pickedColor[i])
            {
                PlayerColorCheck(PlayerControllers.getPlayerController(i+1), ref pMoved[i], ref pColorI[i], i + 1);
            }
        }

        //Register new controller
        if (InputManager.ActiveDevice.Action1.WasPressed && !usedControllers.Contains(InputManager.ActiveDevice))
        {
            for (int i = 0; i < 4; i++)
            {
                if (!p[i])
                {
                    switch (i)
                    {
                        case 0: PlayerControllers.Player1 = InputManager.ActiveDevice; break;
                        case 1: PlayerControllers.Player2 = InputManager.ActiveDevice; break;
                        case 2: PlayerControllers.Player3 = InputManager.ActiveDevice; break;
                        case 3: PlayerControllers.Player4 = InputManager.ActiveDevice; break;
                    }
                    usedControllers.Add(InputManager.ActiveDevice);
                    p[i] = true;
                    Player[i].transform.Find("Join").gameObject.SetActive(false);
                    Player[i].transform.Find("Joined").gameObject.SetActive(true);
                    int newColor = 0;
                    while (Array.Exists<int>(pColorI, n => n == newColor))
                    {
                        newColor = (newColor + 1) % PlayerColors.Length;
                    }
                    pColorI[i] = newColor;
                    UpdateColor(pColorI[i], i + 1);
                    break;
                }
            }
        }

        //Display press start?
        if (!allowSinglePlayer)
        {
            transform.Find("Start").gameObject.SetActive(false);
            bool allPlayers = true;
            if (p[0] && p[1])
            {
                for (int i = 0; i < 4; i++)
                {
                    if (p[i] && (!pickedColor[i] || !pReady[i]))
                    {
                        allPlayers = false;
                        break;
                    }
                }
                if(allPlayers)
                {
                    transform.Find("Start").gameObject.SetActive(true);
                    if (InputManager.ActiveDevice.GetControl(InputControlType.Start))
                    {
                        if (skipSceneSelect)
                        {
                            StartCoroutine(LoadSceneWithFade(1));
                        }
                        else
                        {
                            TrackSelect.SetActive(true);
                            gameObject.SetActive(false);
                        }
                    }
                }
            }
        }
        else
        {
            if (pReady[0])
            {
                transform.Find("Start").gameObject.SetActive(true);
                if (InputManager.ActiveDevice.GetControl(InputControlType.Start))
                {
                    if (skipSceneSelect)
                    {

                        StartCoroutine(LoadSceneWithFade(1));
                    }
                    else
                    {
                        TrackSelect.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                transform.Find("Start").gameObject.SetActive(false);
            }
        }
    }

    void UpdateColor(int index, int playerNum)
    {
        PlayerMaterials[playerNum - 1].color = PlayerColors[index];
        switch (playerNum)
        {
            case 1: PlayerControllers.Color1 = PlayerColors[index]; break;
            case 2: PlayerControllers.Color2 = PlayerColors[index]; break;
            case 3: PlayerControllers.Color3 = PlayerColors[index]; break;
            case 4: PlayerControllers.Color4 = PlayerColors[index]; break;
        }
    }

    IEnumerator LoadSceneWithFade(int toLoad)
    {
        float fadeTime = GameObject.Find("Fader").GetComponentInChildren<Fading>().BeginFade(1);
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(toLoad);
        yield return null;
    }
    void PlayerColorCheck(InputDevice controller, ref bool pMoved, ref int pColorInd, int playerNum)
    {
        if (controller != null)
        {
            if (controller.LeftStickX > deadZone && !pMoved)
            {
                int newColor = (pColorInd + 1) % PlayerColors.Length;
                while(Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor + 1) % PlayerColors.Length;
                }
                pColorInd = newColor;
                UpdateColor(pColorInd, playerNum);
                pMoved = true;
            }
            else if (controller.LeftStickX < -deadZone && !pMoved)
            {
                int newColor = (pColorInd - 1) % PlayerColors.Length;
                while (Array.Exists<int>(pColorI, i => i == newColor))
                {
                    newColor = (newColor - 1 + PlayerColors.Length) % PlayerColors.Length;
                }
                pColorInd = newColor;
                UpdateColor(pColorInd, playerNum);
                pMoved = true;
            }
            else if (Mathf.Abs(controller.LeftStickX) < deadZone)
            {
                pMoved = false;
            }
        }
    }
}
