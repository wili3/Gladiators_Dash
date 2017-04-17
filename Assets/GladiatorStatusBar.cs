using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GladiatorStatusBar : MonoBehaviour {

	public Image filledImage;
	public Gladiator owner;
	public bool isStamina;

	public void UpdateBar()
	{
		if (!isStamina) 
		{
			filledImage.fillAmount = owner.life / 100;
		}
		else 
		{
			filledImage.fillAmount = owner.stamina / 100;
		}
	}
}
