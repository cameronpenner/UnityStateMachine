using UnityEditor;
using UnityEngine;

public class GrowingState : State //notice we're inheriting from State
{
	//you can get references to other states, so you can switch to them
	[SerializeField]
	private State _afterWaitBehaviour;

	//OnDisable works as normal, it also gets called whenever a state exits
	private void OnDisable()
	{
		transform.localScale = new Vector3(1, 1, 1);
	}

	//Update behaves just like a normal MonoBehaviour
	private void Update()
	{
		transform.localScale += new Vector3(1 * Time.deltaTime, 1 * Time.deltaTime, 1 * Time.deltaTime);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			SetState(_afterWaitBehaviour);
		}

		if(transform.localScale.x > 5)
		{
			SetState(_afterWaitBehaviour);
		}
	}
}
