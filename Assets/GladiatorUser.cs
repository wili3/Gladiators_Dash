using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorUser : Gladiator {

	// Use this for initialization
	public void Initialize () {
		state = State.Searching;
		animator = this.GetComponent<Animator>();
		GameEvents.Instance.gladiatorUserLifeBar.owner = this;
		GameEvents.Instance.gladiatorUserLifeBar.UpdateBar ();
		rival = GameEvents.Instance.gladiatorAI;
		animator.SetBool ("Searching", true);
		switched = false;
		if (GameEvents.Instance.gladiatorAI != null) {
			GameEvents.Instance.gladiatorAI.rival = this;
			GameEvents.Instance.gladiatorAI.switched = false;
		}
	}
	
	// Update is called once per frame
	/*void Update () {
		
	}*/

	public override void Search()
	{
		if (!GameEvents.Instance.gladiatorAI)
			return;
		rotation += Time.deltaTime;
		transform.position += (transform.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (GameEvents.Instance.gladiatorAI.transform.position - transform.position), rotation);
		if (Vector3.Distance (transform.position, GameEvents.Instance.gladiatorAI.transform.position) < 2f)
		{
			GameEvents.Instance.gladiatorAI.ready = true;
			GameEvents.Instance.gladiatorAI.state = State.Fighting;
			ready = true;
			state = State.Fighting;
			animator.SetBool ("Searching", false);
			animator.SetBool ("Fighting", true);
			GameEvents.Instance.gladiatorAI.animator.SetBool ("Searching", false);
			GameEvents.Instance.gladiatorAI.animator.SetBool ("Fighting", true);
		}
	}

	public override void Attack()
	{
		if (nextOption == NextOption.AttackOverhead) {
			if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseRight) {
				GameEvents.Instance.gladiatorAI.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType (false);
			} else if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseOverhead) {
				stamina -= 50;
				DrawAttackType (true);

			}
		} else if (nextOption == NextOption.AttackRight) {
			if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseOverhead) {
				GameEvents.Instance.gladiatorAI.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType (false);
			} else if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseRight) {
				stamina -= 50;
				DrawAttackType (true);
			}
		} else if (nextOption == NextOption.DefenseOverhead) {
			stamina -= 25;
			if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.AttackOverhead) {
				DrawDefenseType (true);
			} else {
				DrawDefenseType (false);
			}
		} else if (nextOption == NextOption.DefenseRight) {
			stamina -= 25;
			if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.AttackRight) {
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
		animator.SetBool ("Dead", true);
		UserInterface.Instance.skullImages [idGladiator].color = Color.gray;
		UserInterface.Instance.cards [idGladiator].gameObject.SetActive (false);
		life = 0;
		state = State.Dead;
		GameEvents.Instance.gladiatorUser = null;
		GameEvents.Instance.ready = false;
	}

	public override void ReceiveDamage(int damage)
	{
		life -= damage;
		GameEvents.Instance.gladiatorUserLifeBar.UpdateBar ();

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

	public void Switch()
	{
		rotation += Time.deltaTime;
		transform.position += (transform.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (GameEvents.Instance.houseUser.position - transform.position), rotation);
		if (Vector3.Distance (transform.position, GameEvents.Instance.houseUser.position) < 0.1f) 
		{
			UserInterface.Instance.SwitchCard (id);
		}
	}
}
