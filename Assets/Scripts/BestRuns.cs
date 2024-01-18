using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestRuns : MonoBehaviour
{
    public TextMeshProUGUI bestrunStage1;
    public TextMeshProUGUI bestrunStage2;
    public TextMeshProUGUI bestrunStage3;
    public TextMeshProUGUI bestrunEndless;
    int bestrun1=0;
    int bestrun2=0;
    int bestrun3=0;
    int endless=0;
    // Start is called before the first frame update
    void Start()
    {
        //get the saved best runs
        bestrun1=PlayerPrefs.GetInt("highscore1: ");
        bestrunStage1.text="BEST RUN: "+(bestrun1/12).ToString()+"%";
        if(bestrun1/12>=100)
        {
            bestrunStage1.text="BEST RUN: "+"100%";
        }
        bestrun2=PlayerPrefs.GetInt("highscore2: ");
        bestrunStage2.text="BEST RUN: "+(bestrun2/12).ToString()+"%";
        if(bestrun2/12>=100)
        {
            bestrunStage2.text="BEST RUN: "+"100%";
        }
        bestrun3=PlayerPrefs.GetInt("highscore3: ");
        bestrunStage3.text="BEST RUN: "+(bestrun3/12).ToString()+"%";
        if(bestrun3/12>=100)
        {
            bestrunStage3.text="BEST RUN: "+"100%";
        }
        endless=PlayerPrefs.GetInt("highscoreEndless: ");
        bestrunEndless.text="BEST RUN: "+endless.ToString()+"m";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
