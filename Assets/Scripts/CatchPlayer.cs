using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    public GameObject GameOverScreen;

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        GameOverScreen.SetActive(true);
    }
}
