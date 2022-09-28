using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveMystery : InteractiveItem
{
    // Inspector Assigned
    [TextArea(3, 10)]
    [SerializeField] private string _infoText = null;
    [SerializeField] private AudioCollection _audioCollection = null;

    public int price = 10;

    // Private Fields
    private IEnumerator _coroutine = null;
    private float _hideActivatedTextTime = 0.0f;

    // ---------------------------------------------------------------------------
    // Name	:	GetText (Override)
    // Desc	:	Returns the text to display when player's crosshair is over this
    //			button.
    // ---------------------------------------------------------------------------
    public override string GetText()
    {
        return _infoText;
    }

    public override void Activate(CharacterManager characterManager)
    {
        if (Input.GetButtonDown("Use") && GameManagement.playerCash >= 750 && !MysteryBoxScript.boxIsOpen && !MysteryBoxScript.openBox)
        {
            Debug.Log("GetButtonDown");
            MysteryBoxScript.openBox = true;
            GameManagement.playerCash -= 750;
        }

        if (MysteryBoxScript.canTakeWeapon && Input.GetButtonDown("Use"))
        {
            Debug.Log("canTakeWeapon");
            WeaponManager.setElement = MysteryBoxScript.selectedGun+1;
            WeaponManager.setPrice = price;
            WeaponManager.boolChangeWeapon = true;
            MysteryBoxScript.exitBox = true;
            //WeaponManager.boolChangeWeapon = false;
            //WeaponManager manager = FindObjectOfType<WeaponManager>();
            //manager.DeselectWeapon();
            //manager.weaponsInUse[manager.weaponToSelect] = manager.weaponsInGame[gunIndex[selectedGun] ];
        }
    }

    private IEnumerator DoActivation()
    {
        // We need a valid collection and audio manager
        if (_audioCollection == null || AudioManager.instance == null) yield break;

        // Fetch Clip from Collection
        AudioClip clip = _audioCollection[0];
        if (clip == null) yield break;

        // Play it as one shot sound
        AudioManager.instance.PlayOneShotSound(_audioCollection.audioGroup,
                                                clip,
                                                transform.position,
                                                _audioCollection.volume,
                                                _audioCollection.spatialBlend,
                                                _audioCollection.priority);

        // Run while clip is playing
        yield return new WaitForSeconds(clip.length);
    }

}

