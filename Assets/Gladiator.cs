using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gladiator : MonoBehaviour
{
	public enum State { Searching, Fighting, Dead };
	public abstract void Attack ();
	public abstract void ReceiveDamage (int damage);
	public abstract void Search ();
	public abstract void Die ();
	public bool ready;
	public float speed, rotation;
	public State state;
	public Animator animator;
}
