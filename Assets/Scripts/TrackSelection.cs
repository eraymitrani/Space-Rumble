using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using InControl;

public class TrackSelection : MonoBehaviour {

    public GameObject TrackList;
    public GameObject TrackSelector;
    public GameObject TrackPreview;
    public GameObject TrackTextPrefab;

    int index = 0;
    int totalTracks = 0;
    bool moved = false;
    RectTransform selectorTransform;

    string[] MasterTrackList = new string[]
    {
        "Zach's Scene"
    };

    void Start()
    {
        foreach(string s in MasterTrackList)
        {
            GameObject track = Instantiate(TrackTextPrefab, TrackList.transform);
            track.GetComponent<Text>().text = s;
            totalTracks++;
        }
        TrackList.GetComponent<RectTransform>().sizeDelta = new Vector2(0, totalTracks * 100);
        selectorTransform = TrackSelector.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (InputManager.ActiveDevice.Action1.WasPressed)
        {
            SceneManager.LoadScene(index + 1);
        }

        if (Mathf.Abs(InputManager.ActiveDevice.LeftStickY) < 0.01f)
            moved = false;

        if (Mathf.Abs(InputManager.ActiveDevice.LeftStickY) > 0.01f && !moved)
        {
            if (InputManager.ActiveDevice.LeftStickY < 0)
            {
                if (index != totalTracks - 1)
                {
                    index += 1;
                    selectorTransform.localPosition = new Vector3(selectorTransform.localPosition.x, index * -100 - 50, 0);
                    moved = true;
                }
            }
            else
            {
                if (index != 0)
                {
                    index -= 1;
                    selectorTransform.localPosition = new Vector3(selectorTransform.localPosition.x, index * -100 - 50, 0);
                    moved = true;
                }
            }
        }
    }
}
