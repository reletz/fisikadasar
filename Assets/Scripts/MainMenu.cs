using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public static MainMenu Instance;
    public GameObject MainMenuPanel;
    public GameObject PlayPanel;
    public GameObject StoryPanel;
    public GameObject OptionsPanel;
    public GameObject Stage2Lock;
    public GameObject Stage3Lock;
    public GameObject EndlessLock;

    private bool onPlay=false;
    private bool onStory=false;
    private bool onOptions=false;
    public bool onStage1=false;
    public bool onStage2=false;
    public bool onStage3=false;
    public bool onEndless=false;

    void Awake()
    {
        Instance=this;
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt("Stage1_toStory")==1)
        {
            PlayerPrefs.SetInt("Stage1_toStory", 2);
            onStage1=false;
            Play();
            Story();
        }
        if(PlayerPrefs.GetInt("Stage2_toStory")==1)
        {
            PlayerPrefs.SetInt("Stage2_toStory", 2);
            onStage2=false;
            Play();
            Story();
        }
        if(PlayerPrefs.GetInt("Stage3_toStory")==1)
        {
            PlayerPrefs.SetInt("Stage3_toStory", 2);
            onStage3=false;
            Play();
            Story();
        }

        if(PlayerPrefs.GetInt("Stage1_toStory")==2)
        {
            Stage2Lock.SetActive(false);
        }
        else
        {
            Stage2Lock.SetActive(true);
        }
        if(PlayerPrefs.GetInt("Stage2_toStory")==2)
        {
            Stage3Lock.SetActive(false);
        }
        else
        {
            Stage3Lock.SetActive(true);
        }
        if(PlayerPrefs.GetInt("Stage3_toStory")==2)
        {
            EndlessLock.SetActive(false);
        }
        else
        {
            EndlessLock.SetActive(true);
        }
        
        
        //Esc key in Options to go back to Main Menu
        if(Input.GetKeyDown(KeyCode.Escape) && onPlay)
        {
            Backfrom_Play();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && onStory)
        {
            Backfrom_Story();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && onOptions)
        {
            Backfrom_Options();
        }
    }

    //Here are the functions to be called by the buttons
    public void Stage1()
    {
        onPlay=false;
        onStory=false;
        onStage1=true;
        AudioManager.Instance.StopMusic("BGM");
        PlayPanel.SetActive(false);
        SceneManager.LoadSceneAsync(1);
    }
    public void Stage2()
    {
        onPlay=false;
        onStory=false;
        onStage2=true;
        AudioManager.Instance.StopMusic("BGM");
        PlayPanel.SetActive(false);
        SceneManager.LoadSceneAsync(2);
    }
    public void Stage3()
    {
        onPlay=false;
        onStory=false;
        onStage3=true;
        AudioManager.Instance.StopMusic("BGM");
        PlayPanel.SetActive(false);
        SceneManager.LoadSceneAsync(3);
    }
    public void Endless()
    {
        onPlay=false;
        onStory=false;
        onEndless=true;
        AudioManager.Instance.StopMusic("BGM");
        SceneManager.LoadSceneAsync(4);
    }
    public void Play()
    {
        onPlay=true;
        PlayPanel.SetActive(true);
    }
    public void Backfrom_Play()
    {
        onPlay=false;
        PlayPanel.SetActive(false);
    }

    public void Story()
    {
        onPlay=false;
        onStory=true;
        PlayPanel.SetActive(false);
        StoryPanel.SetActive(true);
    }
    public void Backfrom_Story()
    {
        onPlay=true;
        onStory=false;
        PlayPanel.SetActive(true);
        StoryPanel.SetActive(false);
    }
    public void Options()
    {
        onOptions=true;
        OptionsPanel.SetActive(true);
    }
    public void Backfrom_Options()
    {
        onOptions=false;
        OptionsPanel.SetActive(false);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
