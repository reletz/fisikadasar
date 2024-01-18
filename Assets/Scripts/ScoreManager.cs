using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    

    public int score=0;
    int highscore=0;
    

    private void Awake() {
        instance=this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(MainMenu.Instance.onStage1)
        {
            //get the saved highscore
            highscore = PlayerPrefs.GetInt("highscore1: ", 0);
            highscoreText.text="BEST RUN: "+(highscore/12).ToString()+"%";
            //set the score text
            scoreText.text=score.ToString();
        }
        if(MainMenu.Instance.onStage2)
        {
            //get the saved highscore
            highscore = PlayerPrefs.GetInt("highscore2: ", 0);
            highscoreText.text="BEST RUN: "+(highscore/12).ToString()+"%";
            //set the score text
            scoreText.text=score.ToString();
        }
        if(MainMenu.Instance.onStage3)
        {
            //get the saved highscore
            highscore = PlayerPrefs.GetInt("highscore3: ", 0);
            highscoreText.text="BEST RUN: "+(highscore/12).ToString()+"%";
            //set the score text
            scoreText.text=score.ToString();
        }
        if(MainMenu.Instance.onEndless)
        {
            //get the saved highscore
            highscore = PlayerPrefs.GetInt("highscoreEndless: ", 0);
            highscoreText.text="BEST RUN: "+highscore.ToString()+"m";
            //set the score text
            scoreText.text=score.ToString();
        }
    }

    public void AddPoint(float runProgress)
    {
        if(MainMenu.Instance.onStage1)
        {
            //adds progress to score bar
            score = (int) runProgress;
            scoreText.text=score.ToString()+"m";
            //sets highscore once reached
            if(highscore < score)
            {
                PlayerPrefs.SetInt("highscore1: ", score);
            }
        }
        if(MainMenu.Instance.onStage2)
        {
            //adds progress to score bar
            score = (int) runProgress;
            scoreText.text=score.ToString()+"m";
            //sets highscore once reached
            if(highscore < score)
            {
                PlayerPrefs.SetInt("highscore2: ", score);
            }    
        }
        if(MainMenu.Instance.onStage3)
        {
            //adds progress to score bar
            score = (int) runProgress;
            scoreText.text=score.ToString()+"m";
            //sets highscore once reached
            if(highscore < score)
            {
                PlayerPrefs.SetInt("highscore3: ", score);
            }
        }
        if(MainMenu.Instance.onEndless)
        {
            //adds progress to score bar
            score = (int) runProgress;
            scoreText.text=score.ToString()+"m";
            //sets highscore once reached
            if(highscore < score)
            {
                PlayerPrefs.SetInt("highscoreEndless: ", score);
            }
        }
    }
}
