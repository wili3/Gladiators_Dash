using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : Singleton<UserInterface> {
	public int numOfGladiators = 4;
	public GameObject gladiatorPrefab;
	public List<GladiatorUser> gladiators;
	public List<Image> skullImages;
	public List<RectTransform> cards;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < numOfGladiators; i++) {
			GameObject gladiator = Instantiate (gladiatorPrefab) as GameObject;
			gladiators.Add(gladiator.GetComponent<GladiatorUser>());
			gladiator.transform.position = GameEvents.Instance.houseUser.position;
			gladiator.transform.rotation = GameEvents.Instance.houseUser.rotation;
			gladiator.GetComponent<GladiatorUser> ().idGladiator = i;
		}
	}

	public void TriggerCard(int num)
	{
		HighlightCard (num);
		if (GameEvents.Instance.gladiatorUser == null) {
			GameEvents.Instance.gladiatorUser = gladiators [num];
			GameEvents.Instance.gladiatorUser.Initialize ();
		} else {
			GameEvents.Instance.gladiatorUser.state = Gladiator.State.Switching;
			GameEvents.Instance.gladiatorUser.id = num;
			GameEvents.Instance.gladiatorUser.rival.switched = true;
			GameEvents.Instance.gladiatorUser.ready = false;
			GameEvents.Instance.ready = false;
			GameEvents.Instance.gladiatorUser.animator.SetBool ("Fighting", false);
			GameEvents.Instance.gladiatorUser.animator.SetBool ("Searching", true);
			GameEvents.Instance.fightTime = 0;
		}
	}

	public void SwitchCard(int num)
	{
		GameEvents.Instance.gladiatorUser = gladiators [num];
		GameEvents.Instance.gladiatorUser.Initialize ();
	}

	public void HighlightCard(int num)
	{
		TurnOff ();
		cards [num].GetComponent<CardHighlight> ().Highlight ();
	}

	public void TurnOff()
	{
		for (int i = 0; i < cards.Count; i++) 
		{
			cards [i].GetComponent<CardHighlight> ().Diselect ();
		}
	}
}
