using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inventory : MonoBehaviour
{
    private int currentHP, maxHP = 10;
    public bool isStun;
    private float stunInterval = 0.5f;
    private SpriteRenderer sr;
    private Color original;

    // Use this for initialization
    void Start ()
	{
	    currentHP = maxHP;
	    sr = transform.Find("Sprites").GetComponent<SpriteRenderer>();
	    original = sr.color;

    }

    // Update is called once per frame
    void Update () {
	    if (currentHP <= 0)
	    {
	        Dead();
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
    }
}
