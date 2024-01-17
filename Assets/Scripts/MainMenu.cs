using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MainMenuPanel;
    public GameObject PlayPanel;
    public GameObject StoryPanel;
    public GameObject OptionsPanel;

    private bool onPlay=false;
    private bool onStory=false;
    private bool onOptions=false;
    // Update is called once per frame
    void Update()
    {
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
    public void PLACEHOLDER_Play()
    {
        onPlay=false;
        onStory=false;
        AudioManager.Instance.StopMusic("BGM");
        PlayPanel.SetActive(false);
        SceneManager.LoadSceneAsync(1);
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
