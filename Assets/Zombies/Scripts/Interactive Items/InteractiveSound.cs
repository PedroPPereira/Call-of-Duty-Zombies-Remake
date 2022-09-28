using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveSound : InteractiveItem
{
	// Inspector Assigned
	[TextArea(3,10)]
	[SerializeField] private string 			_infoText				= null;
    public int priceBuy = 500;
	[SerializeField] private AudioCollection	_audioCollection		= null;
	[SerializeField] private int				_bank 					= 0;

    public int price = 500;
    public int weapon = 1;

	// Private Fields
	private IEnumerator	_coroutine				=	null;
	private float		_hideActivatedTextTime	=	0.0f;


	public override string	GetText()
	{
			return _infoText;
	}

	public override void Activate( CharacterManager characterManager )
	{
        if (Input.GetButtonDown("Use") && GameManagement.playerCash >= 500)
        {
            Debug.Log("GetButtonDown");
            WeaponManager.setElement = weapon;
            GameManagement.playerCash -= 500;
            WeaponManager.boolChangeWeapon = true;
        }
    }

	private IEnumerator DoActivation()
	{
		// We need a valid collection and audio manager
		if (_audioCollection==null || AudioManager.instance==null) yield break;

		// Fetch Clip from Collection
		AudioClip clip = _audioCollection[_bank];
		if (clip==null) yield break;

		// Play it as one shot sound
		AudioManager.instance.PlayOneShotSound( _audioCollection.audioGroup,
												clip,
												transform.position,
												_audioCollection.volume,
												_audioCollection.spatialBlend,
												_audioCollection.priority );

		// Run while clip is playing
		yield return new WaitForSeconds( clip.length );
	}
	
}
