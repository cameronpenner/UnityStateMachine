using UnityEditor;
using UnityEngine;

public class GrowingState : State
{
	[SerializeField]
	private State _afterWaitBehaviour;

	private void OnDisable()
	{
		transform.localScale = new Vector3(1, 1, 1);
	}

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
