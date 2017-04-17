using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gladiator : MonoBehaviour
{
	public enum State { Searching, Fighting, Dead };
	public enum NextOption { Attack, Defense, Nothing };
	public abstract void Attack ();
	public abstract void ReceiveDamage (int damage);
	public abstract void Search ();
	public abstract void Die ();
	public abstract void DrawNextOption();
	public bool ready;
	public float speed, rotation;
	public State state;
	public NextOption nextOption;
	public Animator animator;
	public float attackChances, defenseChances, stamina, life;
}
