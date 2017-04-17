using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorUser : Gladiator {

	// Use this for initialization
	void Start () {
		state = State.Searching;
		animator = this.GetComponent<Animator>();
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
		if (Vector3.Distance (transform.position, GameEvents.Instance.gladiatorAI.transform.position) < 1.6f)
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
		if (nextOption == NextOption.Attack) {
			if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.Attack) {
				GameEvents.Instance.gladiatorAI.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType ();
			} else if (GameEvents.Instance.gladiatorAI.nextOption == NextOption.Defense) {
				stamina -= 50;
				DrawAttackType ();
			}
		} else if (nextOption == NextOption.Defense) {
			stamina -= 25;
			animator.SetBool ("Blocking", true);
		}
	}

	public void DrawAttackType()
	{
		int rand = Random.Range (0, 100);

		if (rand < 50) {
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
	}
		
	public override void DrawNextOption()
	{
		int rand = Random.Range (0, 100);

		if (rand < 30) {
			nextOption = NextOption.Defense;
		} else {
			nextOption = NextOption.Attack;
		}
	}
}
