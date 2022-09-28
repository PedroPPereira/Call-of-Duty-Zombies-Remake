using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    public GameObject TutorialTextPauseMenu;

    private Animation anim;


    void Start()
    {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        } 
	}

    public void Resume()
    {
        TutorialTextPauseMenu.SetActive(false);
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
        GameIsPaused = false;
    }

    void Pause()
    {
        Cursor.visible = true;
        TutorialTextPauseMenu.SetActive(true);
        //anim.Play("tutorialTextSoftAppearance");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0.0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
       // SceneManager.LoadScene("Menu"); //Falta fzr menu inicial
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
