using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnTriggerLoadLevelV2 : MonoBehaviour
{

    public Text enterText;
    public string levelToLoad;

    void Start()
    {
        enterText.text = "";
    }

    // Update is called once per frame
    void OnTriggerStay(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            enterText.text = "Teleport 5000$";

            if (Input.GetButtonDown("Use") && GameManagement.PlayerCash() > 5000)
            {
                
                GameManagement.AddCash(-5000);
                
                SceneManager.LoadScene(levelToLoad);
                enterText.text = "";
                
            }
            
        }
    }
    void OnTriggerExit(Collider plyr)
    {
        if (plyr.gameObject.tag == "Player")
        {
            enterText.text = "";
        }
    }
}
