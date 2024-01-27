using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Canvas diageticCanvas;
    public DialogueCanvasManager dialogueCanvasManager;
    public List<string> linesOfDialogue;
    public int currentLine = 0;
    bool shouldPauseWhenTalking = true;
    private bool closeEnoughToTalk;

    // Start is called before the first frame update
    void Start()
    {
        dialogueCanvasManager = FindObjectOfType<DialogueCanvasManager>();
        diageticCanvas.worldCamera = Camera.main;
        diageticCanvas.gameObject.SetActive(false);
        dialogueCanvasManager.nextButton.onClick.AddListener(() => NextPressed());
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && closeEnoughToTalk)
        {
            Initialize();
        }

    }

    public void Initialize()
    {
        currentLine = 0;
        Debug.Log("Initializing dialogue for " + this.gameObject.name);
        dialogueCanvasManager.dialogueWindow.SetActive(true);
        Cursor.visible = true;

        if(shouldPauseWhenTalking)
        {
            Time.timeScale = 0;
        }

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

            dialogueCanvasManager.nextButton.onClick.RemoveListener(NextPressed);
            currentLine = 0;

            // close dialogue
            dialogueCanvasManager.dialogueWindow.SetActive(false);
            Time.timeScale = 1;
            Cursor.visible = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered dialogue trigger");
            diageticCanvas.gameObject.SetActive(true);
            closeEnoughToTalk = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player Exited dialogue trigger");
            closeEnoughToTalk = false;
            diageticCanvas.gameObject.SetActive(false);
        }
    }
}

// public class Dialogue
// {
//     public string dialogueText;
// }
