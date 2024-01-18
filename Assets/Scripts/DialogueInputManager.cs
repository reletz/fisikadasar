using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class DialogueInputManager : MonoBehaviour
{
    public DialogueManager DialogueManagerInstance;
    public bool Active;

    void Start()
    {
        DialogueManagerInstance.LoadDialogue();
    }

    void Update()
    {
        if (!Active) return;

        if (Input.anyKeyDown)
        {
            DialogueManagerInstance.NextLine();
        }
    }

    public void SetActive(bool val)
    {
        Active = val;
    }
}
