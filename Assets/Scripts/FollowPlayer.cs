using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FollowPlayer : MonoBehaviour {

//	Vector3 offset;
//	Vector3 fixed_scale;
//
//	// Use this for initialization
//	void Start () {
//		offset = GetComponent<RectTransform> ().position - GetComponentInParent<Transform> ().position;
//		fixed_scale = transform.localScale;
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		GetComponent<RectTransform> ().position = GetComponentInParent<Transform> ().position + offset;
//		if (GetComponentInParent<Transform> ().localScale.x < 0) {
//			transform.localScale = new Vector3(-fixed_scale.x, fixed_scale.y, fixed_scale.z);
//		} else {
//			transform.localScale = fixed_scale;
//		}
//	}
	public GameObject player_to_follow;
	Vector3 offset;

	void Start () {
		offset = transform.position - player_to_follow.transform.position;
	}

	void Update(){
		transform.position = player_to_follow.transform.position + offset;
		GetComponent<Text> ().text = (player_to_follow.GetComponent<Inventory> ().Get_Hp () / 2).ToString () + "♥" + "    " + 
									  Mathf.Floor(player_to_follow.GetComponentInChildren<WeaponController>().fuel).ToString();
	}

}
