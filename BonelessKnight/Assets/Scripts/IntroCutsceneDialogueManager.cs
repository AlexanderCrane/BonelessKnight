using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutsceneDialogueManager : DialogueManager
{
    public Animator wizardAnimator;
    public GameManager gameManager;
    public AudioSource scream;
    public GameObject staffParticleSource;
    public GameObject playerParticleSource;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    protected override void Start()
    {
        base.Start();
        base.Initialize();
    }

    public override void NextPressed()
    {
        if(currentLine < linesOfDialogue.Count-1)
        {
            currentLine++;
            canvasManager.dialogueTmproText.text = linesOfDialogue[currentLine];
        }
        else
        {
            wizardAnimator.SetBool("Attacking", true);

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
            StartCoroutine(WaitToLoad());
        }
    }

    public IEnumerator WaitToLoad()
    {
        staffParticleSource.SetActive(true);
        yield return new WaitForSeconds(3);
        
        playerParticleSource.SetActive(true);
        scream.Play();

        yield return new WaitForSeconds(1);

        if(gameManager != null)
        {
            gameManager.LoadScene("Level1");
        }
        else //for testing in editor, otherwise should never be null
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
