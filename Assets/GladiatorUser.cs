using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GladiatorUser : Gladiator {

	// Use this for initialization
	void Start () {
		state = State.Searching;
		animator = this.GetComponent<Animator>();
		animator.SetBool ("Searching", true);
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

	}

	public override void Die()
	{

	}

	public override void ReceiveDamage(int damage)
	{

	}
}
