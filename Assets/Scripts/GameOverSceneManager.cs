using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverSceneManager : MonoBehaviour
{
    public GameObject DialogueObject;
    public GameObject PredikatObject;
    public GameObject MenuObject;
    public float PredikatTransitionTime;
    public float MenuTransitionTime;

    private DialogueManager DialogueManagerInstance;
    private Image PredikatImage;
    private bool IsInitialized;
    private int State;
    private float Timer;

    void Awake()
    {
        DialogueManagerInstance = DialogueObject.GetComponent<DialogueManager>();
        PredikatImage = PredikatObject.GetComponent<Image>();
    }

    void Update()
    {
        if (!IsInitialized) return;

        switch (State)
        {
            case 0:
                UpdateDialog();
            break;
            case 1:
                UpdatePredikat();
            break;
            case 2:
                UpdateMenu();
            break;
            case 3:
                return;
        }
    }

    void UpdateDialog()
    {
        if (Input.anyKeyDown)
        {
            if (!DialogueManagerInstance.NextLine())
            {
                State = 1;
                DialogueManagerInstance.HideDialogue();
            }
        }
    }

    void UpdatePredikat()
    {
        if (!PredikatObject.activeInHierarchy)
        {
            PredikatObject.SetActive(true);
            PredikatObject.transform.localScale = new Vector3(2, 2, 1);
            PredikatImage.color = new Color(1, 1, 1, 0);

            Timer = 0;

            AudioManager.Instance.PlaySFX("GameOver2");
        }

        float Mult = 1/PredikatTransitionTime * Time.deltaTime;
        Timer += Time.deltaTime;

        PredikatObject.transform.localScale -= new Vector3(1, 1, 0) * Mult;
        PredikatImage.color += new Color(0, 0, 0, 1) * Mult;

        if (Timer >= PredikatTransitionTime)
        {
            State = 2;
        }
    }

    void UpdateMenu()
    {
        MenuObject.SetActive(true);
        State = 3;
    }

    public void Initialize()
    {
        IsInitialized = true;

        DialogueObject.SetActive(true);
        PredikatObject.SetActive(false);
        MenuObject.SetActive(false);

        DialogueManagerInstance.ShowDialogue();
        DialogueManagerInstance.LoadDialogue();

        State = 0;
    }
}
