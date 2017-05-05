using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyInterface : Singleton<EnemyInterface> {
	public int numOfGladiators = 4;
	public GameObject gladiatorPrefab;
	public List<GladiatorAI> gladiators;
	public List<Image> skullImages;
}
