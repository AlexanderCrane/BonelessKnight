using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CastleGateDialogueManager : DialogueManager
{
    public GameManager gameManager;
    public TextMeshProUGUI buttonTextMesh;

    private void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();    
    }

    protected override void Update() 
    {
        base.Update();
    }

    public override void Initialize()
    {
        Debug.Log("Initializing castle gate dialogue");
        if(gameManager.numSkeletonsRemaining <= 0)
        {
            canvasManager.nextButton.onClick.RemoveAllListeners();
            canvasManager.nextButton.onClick.AddListener(gameManager.QuitGame);
            buttonTextMesh.text = "Quit";

            canvasManager.dialogueWindow.SetActive(true);
            canvasManager.skeletonCountPanel.SetActive(false);
            Cursor.visible = true;

            if(shouldPauseWhenTalking)
            {
                Time.timeScale = 0;
            }

            canvasManager.dialogueTmproText.text = "You win!";
        }
        else
        {
            canvasManager.nextButton.onClick.AddListener(() => NextPressed());

            currentLine = 0;
            Debug.Log("Initializing dialogue for " + this.gameObject.name);
            canvasManager.dialogueWindow.SetActive(true);
            canvasManager.skeletonCountPanel.SetActive(false);
            Cursor.visible = true;

            if(shouldPauseWhenTalking)
            {
                Time.timeScale = 0;
            }

            if(linesOfDialogue.Count > 0)
            {
                canvasManager.dialogueTmproText.text = linesOfDialogue[0];
            }
        }
    }

    public override void NextPressed()
    {
        Debug.Log("Next pressed from castle gate");
        if(currentLine < linesOfDialogue.Count-1)
        {
            currentLine++;
            canvasManager.dialogueTmproText.text = linesOfDialogue[currentLine];
        }
        else
        {
            Debug.Log("Closing dialogue");

            canvasManager.nextButton.onClick.RemoveAllListeners();
            currentLine = 0;

            // close dialogue
            canvasManager.dialogueWindow.SetActive(false);
            if(!isCutscene)
            {
                canvasManager.skeletonCountPanel.SetActive(true);
            }
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }
}
