﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorAI : Gladiator {

	// Use this for initialization
	public void Initialize () {
		state = State.Searching;
		animator = this.GetComponent<Animator>();
		GameEvents.Instance.gladiatorAILifeBar.owner = this;
		GameEvents.Instance.gladiatorAILifeBar.UpdateBar ();
		rival = GameEvents.Instance.gladiatorUser;
		animator.SetBool ("Searching", true);
		if (GameEvents.Instance.gladiatorUser != null)
			GameEvents.Instance.gladiatorUser.rival = this;
	}

	// Update is called once per frame
	/*void Update () {
		
	}*/

	public override void Search()
	{
		if (!GameEvents.Instance.gladiatorUser)
			return;
		rotation += Time.deltaTime;
		transform.position += (transform.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (GameEvents.Instance.gladiatorUser.transform.position - transform.position), rotation);
		if (Vector3.Distance (transform.position, GameEvents.Instance.gladiatorUser.transform.position) < 2f)
		{
			GameEvents.Instance.gladiatorUser.ready = true;
			GameEvents.Instance.gladiatorUser.state = State.Fighting;
			ready = true;
			state = State.Fighting;
			animator.SetBool ("Searching", false);
			animator.SetBool ("Fighting", true);
			GameEvents.Instance.gladiatorUser.animator.SetBool ("Searching", false);
			GameEvents.Instance.gladiatorUser.animator.SetBool ("Fighting", true);
		}
	}

	public override void Attack()
	{
		if (nextOption == NextOption.AttackOverhead) {
			if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.DefenseRight) {
				GameEvents.Instance.gladiatorUser.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType (false);
			} else if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.DefenseOverhead) {
				stamina -= 50;
				DrawAttackType (true);
			}
		} else if (nextOption == NextOption.AttackRight) {
			if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.DefenseOverhead) {
				GameEvents.Instance.gladiatorUser.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType (false);
			} else if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.DefenseRight) {
				stamina -= 50;
				DrawAttackType (true);
			}
		} else if (nextOption == NextOption.DefenseOverhead) {
			stamina -= 25;
			if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.AttackOverhead) {
				DrawDefenseType (true);
			} else {
				DrawDefenseType (false);
			}
		} else if (nextOption == NextOption.DefenseRight) {
			stamina -= 25;
			if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.AttackRight) {
				DrawDefenseType (true);
			} else {
				DrawDefenseType (false);
			}
		}
		animator.SetBool ("Attacking", true);
	}

	public override void Die()
	{
		rival.switched = true;
		rival.animator.SetBool ("Victory", true);
		animator.SetBool ("Dead", true);
		EnemyInterface.Instance.skullImages [idGladiator].color = Color.gray;
		life = 0;
		state = State.Dead;
		GameEvents.Instance.gladiatorAI = null;
		GameEvents.Instance.ready = false;
		Attack ();
	}

	public override void ReceiveDamage(int damage)
	{
		life -= damage;
		GameEvents.Instance.gladiatorAILifeBar.UpdateBar ();
		if (life <= 0) {
			Die ();
		}
	}

	public override void DrawNextOption(bool attack)
	{
		int rand = 0;

		if (attack) 
		{
			rand = Random.Range (0, 100);

			if (rand > 50) 
			{
				nextOption = NextOption.AttackOverhead;
			}
			else 
			{
				nextOption = NextOption.AttackRight;
			}

		}
		else 
		{
			rand = Random.Range (0, 100);

			if (rand > 50) 
			{
				nextOption = NextOption.DefenseOverhead;
			}
			else 
			{
				nextOption = NextOption.DefenseRight;
			}
		}
	}

	public void Hurt(float damage)
	{
		life -= damage;

		if (life < 0)
			life = 0;
	}

	public void Switch()
	{
		rotation += Time.deltaTime;
		transform.position += (transform.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (GameEvents.Instance.houseAI.position - transform.position), rotation);
		if (Vector3.Distance (transform.position, GameEvents.Instance.houseAI.position) < 0.1f) 
		{
			EnemyInterface.Instance.SwitchCard (id);
		}
	}
}
