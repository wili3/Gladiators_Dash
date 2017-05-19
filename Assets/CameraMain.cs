using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMain : Singleton<CameraMain> {

	public Vector2 initialTouch, endTouch;
	public Vector2 finger1Touch, finger2Touch;
	public float initialDist, zoomAmount;
	int numOfTouches = 0;

	public Vector3 initialPos, maxPos;

	public void Activate()
	{
		GameEvents.Instance.start = true;
	}

	void Update()
	{
		ApplyZoom ();
		if (Input.touchCount == 0) 
		{
			if (numOfTouches > 0) {
				TouchesUp ();
			}
			return;
		}

		if (Input.touchCount == 1) 
		{
			if (initialTouch == Vector2.zero) {
				initialTouch = Input.touches [0].position;
			}
			numOfTouches = 1;
		}
		else 
		{
			if (finger1Touch == Vector2.zero) {
				finger1Touch = Input.touches [0].position;
				finger2Touch = Input.touches [1].position;
				initialDist = Vector2.Distance (finger1Touch, finger2Touch);
			} else {
				finger1Touch = Input.touches [0].position;
				finger2Touch = Input.touches [1].position;
				float dist = Vector2.Distance (finger1Touch, finger2Touch);

				if (dist > initialDist) {
					if (zoomAmount < 1)
						zoomAmount += 2*Time.deltaTime;
				} else if(dist < initialDist){
					if (zoomAmount > 0)
						zoomAmount -= 2*Time.deltaTime;
				}
				ApplyZoom ();
			}
			numOfTouches = 2;
		}
	}

	void TouchesUp()
	{
		numOfTouches = 0;
	}

	void ApplyZoom()
	{
		transform.localPosition = Vector3.Lerp (initialPos, maxPos, zoomAmount);
	}
}
