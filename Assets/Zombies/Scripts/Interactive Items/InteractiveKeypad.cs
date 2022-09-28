using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InteractiveKeypad : InteractiveItem
{
	[SerializeField] protected Transform 			_elevator 		= null;
	[SerializeField] protected AudioCollection		_collection		= null;
	[SerializeField] protected int					_bank			= 0;
	[SerializeField] protected float				_activationDelay= 0.0f;
    public string levelToLoad;
    bool _isActivated	=	false;

	public override string GetText ()
	{
		// We have everything we need
		return "Teleport 5000$";
	}

	public override void Activate( CharacterManager characterManager )
	{
		if (_isActivated) return;
        if (Input.GetButtonDown("Use") && GameManagement.PlayerCash() > 5000)
        {
            GameManagement.AddCash(-5000);
            
            // Delay the actual animation for the desired number of seconds
            StartCoroutine(DoDelayedActivation(characterManager));

            _isActivated = true;
        }
        
	}

	protected IEnumerator DoDelayedActivation( CharacterManager characterManager)
	{
		if (!_elevator) yield break;;

		// Play the sound
		if (_collection!=null)
		{
			AudioClip clip = _collection[ _bank ];
			if (clip)
			{
				if (AudioManager.instance)
					AudioManager.instance.PlayOneShotSound( _collection.audioGroup, 
															clip,
															_elevator.position, 
															_collection.volume, 
															_collection.spatialBlend,
															_collection.priority );
				
			}
		}

		// Wait for the desired delay
		yield return new WaitForSeconds( _activationDelay );

		// If we have a character manager
		if (characterManager!=null)
		{
			// Make it a child of the elevator
			characterManager.transform.parent = _elevator;

			// Get the animator of the elevator and activate it animation
			Animator animator = _elevator.GetComponent<Animator>();
			if (animator) animator.SetTrigger( "Activate");

			// Freeze the FPS motor so it can rotate/jump/croach but can
			// not move off of the elevator.
			if (characterManager.fpsController)
			{
				characterManager.fpsController.freezeMovement = true;
			}
		}
        yield return new WaitForSeconds(6.0f);
        SceneManager.LoadScene(levelToLoad);
    }
}
