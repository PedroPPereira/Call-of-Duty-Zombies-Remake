using System.Collections;
using UnityEngine;

public class WeaponManager : MonoBehaviour {

    public GameObject[] weaponsInUse;
    public GameObject[] weaponsInGame;
    public Rigidbody[] worldModels;

    public RaycastHit hit;
    public float distance = 2.0f;
    public LayerMask layerMaskWeapon;
    public LayerMask layerMaskAmmo;
    public LayerMask layerMaskPurchase;

    public Transform dropPosition;

    public float switchWeaponTime = 0.5f;
    [HideInInspector]
    public bool canSwitch = true;
    [HideInInspector]
    public bool showWepGui = false;
    [HideInInspector]
    public bool showAmmoGui = false;
    public bool showPurchaseGui = true;
    public static bool boolChangeWeapon = false;
    public static bool boolBought = false;
    private bool equipped = false;
    public int weaponToSelect;
    public static int setElement;
    public static int setPrice;
    public int setPriceAmmo;

    static int ammoLeft = 9; //Sets weapon ammo

    public int weaponToDrop;
    public GUISkin mySkin;
    public AudioClip pickupSound;
    private string textFromPickupScript = "";
    private string notes = "";
    private string note = "Press key E to pick up Ammo";
    private string note2 = "Select appropriate weapon to pick up ammo";

    

    [SerializeField] private AudioCollection _audioCollection = null;
    [SerializeField] private int _bank = 0;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

        //Vector3 position = transform.parent.position;
        //Vector3 direction = transform.TransformDirection(Vector3.forward);


        //if (Physics.Raycast(position, direction, out hit, distance, layerMaskPurchase.value))
        //{
        //    WallWeapon prefab = hit.transform.GetComponent<WallWeapon>();
        //    setElement = prefab.weapon;
        //    setPrice = prefab.price;
        //    showPurchaseGui = true;

        //    if (weaponsInUse[0] != weaponsInGame[setElement]) //&& weaponsInUse[1] != weaponsInGame[setElement])
        //    {
        //        equipped = false;
        //    }
        //    else
        //    {
        //        equipped = true;
        //    }
        //    Debug.Log("ccccf1");
        //    if (canSwitch)
        //    {
        //        Debug.Log("ccccf2");
        //        if (/*!equipped &&*/ Input.GetButtonDown("Use") && GameManagement.PlayerCash() >= setPrice)
        //        {
        //            Debug.Log("ccccf");
        //            //DropWeapon(weaponToDrop);
        //            DeselectWeapon();
        //            GameManagement.AddCash(-setPrice);
        //            weaponsInUse[weaponToSelect] = weaponsInGame[setElement];
        //            if (setElement == 8)
        //            {
        //                //PICKUP.cs is MISSING!!! TODO!!!

        //               /* Pickup pickupGOW1 = hit.transform.GetComponent<Pickup>();
        //                addStickGrenades(pickupGOW1.amount);*/
        //            }
        //            Destroy(hit.collider.transform.parent.gameObject);
        //        }
        //        //else
        //        //{
        //        //    if (Input.GetKeyDown("e") && GameManagement.PlayerCash() >= setPriceAmmo)
        //        //    {
        //        //        if (setElement == 8)
        //        //        {
        //        //            //PICKUP.cs is MISSING!!! TODO!!!

        //        //            /*Pickup pickupGOW = hit.transform.GetComponent<Pickup>();
        //        //            addStickGrenades(pickupGOW.amount);
        //        //            Destroy(hit.collider.transform.parent.gameObject);*/
        //        //        }
        //        //        else
        //        //        {
        //        //            BuyAmmo(setElement);
        //        //            GameManagement.AddCash(-setPriceAmmo);
        //        //        }
        //        //    }
        //        //}
        //    }
        //}
        //else
        //{
        //    showPurchaseGui = false;
        //}

        

        if (boolChangeWeapon)
        {
            Debug.Log("boolChangeWeapon");
            UpdateWeapon();
            boolChangeWeapon = false;
        }
    }

    public void UpdateWeapon()
    {
        
        if (weaponsInUse[0] != weaponsInGame[setElement])
        {
            equipped = false;
        }
        else
        {
            equipped = true;
        }
        if (canSwitch)
        {
            Debug.Log("canSwitch");
            if (GameManagement.PlayerCash() >= setPrice)
            {
                GameManagement.SubCash(setPrice);
                DeselectWeapon();
                
                weaponsInUse[weaponToSelect] = weaponsInGame[setElement];
                StartCoroutine(DoActivation());
                //Destroy(hit.collider.transform.parent.gameObject);
            }
        }
    }



    void BuyAmmo(int WallWeapon)
    {
        /* //Não funciona! Falta fzr a parte do ammo! Pode n ser necessário!
        if(WallWeapon == 0)//scar
        {
            WeaponScriptNEW mags = weaponsInUse[weaponToSelect].gameObject.transform.GetComponent<WeaponScriptNEW>();
            mags.magazine += 3;
        }
        else if (WallWeapon == 5)
        {
            ShotGunScriptNEW bullets = weaponsInUse[WeaponToSelect].gameObject.transform.GetComponent<ShotGunScriptNEW>();
            bullets.magazines += 5;
        }
        */
    }

    void addStickGrenades(int amount)
    {
       /* GrenadeScript stickGrenade = weaponsInGame[8].gameObject.transform.GetComponent<GrenadeScript>();
        stickGrenade.grenadeCount += amount;
        stickGrenade.DrawWeapon();*/
    }


    public void DeselectWeapon()
    {
        //Deactivate all weapons
        for(int i=0; i< weaponsInUse.Length; i++)
        {
            weaponsInUse[i].gameObject.SendMessage("Deselect", SendMessageOptions.DontRequireReceiver);
            weaponsInUse[i].gameObject.SetActive(false);
        }
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        canSwitch = false;
        yield return new WaitForSeconds(switchWeaponTime);
        SelectWeapon(weaponToSelect);
        yield return new WaitForSeconds(switchWeaponTime);
        canSwitch = true;
    }

    void SelectWeapon(int i)
    {
        //Activate selected weapons
        weaponsInUse[i].gameObject.SetActive(true);
        weaponsInUse[i].gameObject.SendMessage("DrawWeapon", SendMessageOptions.DontRequireReceiver);
        WallWeapon temp = weaponsInUse[i].gameObject.transform.GetComponent<WallWeapon>();
        weaponToDrop = temp.weapon;
    }

    void DropWeapon(int index)
    {
        for(int i=0;i<worldModels.Length;i++)
        {
            if(i==index)
            {
                Rigidbody drop = Instantiate(worldModels[i], dropPosition.transform.position, dropPosition.transform.rotation) as Rigidbody;
                drop.AddRelativeForce(0, 50, Random.Range(100, 200));
            }
        }
    }

    public static int getWeaponAmmo()
    {
        return ammoLeft;
    }

    public static void subWeaponAmmo()
    {
        ammoLeft--;
    }

    public static void reloadWeaponAmmo()
    {
        ammoLeft = 9;
    }


    private IEnumerator DoActivation()
    {
        // We need a valid collection and audio manager
        if (_audioCollection == null || AudioManager.instance == null) yield break;

        // Fetch Clip from Collection
        AudioClip clip = _audioCollection[_bank];
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
