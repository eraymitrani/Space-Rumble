using System.Collections;
using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private int currentHP;
    public int maxHP = 1;
    public bool isStun;
	public bool isImmovable = false;
    private float stunInterval = 0.5f;
    private SpriteRenderer sr;
    public ScoreManager scoreManager;
    private UnityStandardAssets._2D.Platformer2DUserControl userControl;
    private Animator m_Anim;
	private bool is_dead = false;

	float immov_timer = 0f;
	float immov_time = 3f; //sorry for not using IEnumerators ¯\_(ツ)_/¯
	public bool is_powered_up = false;
	float powered_up_time = 3;
	float powered_up_timer = 0;

    // Use this for initialization
    void Start ()
    {
        m_Anim = GetComponent<Animator>();
	    //currentHP = maxHP;
		currentHP = 1;
	    sr = transform.Find("Sprites").GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogWarning("Sprite not found");
        }
        userControl = GetComponent<UnityStandardAssets._2D.Platformer2DUserControl>();
    }

    // Update is called once per frame
    void Update () {
        m_Anim.SetInteger("currentHp", currentHP);
        if (currentHP <= 0 && !m_Anim.GetBool("Dead"))
	    {
	        //InputManager.ActiveDevice.Vibrate(1f);
            scoreManager.addScore(userControl.player_num, -1);
            m_Anim.SetBool("Dead", true);
            userControl.enabled = false;
            StartCoroutine(killSelf());
	    }

		if (is_powered_up) {
			powered_up_timer += Time.deltaTime;
			if (powered_up_timer >= powered_up_time) {
				is_powered_up = false;
				powered_up_timer = 0;
			}
		}

		//immovability shield
		if (isImmovable || is_powered_up) {
			Debug.DrawRay (transform.position, 5 * Vector3.up);

			gameObject.GetComponent<SpriteRenderer> ().enabled = true;

			immov_timer += Time.deltaTime;
			if (immov_timer >= immov_time) {
				isImmovable = false;
				immov_timer = 0;
			}

			if (!is_powered_up) {
				if (Mathf.Abs (GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.LeftStickX) > 0.05f ||
				    GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.RightTrigger.WasPressed ||
				    GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.LeftTrigger.WasPressed) {
					isImmovable = false;
				}
			}
		} else {
			gameObject.GetComponent<SpriteRenderer> ().enabled = false;
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
        //GetComponentInChildren<Text>().text = Get_Hp().ToString() + "♥";
    }

    IEnumerator killSelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
