using UnityEditor;
using UnityEngine;

/// <summary>
/// Inherit from State to use components in the state machine
/// </summary>
[RequireComponent(typeof(StateMachine))]
public class State : MonoBehaviour
{
	//All states have a reference to the StateMachine
	[HideInInspector]
	public StateMachine StateMachine;

	//Use SetState from any State class to switch states
	protected void SetState(State state)
	{
		StateMachine.SetState(state);
	}
}
