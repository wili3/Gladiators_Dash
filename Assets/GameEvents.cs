using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : Singleton<GameEvents> {

	public GladiatorUser gladiatorUser;
	public GladiatorAI gladiatorAI;
	public bool ready, start;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!ready) {
			Search ();
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
}
