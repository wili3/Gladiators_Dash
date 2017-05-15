using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blood : StateMachineBehaviour {
	public GameObject bloodPrefab, sparklesPrefab, bloodSound, shieldSound;
	public float sparklesTime = 0.39f, bloodTime = 0.43f;
	public bool defended, instantiated;
	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (animator.GetComponent<Gladiator> ().rival.nextOption == Gladiator.NextOption.DefenseOverhead && animator.GetComponent<Gladiator>().nextOption == Gladiator.NextOption.AttackOverhead) {
			defended = true;
		}
		else if (animator.GetComponent<Gladiator> ().rival.nextOption == Gladiator.NextOption.DefenseRight && animator.GetComponent<Gladiator>().nextOption == Gladiator.NextOption.AttackRight) {
			defended = true;
		} else {
			defended = false;
		}
	}


	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log("animator: " + stateInfo.normalizedTime);
		if (stateInfo.normalizedTime > sparklesTime && !instantiated) {
			if (defended) {
				GameObject sparkles = Instantiate (sparklesPrefab) as GameObject;
				sparkles.transform.SetParent (animator.GetComponent<Gladiator> ().rival.shieldPosition);
				sparkles.transform.position = animator.GetComponent<Gladiator> ().rival.shieldPosition.position;
				sparkles.transform.rotation = animator.GetComponent<Gladiator> ().rival.shieldPosition.rotation;
				sparkles.transform.localScale = Vector3.one;

				GameObject sparklesSound = Instantiate (shieldSound) as GameObject;
				sparklesSound.transform.SetParent (animator.GetComponent<Gladiator> ().rival.shieldPosition);
				sparklesSound.transform.position = animator.GetComponent<Gladiator> ().rival.shieldPosition.position;
				sparklesSound.transform.rotation = animator.GetComponent<Gladiator> ().rival.shieldPosition.rotation;
				sparklesSound.transform.localScale = Vector3.one;
				instantiated = true;
			} else {
				if (stateInfo.normalizedTime > bloodTime) {
					GameObject blood = Instantiate (bloodPrefab) as GameObject;
					blood.transform.SetParent (animator.GetComponent<Gladiator> ().rival.bloodPosition);
					blood.transform.position = animator.GetComponent<Gladiator> ().rival.bloodPosition.position;
					blood.transform.rotation = animator.GetComponent<Gladiator> ().rival.bloodPosition.rotation;
					blood.transform.localScale = Vector3.one * 2;

					GameObject bloodSnd = Instantiate (bloodSound) as GameObject;
					bloodSnd.transform.SetParent (animator.GetComponent<Gladiator> ().rival.shieldPosition);
					bloodSnd.transform.position = animator.GetComponent<Gladiator> ().rival.shieldPosition.position;
					bloodSnd.transform.rotation = animator.GetComponent<Gladiator> ().rival.shieldPosition.rotation;
					bloodSnd.transform.localScale = Vector3.one;
					instantiated = true;
				}
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		instantiated = false;
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
