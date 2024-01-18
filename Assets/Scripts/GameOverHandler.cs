using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverHandler : MonoBehaviour
{
    public GameObject GameOverScreen;
    public GameObject Player;

    private GameOverSceneManager ScreenManager;
    private PlayerMovement PlayerMovementInstance;
    void Awake()
    {
        ScreenManager = GameOverScreen.GetComponent<GameOverSceneManager>();
        PlayerMovementInstance = Player.GetComponent<PlayerMovement>();
    }

    public void GameOver()
    {
        AudioManager.Instance.StopMusic("BGM");

        PlayerMovementInstance.Moveable = false;
        Player.layer += 4;

        GameOverScreen.SetActive(true);
        ScreenManager.Initialize();
    }
}
