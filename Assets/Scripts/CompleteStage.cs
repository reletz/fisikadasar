using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteStage : MonoBehaviour
{
    public GameObject Black;
    public GameObject Bg1;
    public GameObject Bg2;
    public GameObject Bg3;
    public GameObject TextCongrats;
    public GameObject NextStage;
    private bool isOn=false;
    private bool isOn1=false;
    private bool isOpaque=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

    private IEnumerator Wait()
    {
        if(Bg2.activeSelf==true)
        {
            Bg2.SetActive(false);
            Bg3.SetActive(true);
        }
        else if(Bg3.activeSelf==true)
        {
            Bg3.SetActive(false);
            Bg1.SetActive(true);
        }
        else if(Bg1.activeSelf==true)
        {
            Bg1.SetActive(false);
            Bg2.SetActive(true);
        }

        ShiftingBG.Instance.backgroundRenderer.material.mainTextureOffset=new Vector2(0,0);
        yield return new WaitForSeconds(0.5f);
        isOpaque=true;
    }
    void Update()
    {
        
        if(!MainMenu.Instance.onEndless)
        {
            if(PlayerMovement.Instance.RunFill.fillAmount>=1 && !isOn)
            {
                GameObject.Find("Roga").GetComponent<CatchPlayer>().unPausable=true;
                PlayerMovement.Instance.Moveable=false;
                PlayerMovement.Instance.gameObject.layer += 4;
                isOn=true;
                isOn1=true;
            }
            if(PlayerMovement.Instance.RunFill.fillAmount>=1 && isOn1 && PlayerMovement.Instance.PlayerRb.velocity.y==0)
            {
                PlayerMovement.Instance.PlayerRb.velocity=new Vector2(PlayerMovement.Instance.MaxSpeed, 0);
                PlayerMovement.Instance.PlayerRb.bodyType = RigidbodyType2D.Kinematic;
                StartCoroutine(End());
                isOn1=false;
            }
        }
        if(MainMenu.Instance.onEndless)
        {
            if(PlayerMovement.Instance.RunFill.fillAmount>=1 && !isOn)
            {
                isOn=true;
                PlayerMovement.Instance.runProgressE=0;
            }
            if(isOn)
            {
                if(!isOpaque)
                {
                    float opacity = Black.GetComponent<SpriteRenderer>().color.a;
                    opacity += Time.deltaTime * 0.6f;
                    Black.GetComponent<SpriteRenderer>().color = new Color(Black.GetComponent<SpriteRenderer>().color.r,Black.GetComponent<SpriteRenderer>().color.g,Black.GetComponent<SpriteRenderer>().color.b,opacity);
                }
                if(Black.GetComponent<SpriteRenderer>().color.a>=1 && !isOn1)
                {
                    isOn1=true;
                    StartCoroutine(Wait());
                }
                if(isOpaque)
                {  
                    float opacity = Black.GetComponent<SpriteRenderer>().color.a;
                    opacity -= Time.deltaTime * 0.6f;
                    Black.GetComponent<SpriteRenderer>().color = new Color(Black.GetComponent<SpriteRenderer>().color.r,Black.GetComponent<SpriteRenderer>().color.g,Black.GetComponent<SpriteRenderer>().color.b,opacity);
                    if(Black.GetComponent<SpriteRenderer>().color.a<=0)
                    {
                        isOpaque=false;
                        isOn=false;
                        isOn1=false;
                    }   
                }
            }
        }
        
    }
    private IEnumerator End()
    {
        AudioManager.Instance.StopMusic("BGM");
        GameObject.Find("BackgroundEnvironment").GetComponent<CameraFollow>().enabled = false;
        GameObject.Find("BGimage").GetComponent<ShiftingBG>().enabled = false;
        PlayerMovement.Instance.Camera.GetComponent<CameraFollow>().enabled = false;
        if(MainMenu.Instance.onStage1)
        {
            PlayerPrefs.SetInt("highscore1: ", (int) PlayerMovement.Instance.runProgress);
        }
        if(MainMenu.Instance.onStage2)
        {
            PlayerPrefs.SetInt("highscore2: ", (int) PlayerMovement.Instance.runProgress);
        }
        if(MainMenu.Instance.onStage3)
        {
            PlayerPrefs.SetInt("highscore3: ", (int) PlayerMovement.Instance.runProgress);
        }
        if(MainMenu.Instance.onEndless)
        {
            PlayerPrefs.SetInt("highscoreEndless: ", (int) PlayerMovement.Instance.runProgress);
        }
        yield return new WaitForSeconds(3f);
        TextCongrats.SetActive(true);
        AudioManager.Instance.PlaySFX("Finish");
        yield return new WaitForSeconds(0.5f);
        TextCongrats.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TextCongrats.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        TextCongrats.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        TextCongrats.SetActive(true);
        NextStage.SetActive(true);
        AudioManager.Instance.StopMusic("BGM");
    }

    public void BackfromStage1()
    {
        PlayerPrefs.SetInt("Stage1_toStory", 1);
        Time.timeScale=1;
        PauseMenu.Instance.PausePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(0);
    }
    public void BackfromStage2()
    {
        PlayerPrefs.SetInt("Stage2_toStory", 1);
        Time.timeScale=1;
        PauseMenu.Instance.PausePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(0);
    }
    public void BackfromStage3()
    {
        PlayerPrefs.SetInt("Stage3_toStory", 1);
        Time.timeScale=1;
        PauseMenu.Instance.PausePanel.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadSceneAsync(0);
    }
}
