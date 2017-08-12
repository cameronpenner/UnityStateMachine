using System.Reflection;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

/// <summary>
/// Inherit from State to use components in the state machine
/// </summary>
[RequireComponent(typeof(StateMachine))]
public class StateBehaviour : MonoBehaviour
{
	//All states have a reference to the StateMachine
	//[HideInInspector]
	public StateMachine StateMachine { get; set; }

	//Use SetState from any State class to switch states
	public void SetState(StateBehaviour state)
	{
		if(StateMachine == null)
		{
			StateMachine = GetComponent<StateMachine>();
		}

		StateMachine.SetState(state);
	}

	//private void OnEnable()
	//{
	//	Debug.Log("Enabled");
	//}

	//private void OnDisable()
	//{
	//	Debug.Log("Disabled");
	//}
}

#if UNITY_EDITOR

/// <summary>
/// Handles State's GUI, makes sure only one is active at a time
/// </summary>
[CustomEditor(typeof(StateBehaviour), true)]
[CanEditMultipleObjects]
public class StateEditor : Editor
{
	//reference to the component
	private StateBehaviour state;

	private void OnEnable()
	{
		//get a reference to our State component
		state = (StateBehaviour)target;
		isEnabled = state.enabled;

		//set the script's icon
		SetIcon();
	}

	//keeps track of current enabled status
	private bool isEnabled = true;

	public override void OnInspectorGUI()
	{
		//get a reference to our State component
		state = (StateBehaviour)target;

		//if state changed
		if(state.enabled != isEnabled)
		{
			if(state.StateMachine == null)
			{
				state.StateMachine = state.GetComponent<StateMachine>();
			}

			//something changed, we will now update the lastMode
			isEnabled = state.enabled;

			if(state.enabled)
			{
				state.StateMachine.CurrentState = state;
			}

			state.StateMachine.UpdateReferences();
		}

		//draw children's inspectors
		DrawDefaultInspector();
	}

	private void SetIcon()
	{
		//load the image from resources
		Texture2D icon = (Texture2D)Resources.Load("State");

		//set script icon through reflection
		var editorGUIUtilityType = typeof(EditorGUIUtility);
		var bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
		var args = new object[] { state, icon };
		editorGUIUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);
	}
}

#endif
