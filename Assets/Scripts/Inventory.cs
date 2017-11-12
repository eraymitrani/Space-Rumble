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
        InputManager.ActiveDevice.Vibrate(10000f);
        if (currentHP <= 0 && !m_Anim.GetBool("Dead"))
	    {
	        InputManager.ActiveDevice.Vibrate(1f);
            InputManager.ActiveDevice.Vibrate(1f,1f);
            scoreManager.addScore(userControl.player_num, -1);
            m_Anim.SetBool("Dead", true);
            userControl.enabled = false;
            StartCoroutine(killSelf());
	    }

		//immovability shield
		if (isImmovable) {
			Debug.DrawRay (transform.position, 5 * Vector3.up);

			gameObject.GetComponent<SpriteRenderer> ().enabled = true;

			immov_timer += Time.deltaTime;
			if (immov_timer >= immov_time) {
				isImmovable = false;
				immov_timer = 0;
			}

			if (GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.AnyButton.WasPressed ||
			    Mathf.Abs (GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.LeftStickX) > 0.05f ||
			    GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.RightTrigger.WasPressed ||
			    GetComponent<UnityStandardAssets._2D.Platformer2DUserControl> ().controller.LeftTrigger.WasPressed) {
				isImmovable = false;
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
