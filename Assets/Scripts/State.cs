using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(StateMachine))]
public class State : MonoBehaviour
{
	[HideInInspector]
	public StateMachine StateMachine;

	protected void SetState(State state)
	{
		StateMachine.SetState(state);
	}
}
