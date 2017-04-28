using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class UserInterface : Singleton<UserInterface> {
	public int numOfGladiators = 4;
	public GameObject gladiatorPrefab;
	public List<GladiatorUser> gladiators;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < numOfGladiators; i++) {
			GameObject gladiator = Instantiate (gladiatorPrefab) as GameObject;
			gladiators.Add(gladiator.GetComponent<GladiatorUser>());
			gladiator.transform.position = GameEvents.Instance.houseUser.position;
			gladiator.transform.rotation = GameEvents.Instance.houseUser.rotation;
		}
	}

	public void TriggerCard(int num)
	{
		if (GameEvents.Instance.gladiatorUser == null) {
			GameEvents.Instance.gladiatorUser = gladiators [num];
			GameEvents.Instance.gladiatorUser.Initialize ();
		}
	}
}
