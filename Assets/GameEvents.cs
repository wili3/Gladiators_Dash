﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : Singleton<GameEvents> {

	public GladiatorUser gladiatorUser;
	public GladiatorAI gladiatorAI;
	public GladiatorStatusBar gladiatorUserLifeBar, gladiatorAILifeBar;
	public Transform houseUser, houseAI;
	public bool ready, start;
	public float fightTime, fightClashTime = 5;
	public int turn;
	// Use this for initialization
	void Start () {
		//gladiatorAI.Initialize ();
	}
	
	// Update is called once per frame
	void Update () {

		if (gladiatorUser == null || gladiatorAI == null)
			return;

		if (gladiatorUser.state == Gladiator.State.Switching) {
			gladiatorUser.Switch ();
		}
		if (gladiatorAI.state == Gladiator.State.Switching) {
			gladiatorAI.Switch ();
		}
		if (!ready) {
			Search ();
		}
		if (gladiatorUser.state == Gladiator.State.Fighting && gladiatorAI.state == Gladiator.State.Fighting) {
			fightTime += Time.deltaTime;
			if (fightTime >= fightClashTime) {
				DrawGladiatorMoves ();
			}
		}
	}

	void Search()
	{
		if (!start || gladiatorUser.state == Gladiator.State.Switching || gladiatorAI.state == Gladiator.State.Switching)
			return;
		if (!gladiatorUser.ready) {
			gladiatorUser.Search ();
		}
		if (!gladiatorAI.ready) {
			gladiatorAI.Search ();
		}

		if (gladiatorUser.ready && gladiatorAI.ready) {
			ready = true;
		}
	}

	public void DrawGladiatorMoves()
	{
		fightTime = 0;


		if (turn % 2 == 0) {
			gladiatorUser.DrawNextOption (true);
			gladiatorAI.DrawNextOption (false);
		} else {
			gladiatorUser.DrawNextOption (false);
			gladiatorAI.DrawNextOption (true);
		}

		gladiatorUser.Attack ();
		if(gladiatorAI != null)gladiatorAI.Attack ();
		turn++;
	}
}
