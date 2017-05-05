using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardHighlight : MonoBehaviour {

	Vector2 initialPos, targetPos;
	RectTransform rec;
	bool highlighted;

	void Start()
	{
		rec = this.GetComponent<RectTransform> ();
		initialPos = rec.anchoredPosition;
		targetPos = new Vector2 (initialPos.x, initialPos.y + 100);
	}

	void Update()
	{
		if (highlighted) {
			if (rec.anchoredPosition != targetPos) {
				rec.anchoredPosition = targetPos;
			}
		} else {
			if (rec.anchoredPosition != initialPos) {
				rec.anchoredPosition = initialPos;
			}
		}
	}

	public void Highlight()
	{
		highlighted = true;
	}

	public void Diselect()
	{
		highlighted = false;
	}
}
