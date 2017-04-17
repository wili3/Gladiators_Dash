using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorAI : Gladiator {

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
		if (!GameEvents.Instance.gladiatorUser)
			return;
		rotation += Time.deltaTime;
		transform.position += (transform.forward * speed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (GameEvents.Instance.gladiatorUser.transform.position - transform.position), rotation);
	}

	public override void Attack()
	{
		if (nextOption == NextOption.Attack) {
			if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.Attack) {
				GameEvents.Instance.gladiatorUser.ReceiveDamage (25);
				stamina -= 25;
				DrawAttackType ();
			} else if (GameEvents.Instance.gladiatorUser.nextOption == NextOption.Defense) {
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

		if (rand < 50) {
			nextOption = NextOption.Defense;
		} else {
			nextOption = NextOption.Attack;
		}
	}

	public void Hurt(float damage)
	{
		life -= damage;

		if (life < 0)
			life = 0;
	}
}
