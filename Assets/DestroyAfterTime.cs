using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

	public float target = 4,currentTime;
	// Update is called once per frame
	void Update () {
		currentTime += Time.deltaTime;
		if (currentTime >= target)
			Destroy (this.gameObject);
	}
}
