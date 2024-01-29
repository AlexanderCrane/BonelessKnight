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
    protected bool closeEnoughToTalk;
    public bool alreadyTalking;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        canvasManager = FindObjectOfType<CanvasManager>();
        if(!isCutscene)
        {
            diageticCanvas.worldCamera = Camera.main;
            diageticCanvas.gameObject.SetActive(false);
        }
        else
        {
            // Initialize();
        }
    }

    protected virtual void Update() 
    {
        if(Input.GetKeyDown(KeyCode.E) && closeEnoughToTalk && !alreadyTalking)
        {
            Initialize();
        }

    }

    public virtual void Initialize()
    {
        alreadyTalking = true;
        canvasManager.nextButton.onClick.AddListener(() => NextPressed());

        currentLine = 0;
        Debug.Log("Initializing dialogue for " + this.gameObject.name);
        Debug.Log("There are " + linesOfDialogue.Count + " lines of dialogue");
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
            Debug.Log("Moving on to next line");
            currentLine++;
            canvasManager.dialogueTmproText.text = linesOfDialogue[currentLine];
        }
        else
        {
            Debug.Log("Closing dialogue");
            currentLine = 0;

            canvasManager.nextButton.onClick.RemoveAllListeners();

            // close dialogue
            canvasManager.dialogueWindow.SetActive(false);
            if(!isCutscene)
            {
                canvasManager.skeletonCountPanel.SetActive(true);
            }
            Time.timeScale = 1;
            Cursor.visible = false;
            alreadyTalking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Player entered dialogue trigger for " +  this.gameObject.name);
            diageticCanvas.gameObject.SetActive(true);
            closeEnoughToTalk = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canvasManager.nextButton.onClick.RemoveAllListeners();

            Debug.Log("Player Exited dialogue trigger");
            closeEnoughToTalk = false;
            diageticCanvas.gameObject.SetActive(false);
        }
    }
}

