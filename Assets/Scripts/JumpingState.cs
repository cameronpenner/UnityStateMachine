using UnityEditor;
using UnityEngine;

public class JumpingState : State
{
	[SerializeField]
	private State _afterJumpState;

	private Rigidbody _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

	private void OnEnable()
	{
		_rb.AddForce(new Vector3(0, 300, 0));
	}

	private void OnCollisionEnter(Collision col)
	{
		SetState(_afterJumpState);
	}
}
