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

/// <summary>
/// Handles State's GUI, makes sure only one is active at a time
/// </summary>
[CustomEditor(typeof(State), true)]
[CanEditMultipleObjects]
public class StateEditor : Editor
{
	//reference to the component
	private State state;

	private void OnEnable()
	{
		//get a reference to our State component
		state = (State)target;
		isEnabled = state.enabled;
	}

	//keeps track of current enabled status
	private bool isEnabled = true;

	public override void OnInspectorGUI()
	{
		//get a reference to our State component
		state = (State)target;

		//if state changed
		if(state.enabled != isEnabled)
		{
			//something changed, we will now update the lastMode
			isEnabled = state.enabled;

			//if the state was enabled
			if(state.enabled)
			{
				state.StateMachine.CurrentState = state;
			}

			//update all the other states, to make sure we always have exactly 1 running state
			state.StateMachine.UpdateReferences();
		}

		//draw childrens inspectors
		DrawDefaultInspector();
	}
}
