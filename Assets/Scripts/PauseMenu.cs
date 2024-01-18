using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    public GameObject PausePanel;
    public GameObject OptionsPanel;
   

    bool isPaused = false;
    private bool isOnOptions=false;

    void Awake()
    {
        Instance=this;
    }
    // Update is called once per frame
    void Update()
    {

    //Esc key to pause
    if (Input.GetKeyDown(KeyCode.Escape) && !isOnOptions)
    {
        if (isPaused)
        {
            Continue();
        }
        else
        {
            // Prevents pausing when GameOver
            if (!GameObject.Find("Roga").GetComponent<CatchPlayer>().unPausable)
            {
                isPaused=true;
                Pause();    
            }
            
        }
    }
    //Esc key in Options to go back to Pause
    if(Input.GetKeyDown(KeyCode.Escape) && isOnOptions)
    {
        BacktoPause();
    }
    }

    //Here are the functions to be called by the buttons
    public void BacktoMenu()
    {
        AudioManager.Instance.StopMusic("BGM");
        Time.timeScale=1;
        PausePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(0);
        if(MainMenu.Instance.onStage1)
        {
            if(PlayerMovement.Instance.runProgress>PlayerPrefs.GetInt("highscore1: "))
            {
                PlayerPrefs.SetInt("highscore1: ", (int) PlayerMovement.Instance.runProgress);
            }
            MainMenu.Instance.onStage1=false;
        }
        if(MainMenu.Instance.onStage2)
        {
            if(PlayerMovement.Instance.runProgress>PlayerPrefs.GetInt("highscore2: "))
            {
                PlayerPrefs.SetInt("highscore2: ", (int) PlayerMovement.Instance.runProgress);
            }
            MainMenu.Instance.onStage2=false;
        }
        if(MainMenu.Instance.onStage3)
        {
            if(PlayerMovement.Instance.runProgress>PlayerPrefs.GetInt("highscore3: "))
            {
                PlayerPrefs.SetInt("highscore3: ", (int) PlayerMovement.Instance.runProgress);
            }
            MainMenu.Instance.onStage3=false;
        }
        if(MainMenu.Instance.onEndless)
        {
            if(PlayerMovement.Instance.runProgress>PlayerPrefs.GetInt("highscoreEndless: "))
            {
                PlayerPrefs.SetInt("Endless", (int) PlayerMovement.Instance.runProgress);
            }
            MainMenu.Instance.onEndless=false;
        }
    }

    public void Options()
    {
        isOnOptions=true;
        PausePanel.SetActive(false);
        OptionsPanel.SetActive(true);
    }
    public void BacktoPause()
    {
        isOnOptions=false;
        PausePanel.SetActive(true);
        OptionsPanel.SetActive(false);
    }
    public void Pause()
    {
        // Prevents pausing when GameOver
        if (!GameObject.Find("Roga").GetComponent<CatchPlayer>().unPausable)
        {
            AudioManager.Instance.PauseMusic("BGM");
            PausePanel.SetActive(true);
            Time.timeScale=0;
        }
    }
    public void Continue()
    {
        isPaused = false;
        AudioManager.Instance.UnPauseMusic("BGM");
        PausePanel.SetActive(false);
        Time.timeScale=1;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    
}
