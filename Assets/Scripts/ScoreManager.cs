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
        //get the saved highscore
        highscore = PlayerPrefs.GetInt("highscore: ", 0);
        highscoreText.text="HIGHSCORE: "+highscore.ToString()+"m";
        //set the score text
        scoreText.text=score.ToString();
    }

    public void AddPoint(float runProgress)
    {
        //adds progress to score bar
        score = (int) runProgress;
        scoreText.text=score.ToString()+"m";
        //sets highscore once reached
        if(highscore < score)
            PlayerPrefs.SetInt("highscore: ", score);
    }
}
