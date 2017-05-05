using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInterface : Singleton<EnemyInterface> {
	public int numOfGladiators = 4, currentGladiator = 0;
	public GameObject gladiatorPrefab;
	public List<GladiatorAI> gladiators;
	public List<Image> skullImages;
	public float currentMaxLife;
	void Start()
	{
		for (int i = 0; i < numOfGladiators; i++) {
			GameObject gladiator = Instantiate (gladiatorPrefab) as GameObject;
			gladiators.Add(gladiator.GetComponent<GladiatorAI>());
			gladiator.transform.position = GameEvents.Instance.houseAI.position;
			gladiator.transform.rotation = GameEvents.Instance.houseAI.rotation;
			gladiator.GetComponent<GladiatorAI> ().idGladiator = i;
		}

		TriggerCard (healthiest());
	}

	void Update()
	{
		if (GameEvents.Instance.fightTime > 2) {
			if (currentMaxLife > 50) {
				if (gladiators [currentGladiator].life < 50) {
					TriggerCard (healthiest ());
				}
			} else {
				if (gladiators [currentGladiator].life == 0) {
					TriggerCard (healthiest ());
				}
			}
		}

		if (gladiators [currentGladiator].life == 0) {
			TriggerCard (healthiest ());
		}
	}

	public void TriggerCard(int num)
	{
		if (GameEvents.Instance.gladiatorAI == null) {
			GameEvents.Instance.gladiatorAI = gladiators [num];
			GameEvents.Instance.gladiatorAI.Initialize ();
			currentGladiator = num;
		} else {
			GameEvents.Instance.gladiatorAI.state = Gladiator.State.Switching;
			GameEvents.Instance.gladiatorAI.id = num;
			GameEvents.Instance.gladiatorAI.rival.switched = true;
			GameEvents.Instance.gladiatorAI.ready = false;
			GameEvents.Instance.ready = false;
			GameEvents.Instance.gladiatorAI.animator.SetBool ("Fighting", false);
			GameEvents.Instance.gladiatorAI.animator.SetBool ("Searching", true);
			GameEvents.Instance.fightTime = 0;
		}
	}

	public void SwitchCard(int num)
	{
		GameEvents.Instance.gladiatorAI = gladiators [num];
		GameEvents.Instance.gladiatorAI.Initialize ();
		currentGladiator = num;
	}

	public int healthiest()
	{
		float maxLife = 0;
		int indexOfMax = 0;
		for(int i = 0; i < gladiators.Count; i++)
		{
			if (gladiators [i].life > maxLife) {
				maxLife = gladiators [i].life;
				indexOfMax = i;
				currentMaxLife = maxLife;
			}
		}

		return indexOfMax;
	}
}
