using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControllerAssign : MonoBehaviour {

    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Player4;
    public GameObject TrackSelect;
    public float deadZone = 0.5f;
    public Color[] PlayerColors;

    bool p1 = false, p2 = false, p3 = false, p4 = false;
    HashSet<InputDevice> usedControllers = new HashSet<InputDevice>();
    GameObject p1Color, p1Char, p2Color, p2Char, p3Color, p3Char, p4Color, p4Char;
    int p1ColorI = 0, p2ColorI = 1, p3ColorI = 2, p4ColorI = 3;
    bool p1Moved = false, p2Moved = false, p3Moved = false, p4Moved = false;

    void Awake()
    {
        PlayerControllers.reset();
        p1Color = Player1.transform.Find("Joined/Panel/ColorPanel").gameObject;
        p2Color = Player2.transform.Find("Joined/Panel/ColorPanel").gameObject;
        p3Color = Player3.transform.Find("Joined/Panel/ColorPanel").gameObject;
        p4Color = Player4.transform.Find("Joined/Panel/ColorPanel").gameObject;

        p1Char = Player1.transform.Find("Joined/Panel/PlayerPreview").gameObject;
        p2Char = Player2.transform.Find("Joined/Panel/PlayerPreview").gameObject;
        p3Char = Player3.transform.Find("Joined/Panel/PlayerPreview").gameObject;
        p4Char = Player4.transform.Find("Joined/Panel/PlayerPreview").gameObject;
    }

    void Update()
    {
        if(InputManager.ActiveDevice.Action1.WasPressed)
        {
            if (usedControllers.Contains(InputManager.ActiveDevice))
                return;

            if (!p1)
            {
                PlayerControllers.Player1 = InputManager.ActiveDevice;
                p1 = true;
                Player1.transform.Find("Join").gameObject.SetActive(false);
                Player1.transform.Find("Joined").gameObject.SetActive(true);
            }
            else if(!p2)
            {
                PlayerControllers.Player2 = InputManager.ActiveDevice;
                p2 = true;
                Player2.transform.Find("Join").gameObject.SetActive(false);
                Player2.transform.Find("Joined").gameObject.SetActive(true);
            }
            else if (!p3)
            {
                PlayerControllers.Player3 = InputManager.ActiveDevice;
                p3 = true;
                Player3.transform.Find("Join").gameObject.SetActive(false);
                Player3.transform.Find("Joined").gameObject.SetActive(true);
            }
            else if (!p4)
            {
                PlayerControllers.Player4 = InputManager.ActiveDevice;
                p4 = true;
                Player4.transform.Find("Join").gameObject.SetActive(false);
                Player4.transform.Find("Joined").gameObject.SetActive(true);
            }

            usedControllers.Add(InputManager.ActiveDevice);
        }

        if (PlayerControllers.Player1 != null && InputManager.ActiveDevice.GetControl(InputControlType.Start))
        {
            TrackSelect.SetActive(true);
            gameObject.SetActive(false);
        }

        if(PlayerControllers.Player1 != null)
        {
            if(PlayerControllers.Player1.LeftStickX > deadZone && !p1Moved)
            {
                p1ColorI = Mathf.Clamp(p1ColorI + 1, 0, PlayerColors.Length - 1);
                UpdateColor(p1Color, p1Char, p1ColorI);
                p1Moved = true;
            }
            else if(PlayerControllers.Player1.LeftStickX < -deadZone && !p1Moved)
            {
                p1ColorI = Mathf.Clamp(p1ColorI - 1, 0, PlayerColors.Length - 1);
                UpdateColor(p1Color, p1Char, p1ColorI);
                p1Moved = true;
            }
            else if(PlayerControllers.Player1.LeftStickX < deadZone && PlayerControllers.Player1.LeftStickX > -deadZone)
            {
                p1Moved = false;
            }
        }

        if (PlayerControllers.Player2 != null)
        {
            if (PlayerControllers.Player2.LeftStickX > deadZone && !p2Moved)
            {
                p2ColorI = Mathf.Clamp(p2ColorI + 1, 0, PlayerColors.Length - 1);
                UpdateColor(p2Color, p2Char, p2ColorI);
                p2Moved = true;
            }
            else if (PlayerControllers.Player2.LeftStickX < -deadZone && !p2Moved)
            {
                p2ColorI = Mathf.Clamp(p2ColorI - 1, 0, PlayerColors.Length - 1);
                UpdateColor(p2Color, p2Char, p2ColorI);
                p2Moved = true;
            }
            else if (PlayerControllers.Player2.LeftStickX < deadZone && PlayerControllers.Player2.LeftStickX > -deadZone)
            {
                p2Moved = false;
            }
        }

        if (PlayerControllers.Player3 != null)
        {
            if (PlayerControllers.Player3.LeftStickX > deadZone && !p3Moved)
            {
                p3ColorI = Mathf.Clamp(p3ColorI + 1, 0, PlayerColors.Length - 1);
                UpdateColor(p3Color, p3Char, p3ColorI);
                p3Moved = true;
            }
            else if (PlayerControllers.Player3.LeftStickX < -deadZone && !p3Moved)
            {
                p3ColorI = Mathf.Clamp(p3ColorI + 1, 0, PlayerColors.Length - 1);
                UpdateColor(p3Color, p3Char, p3ColorI);
                p3Moved = true;
            }
            else if (PlayerControllers.Player3.LeftStickX < deadZone && PlayerControllers.Player3.LeftStickX > -deadZone)
            {
                p3Moved = false;
            }
        }

        if (PlayerControllers.Player4 != null)
        {
            if (PlayerControllers.Player4.LeftStickX > deadZone && !p4Moved)
            {
                p4ColorI = Mathf.Clamp(p4ColorI + 1, 0, PlayerColors.Length - 1);
                UpdateColor(p4Color, p4Char, p4ColorI);
                p4Moved = true;
            }
            else if (PlayerControllers.Player4.LeftStickX < -deadZone && !p4Moved)
            {
                p4ColorI = Mathf.Clamp(p4ColorI + 1, 0, PlayerColors.Length - 1);
                UpdateColor(p4Color, p4Char, p4ColorI);
                p4Moved = true;
            }
            else if (PlayerControllers.Player4.LeftStickX < deadZone && PlayerControllers.Player4.LeftStickX > -deadZone)
            {
                p4Moved = false;
            }
        }
    }

    void UpdateColor(GameObject panel, GameObject player, int index)
    {
        panel.GetComponent<Image>().color = PlayerColors[index];
        player.GetComponent<Image>().color = PlayerColors[index];
    }
}
