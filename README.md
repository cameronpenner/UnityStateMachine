# UnityStateMachine
## Getting Started
1. Create a new script and inherit from State

  ```C#
  public class JumpingState : State { }
  ```
2. Drag your new state onto a Game Object (A StateMachine should automatically attatch itself)
3. Create another state and add it to the GameObject
4. Switch between states by calling SetState()

  ```C#
  public class JumpingState : State
  {
    public State otherState;
  
    OnMouseDown()
    {
      SetState(otherState);
    }
  }
  ```

##Other Information
Switch states while editing or playing by clicking the buttons in the editor
