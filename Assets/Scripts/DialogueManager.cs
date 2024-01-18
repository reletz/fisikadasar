using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct DialogueLine
{
    public bool HasName;
    public string Name;
    public string Text;
}

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI DialogueField;
    public GameObject DialogueBox;
    public TextMeshProUGUI NameField;
    public GameObject NameBox;
    public Dialogue DialogueInstance;

    private int CurrentLine;
    private bool IsVisible;

    public void ShowDialogue()
    {
        IsVisible = true;
        DialogueBox.SetActive(true);
        DisplayLine(CurrentLine);
    }

    public void HideDialogue()
    {
        IsVisible = false;
        DialogueBox.SetActive(false);
        NameBox.SetActive(false);
    }

    public void LoadDialogue()
    {
        ShowDialogue();
        DisplayLine(CurrentLine = 0);
    }

    public void LoadDialogue(Dialogue NewDialogue)
    {
        DialogueInstance = NewDialogue;
        DisplayLine(CurrentLine = 0);
    }

    public bool NextLine()
    {
        if (CurrentLine < DialogueInstance.Lines.Length - 1)
        {
            DisplayLine(++CurrentLine);
            return true;
        }

        if (CurrentLine == DialogueInstance.Lines.Length) return false;

        ++CurrentLine;
        HideDialogue();
        return false;
    }

    public void DisplayLine(int Line)
    {
        if (!IsVisible) return;

        DialogueLine Current = DialogueInstance.Lines[Line];

        if (Current.HasName)
        {
            NameBox.SetActive(true);
            NameField.SetText(Current.Name);
        }
        else
        {
            NameBox.SetActive(false);
        }

        DialogueField.SetText(Current.Text);
    }
}
