using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorUser : Gladiator {

	// Use this for initialization
	void Start () {
		state = State.Searching;
		animator = this.GetComponent<Animator>();
		GameEvents.Instance.gladiatorUserLifeBar.owner = this;
		GameEvents.Instance.gladiatorUserLifeBar.UpdateBar ();
		rival = GameEvents.Instance.gladiatorAI;
		//animator.SetBool ("Searching", true);
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
				DrawAttackType ();
			} else if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseOverhead) {
				stamina -= 50;
				DrawAttackType ();
			}
		} else if (nextOption == NextOption.AttackRight) {
			if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseOverhead) {
				GameEvents.Instance.gladiatorAI.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType ();
			} else if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.DefenseRight) {
				stamina -= 50;
				DrawAttackType ();
			}
		} else if (nextOption == NextOption.DefenseOverhead) {
			stamina -= 25;
			animator.SetBool ("BlockingOverhead", true);
		} else if (nextOption == NextOption.DefenseRight) {
			stamina -= 25;
			animator.SetBool ("BlockingRight", true);
		}
	}

	public void DrawAttackType()
	{
		if (nextOption == NextOption.AttackOverhead) {
			animator.SetBool ("Overhead", true);
		} else {
			animator.SetBool ("RightAttack", true);
		}
	}

	public override void Die()
	{

	}

	public override void ReceiveDamage(int damage)
	{
		life -= damage;

		if (life < 0)
			life = 0;
		GameEvents.Instance.gladiatorUserLifeBar.UpdateBar ();
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
}
