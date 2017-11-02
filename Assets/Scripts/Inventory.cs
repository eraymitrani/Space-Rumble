using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int currentHP, maxHP = 10;
    public bool isStun;
    private float stunInterval = 0.5f;
    private SpriteRenderer sr;
    private Color original;
    public ScoreManager scoreManager;
    private UnityStandardAssets._2D.Platformer2DUserControl userControl;
    private Animator m_Anim;

    // Use this for initialization
    void Start ()
    {
        m_Anim = GetComponent<Animator>();
	    currentHP = maxHP;
	    sr = transform.Find("Sprites").GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("Sprite not found");
        }
	    original = sr.color;
        userControl = GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>();
    }

    // Update is called once per frame
    void Update () {
	    if (currentHP <= 0)
	    {
            //Dead();
            scoreManager.addScore(userControl.player_num, -1);
            scoreManager.announceWinner();
            m_Anim.SetBool("Dead", true);
	    }
	}

    public int Get_Hp()
    {
        return currentHP;
    }
    private void Dead()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    private void FlashOn()
    {
        sr.color = Color.clear;
    }

    private void FlashOff()
    {
        sr.color = original;
    }
    private void CancelStun()
    {
        isStun = false;
        CancelInvoke("FlashOn");
        CancelInvoke("FlashOff");
        sr.color = original;
    }
    public void Damage(int dmg)
    {
        currentHP -= dmg;
        isStun = true;
        InvokeRepeating("FlashOn", 0.0f, 0.2f);
        InvokeRepeating("FlashOff", 0.1f, 0.2f);
        Invoke("CancelStun", stunInterval);
        GetComponentInChildren<Text>().text = (Get_Hp() / 2).ToString() + "♥";
    }
}
