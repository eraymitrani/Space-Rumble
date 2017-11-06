using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


	public float shakeDuration = 1f;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private float startTime;
	private Vector3 originalPos;


	void Awake() {
		
		originalPos = this.transform.position;

		MeteorMovement.meteorcrash += Shake;
		StartCoroutine (Shake ());
	}

	public IEnumerator Shake()
	{
		startTime = Time.time;
		while (startTime + shakeDuration <= Time.time) {
			this.transform.position = originalPos + Random.insideUnitSphere * shakeAmount;
			//shakeDuration -= Time.deltaTime * decreaseFactor;
			yield return null;
		}
		//this.transform.position = originalPos;
		yield return null;
	}
}
