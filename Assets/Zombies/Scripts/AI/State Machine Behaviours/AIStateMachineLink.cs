using UnityEngine;
using System.Collections;

public enum ComChannelName { ComChannel1, ComChannel2, ComChannel3, ComChannel4};


public class AIStateMachineLink : StateMachineBehaviour 
{
	// The AI State Machine reference
	protected AIStateMachine _stateMachine;
	public AIStateMachine stateMachine{ set{ _stateMachine = value;}}
}
