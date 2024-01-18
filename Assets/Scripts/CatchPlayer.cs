using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchPlayer : MonoBehaviour
{
    public GameOverHandler Handler;
    public bool unPausable=false;

    void OnTriggerEnter2D(Collider2D Other)
    {
        if (Other.CompareTag("Player"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Handler.GameOver();
        unPausable=true;
    }
}
