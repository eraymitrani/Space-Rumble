using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {


	public float shakeDuration = 1f;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	private float startTime;
	private Vector3 originalPos;


	public void Shake() {
		StartCoroutine (RunShake ());
	}

	public IEnumerator RunShake()
	{
		Vector3 start = this.transform.position;
		for (float t = 0; t < shakeDuration; t += Time.deltaTime) {
			this.transform.position = start + Random.insideUnitSphere * shakeAmount;
			yield return null;
		}
		this.transform.position = start;
	}
}
