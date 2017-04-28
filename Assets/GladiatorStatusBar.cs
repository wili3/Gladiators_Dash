using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GladiatorStatusBar : MonoBehaviour {

	public Image filledImage;
	public Gladiator owner;
	public bool isStamina;
	public float totalLife = 100;

	public void UpdateBar()
	{
		if (!isStamina) 
		{
			filledImage.fillAmount = owner.life / totalLife;
		}
		else 
		{
			filledImage.fillAmount = owner.stamina / totalLife;
		}
	}
}
