using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gladiator : MonoBehaviour
{
	public enum State { Searching, Fighting, Dead, Switching };
	public enum NextOption { AttackOverhead, AttackRight, DefenseOverhead, DefenseRight, Nothing };
	public abstract void Attack ();
	public abstract void ReceiveDamage (int damage);
	public abstract void Search ();
	public abstract void Die ();
	public abstract void DrawNextOption(bool attack);
	public bool ready;
	public float speed, rotation;
	public State state;
	public NextOption nextOption;
	public Animator animator;
	public float attackChances, defenseChances, stamina, life;
	public Transform bloodPosition, shieldPosition;
	public Gladiator rival;
	public int id, idGladiator;
	public bool switched;

	public void DrawAttackType(bool defended)
	{
		if (nextOption == NextOption.AttackOverhead) {
			if (!defended) {
				animator.SetBool ("Overhead", true);
			} else {
				animator.SetBool ("OverheadDefended", true);
			}
		} else {
			if (!defended) {
				animator.SetBool ("RightAttack", true);
			} else {
				animator.SetBool ("RightAttackDefended", true);
			}
		}
	}

	public void DrawDefenseType(bool defended)
	{
		if (nextOption == NextOption.DefenseOverhead) {
			if (!defended) {
				animator.SetBool ("BlockingOverheadFailed", true);
			} else {
				animator.SetBool ("BlockingOverhead", true);
			}
		} else {
			if (!defended) {
				animator.SetBool ("BlockingRightFailed", true);
			} else {
				animator.SetBool ("BlockingRight", true);
			}
		}
	}
}
