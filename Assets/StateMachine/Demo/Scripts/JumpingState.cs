using UnityEditor;
using UnityEngine;

public class JumpingState : StateBehaviour //notice we're inheriting from State
{
	//you can get references to other states, so you can switch to them
	[SerializeField]
	private StateBehaviour _afterJumpState;

	private Rigidbody _rb;

	//Awake will be called for all states when the game starts, regardless of whether they're the starting state
	private void Awake()
	{
		_rb = GetComponent<Rigidbody>();
	}

	//OnEnable is called like normal, like when the state starts
	private void OnEnable()
	{
		_rb.AddForce(new Vector3(0, 300, 0));
	}

	//be careful, some unity methods will still run even if the behaviour is disabled
	private void OnCollisionEnter(Collision col)
	{
		if(enabled)
		{
			SetState(_afterJumpState);
		}
	}
}
