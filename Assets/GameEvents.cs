using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : Singleton<GameEvents> {

	public GladiatorUser gladiatorUser;
	public GladiatorAI gladiatorAI;
	public GladiatorStatusBar gladiatorUserLifeBar, gladiatorAILifeBar;
	public bool ready, start;
	public float fightTime, fightClashTime = 5;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!ready) {
			Search ();
		}
		if (gladiatorUser.state == Gladiator.State.Fighting) {
			fightTime += Time.deltaTime;
			if (fightTime >= fightClashTime) {
				DrawGladiatorMoves ();
			}
		}
	}

	void Search()
	{
		if (!start)
			return;
		if (!gladiatorUser.ready) {
			gladiatorUser.Search ();
			gladiatorAI.Search ();
		} else {
			ready = true;
		}
	}

	public void DrawGladiatorMoves()
	{
		fightTime = 0;

		gladiatorUser.DrawNextOption ();
		gladiatorAI.DrawNextOption ();

		gladiatorUser.Attack ();
		gladiatorAI.Attack ();
	}
}
