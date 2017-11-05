using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int currentHP;
    public int maxHP = 5;
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
	    if (currentHP <= 0 && !m_Anim.GetBool("Dead"))
	    {
            scoreManager.addScore(userControl.player_num, -1);
            m_Anim.SetBool("Dead", true);
            userControl.enabled = false;
            StartCoroutine(killSelf());
	    }
	}

    public int Get_Hp()
    {
        return currentHP;
    }

    private void CancelStun()
    {
        isStun = false;
        m_Anim.SetBool("DamageTaken", false);
    }

    public void Damage(int dmg)
    {
        currentHP -= dmg;
        isStun = true;
        m_Anim.SetBool("DamageTaken", true);
        Invoke("CancelStun", stunInterval);
        GetComponentInChildren<Text>().text = Get_Hp().ToString() + "♥";
    }

    IEnumerator killSelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
