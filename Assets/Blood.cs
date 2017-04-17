﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : StateMachineBehaviour {
	public GameObject bloodPrefab, sparklesPrefab;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (animator.GetComponent<Gladiator> ().rival.nextOption == Gladiator.NextOption.Defense) {
			GameObject sparkles = Instantiate (sparklesPrefab) as GameObject;
			sparkles.transform.SetParent (animator.GetComponent<Gladiator> ().rival.shieldPosition);
			sparkles.transform.position = animator.GetComponent<Gladiator> ().rival.shieldPosition.position;
			sparkles.transform.rotation = animator.GetComponent<Gladiator> ().rival.shieldPosition.rotation;
			sparkles.transform.localScale = Vector3.one;
		} else {
			GameObject blood = Instantiate (bloodPrefab) as GameObject;
			blood.transform.SetParent (animator.GetComponent<Gladiator> ().rival.bloodPosition);
			blood.transform.position = animator.GetComponent<Gladiator> ().rival.bloodPosition.position;
			blood.transform.rotation = animator.GetComponent<Gladiator> ().rival.bloodPosition.rotation;
			blood.transform.localScale = Vector3.one * 2;
		}
	}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}