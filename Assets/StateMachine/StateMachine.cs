using UnityEditor;
using UnityEngine;

/// <summary>
/// The StateMachine handles state switching logic
/// States will use it to switch between each other
/// </summary>
public class StateMachine : MonoBehaviour
{
	public State[] States;

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

	//updates the state and statemachine references
	public void UpdateReferences()
	{
		//get list of all states in our object
		States = GetComponents<State>();

		//if we have no states selected...
		if(CurrentState == null && States.Length > 0)
		{
			//...select the first state
			CurrentState = States[0];
		}

		//for every state
		foreach(var state in States)
		{
			//if it's the starting state
			if(state == CurrentState)
			{
				CurrentState = state;
				state.enabled = true;
			}
			else //it's not that starting state
			{
				state.enabled = false;
			}
		}
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

		stateMachine.UpdateReferences();
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.LabelField("SwitchStates", EditorStyles.boldLabel);

		//make a list of buttons to use for switching states
		foreach(var state in stateMachine.States)
		{
			if(GUILayout.Button(state.GetType().Name))
			{
				//set as our new state
				stateMachine.SetState(state);

				if(!Application.isPlaying)
				{
					stateMachine.UpdateReferences();
				}
			}
		}
	}
}
