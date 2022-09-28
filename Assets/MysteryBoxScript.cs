using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBoxScript : MonoBehaviour {

    Animator controller;
    Animation animate;

    public static bool openBox=false, boxIsOpen, canTakeWeapon, triggerWeapon;

    public GameObject[] guns;
    public static int[] gunIndex;
    public static int boxPrice = 10;
    public AudioClip boxMusic;
    int weaponIndex;

    public static int selectedGun = 0;
    public Transform cubePosition;
    public float timer;
    public int counter, counterCompare;
    public static bool exitBox=false;

	// Use this for initialization
	void Start () {
        controller = GetComponentsInChildren<Animator>() [0];
        animate = GetComponentsInChildren<Animation>() [0];
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(openBox)
        {
            openBox = false;

            controller.Play("LidAnimation"); //Open Mystery Box
            animate.Play(); //RunGunMovement
            boxIsOpen = true;
            //audio.clip = boxMusic;
            //audio.Play();
            canTakeWeapon = false;
            triggerWeapon = true;

        }
        else if(controller.GetCurrentAnimatorStateInfo(0).IsName("LidAnimation") || animate.IsPlaying("GunMovAnimation"))
        {
            timer += Time.deltaTime;
            if (timer<4.0f && counter<counterCompare)
            {
                counter++;
            }
            else if(counter==counterCompare)
            {
                counter = 0;
                RandomGun();
                counterCompare++;
            }
            else if (triggerWeapon)
            {
                canTakeWeapon = true;
                triggerWeapon = false;
            }
            guns[selectedGun].transform.position = cubePosition.transform.position;
        }
        else if(boxIsOpen)
        {
            controller.Play("CloseLid"); // Close Lid
            counter = 0;
            counterCompare = 0;
            timer = 0;
            if (!animate.IsPlaying("GunMovAnimation") && !animate.IsPlaying("CloseLid")) boxIsOpen = false;
        }
	}

    void RandomGun()
    {
        int gunCount = guns.Length;
        int rand = Random.Range(0, gunCount);
        while(rand==selectedGun)
        {
            rand = Random.Range(0, gunCount);
        }
        selectedGun = rand;

        for (int i = 0; i < guns.Length; i++)
        {
            if (i != selectedGun) guns[i].SetActive(false);
        }
        guns[selectedGun].SetActive(true);
        guns[selectedGun].transform.position = cubePosition.transform.position;

    }

    //private void OnTriggerStay(Collider collide)
    //{
    //    Debug.Log("OnTriggerStay");
    //    if (collide.tag == "Player")
    //    {
    //        Debug.Log("Player");
    //        boxPrice = InteractiveMystery.price;
    //        if (Input.GetButtonDown("Use") && GameManagement.playerCash >= boxPrice && !boxIsOpen && !openBox)
    //        {
    //            Debug.Log("GetButtonDown");
    //            openBox = true;
    //            GameManagement.playerCash -= boxPrice;
    //        }

    //        if (canTakeWeapon && Input.GetButtonDown("Use"))
    //        {
    //            Debug.Log("canTakeWeapon");
    //            WeaponManager.setElement = selectedGun;
    //            WeaponManager.setPrice = boxPrice;
    //            WeaponManager.boolChangeWeapon = true;

    //            WeaponManager.boolChangeWeapon = false;
    //            WeaponManager manager = FindObjectOfType<WeaponManager>();
    //            manager.DeselectWeapon();
    //            manager.weaponsInUse[manager.weaponToSelect] = manager.weaponsInGame[gunIndex[selectedGun] ];
    //            for (int i = 0; i < guns.Length; i++)
    //            {
    //                guns[i].SetActive(false);
    //            }

    //            controller.Play("CloseLid"); // Close Lid
    //        }
    //    }
    //}

    private void Update()
    {
        if (exitBox)
        {
            exitBox = false;
            for (int i = 0; i < guns.Length; i++)
            {
                guns[i].SetActive(false);
            }

            controller.Play("CloseLid"); // Close Lid
        }
    }

    //private void OnTriggerExit(Collider collide)
    //{
    //    if(collide.tag == "Player")
    //    canTakeWeapon = false;
    //}
}
