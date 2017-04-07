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

	}

	public override void Die()
	{

	}

	public override void ReceiveDamage(int damage)
	{

	}
}
