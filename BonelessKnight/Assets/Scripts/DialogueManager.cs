using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public DialogueCanvasManager dialogueCanvasManager;
    public List<string> linesOfDialogue;
    private int currentLine = 0;
    bool shouldPauseWhenTalking = true;

    // Start is called before the first frame update
    void Start()
    {
        dialogueCanvasManager = FindObjectOfType<DialogueCanvasManager>();
    }

    public void Initialize()
    {
        dialogueCanvasManager.dialogueWindow.SetActive(true);
        
        if(shouldPauseWhenTalking)
        {
            Time.timeScale = 0;
        }

        dialogueCanvasManager.nextButton.onClick.AddListener(delegate { NextPressed(); });

        if(linesOfDialogue.Count > 0)
        {
            dialogueCanvasManager.tmproText.text = linesOfDialogue[0];
        }

    }

    public void NextPressed()
    {
        Debug.Log("Next pressed");
        if(currentLine < linesOfDialogue.Count-1)
        {
            currentLine++;
            dialogueCanvasManager.tmproText.text = linesOfDialogue[currentLine];
        }
        else
        {
            Debug.Log("Closing dialogue");
            Time.timeScale = 1;
            // close dialogue
            dialogueCanvasManager.dialogueWindow.SetActive(false);
        }
    }
}

// public class Dialogue
// {
//     public string dialogueText;
// }
