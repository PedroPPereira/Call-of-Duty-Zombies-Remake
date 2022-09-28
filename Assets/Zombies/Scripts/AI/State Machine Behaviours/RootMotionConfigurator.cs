using UnityEngine;
using System.Collections;


public class RootMotionConfigurator : AIStateMachineLink 
{
	// Inspector Assigned Reference Incrementing Variables
	[SerializeField]	private int	_rootPosition=	0;
	[SerializeField]	private int _rootRotation=  0;

	private bool _rootMotionProcessed = false;


	override public void OnStateEnter(Animator animator, AnimatorStateInfo animStateInfo, int layerIndex )
	{


		// Request the enabling/disabling of root motion for this animation state 
		if (_stateMachine)
		{
			_stateMachine.AddRootMotionRequest( _rootPosition, _rootRotation );
			_rootMotionProcessed = true;
		}
		
	}


	override public void OnStateExit(Animator animator, AnimatorStateInfo animStateInfo, int layerIndex )
	{
		

		// Inform the AI State Machine that we wish to relinquish our root motion request.
		if (_stateMachine && _rootMotionProcessed)
		{
			_stateMachine.AddRootMotionRequest( -_rootPosition, -_rootRotation );
			_rootMotionProcessed = false;
		}
	}
}
