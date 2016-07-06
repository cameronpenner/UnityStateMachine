using UnityEditor;
using UnityEngine;

/// <summary>
/// The StateMachine handles state switching logic
/// States will use it to switch between each other
/// </summary>
public class StateMachine : MonoBehaviour
{
	[Tooltip("Drag a state here to set your starting state")]
	public State StartingState;

	[Tooltip("A list of all the managed states")]
	public State[] States;

	[Tooltip("Our current state")]
	public State _currentState;

	//disables the current state, and activatese the new one
	public void SetState(State newState)
	{
		//make sure the new state is valid
		if(newState == null)
		{
			throw new System.Exception("Tried to set null state");
		}

		//disable the current state
		if(_currentState != null)
		{
			_currentState.enabled = false;
		}

		//start our new state
		newState.enabled = true;
		_currentState = newState;
	}
}

/// <summary>
/// The StateMachineEditor class manages the state machine inside the inspector,
/// Making sure the states have proper references and only the starting state is enabled
/// </summary>
[CustomEditor(typeof(StateMachine))]
[CanEditMultipleObjects]
public class StateMachineEditor : Editor
{
	//reference to our StateMachine instance
	private StateMachine stateMachine;

	private void OnEnable()
	{
		stateMachine = (StateMachine)target;

		UpdateReferences();
	}

	public override void OnInspectorGUI()
	{
		//grab the base component
		StateMachine stateMachine = (StateMachine)target;

		//draw the transform object field
		EditorGUI.BeginChangeCheck();
		stateMachine.StartingState = (State)EditorGUILayout.ObjectField("Starting State", stateMachine.StartingState, typeof(State));

		//if the reference changed
		if(EditorGUI.EndChangeCheck())
		{
			//update our references
			UpdateReferences();
			EditorUtility.SetDirty(stateMachine);
		}
	}

	//updates the
	private void UpdateReferences()
	{
		stateMachine.States = stateMachine.GetComponents<State>();

		if(stateMachine.StartingState == null && stateMachine.States.Length > 0)
		{
			stateMachine.StartingState = stateMachine.States[0];
		}

		foreach(var state in stateMachine.States)
		{
			if(state == stateMachine.StartingState)
			{
				state.enabled = true;
				stateMachine._currentState = state;
			}
			else
			{
				state.enabled = false;
			}
			state.StateMachine = stateMachine;
		}
	}
}
