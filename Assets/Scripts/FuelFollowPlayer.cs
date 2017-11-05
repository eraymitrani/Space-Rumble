using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelFollowPlayer : MonoBehaviour {

//	public GameObject player_to_follow;
//	Vector3 offset;
//
//	void Start () {
//		offset = transform.position - player_to_follow.transform.position;
//	}
//
//	void Update(){
//		transform.position = player_to_follow.transform.position + offset;
//		GetComponent<Slider> ().value = player_to_follow.GetComponentInChildren<WeaponController> ().fuel;
//	
//	}

	void Update(){
		GetComponent<Slider> ().value = transform.parent.parent.GetComponentInChildren<WeaponController> ().fuel;
	}
}
