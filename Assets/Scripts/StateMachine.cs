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
	public State CurrentState;

	//disables the current state, and activatese the new one
	public void SetState(State newState)
	{
		//make sure the new state is valid
		if(newState == null)
		{
			throw new System.Exception("Tried to set null state");
		}

		//disable the current state
		if(CurrentState != null)
		{
			CurrentState.enabled = false;
		}

		//start our new state
		newState.enabled = true;
		CurrentState = newState;
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
		//get a reference to our StateMachine component
		stateMachine = (StateMachine)target;

		UpdateReferences();
	}

	public override void OnInspectorGUI()
	{
		if(Application.isPlaying)
		{
			EditorGUILayout.LabelField("SwitchStates", EditorStyles.boldLabel);

			//make a list of buttons to use for switching states
			foreach(var state in stateMachine.States)
			{
				if(GUILayout.Button(state.GetType().Name))
				{
					//set as our new state
					stateMachine.SetState(state);
				}
			}
		}
		else
		{
			EditorGUILayout.LabelField("Set Starting State", EditorStyles.boldLabel);

			//make a list of buttons to select our starting state
			foreach(var state in stateMachine.States)
			{
				if(GUILayout.Button(state.GetType().Name))
				{
					//set our starting state and update references
					stateMachine.StartingState = state;
					UpdateReferences();
				}
			}
		}
	}

	//updates the state and statemachine references
	private void UpdateReferences()
	{
		//get list of all states in our object
		stateMachine.States = stateMachine.GetComponents<State>();

		//if we have no states selected...
		if(stateMachine.StartingState == null && stateMachine.States.Length > 0)
		{
			//...select the first state
			stateMachine.StartingState = stateMachine.States[0];
		}

		//for every state
		foreach(var state in stateMachine.States)
		{
			//if it's the starting state
			if(state == stateMachine.StartingState)
			{
				state.enabled = true;
			}
			else //it's not that starting state
			{
				state.enabled = false;
			}

			//make sure it has a reference to the state machine
			state.StateMachine = stateMachine;
		}
	}
}
