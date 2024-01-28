using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Canvas diageticCanvas;
    public CanvasManager canvasManager;
    public List<string> linesOfDialogue;
    public int currentLine = 0;
    public bool isCutscene = false;
    public bool shouldPauseWhenTalking = true;
    private bool closeEnoughToTalk;

    // Start is called before the first frame update
    void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        canvasManager.nextButton.onClick.AddListener(() => NextPressed());
        if(!isCutscene)
        {
            diageticCanvas.worldCamera = Camera.main;
            diageticCanvas.gameObject.SetActive(false);
        }
        else
        {
            Initialize();
        }
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

    public virtual void NextPressed()
    {
        Debug.Log("Next pressed");
        if(currentLine < linesOfDialogue.Count-1)
        {
            currentLine++;
            canvasManager.dialogueTmproText.text = linesOfDialogue[currentLine];
        }
        else
        {
            Debug.Log("Closing dialogue");

            canvasManager.nextButton.onClick.RemoveListener(NextPressed);
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
