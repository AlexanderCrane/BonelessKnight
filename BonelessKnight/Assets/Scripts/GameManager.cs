using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numSkeletonsRemaining;
    
    private CanvasManager canvasManager;
    
    private void Awake() {
        DontDestroyOnLoad(this.gameObject);    
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasManager =  FindObjectOfType<CanvasManager>();

        if(canvasManager == null && SceneManager.GetActiveScene().name != "MainMenu" &&  SceneManager.GetActiveScene().name != "Intro")
        {
            Debug.LogError("Cannot find main UI canvas or it is missing CanvasManager component!");
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name + " with mode: " + mode);

        canvasManager =  FindObjectOfType<CanvasManager>();

        if(canvasManager == null)
        {
            Debug.LogError("Cannot find main UI canvas or it is missing CanvasManager component!");
        }

        if(SceneManager.GetActiveScene().name != "MainMenu" &&  SceneManager.GetActiveScene().name != "Intro")
        {
            canvasManager.skeletonCountPanel.SetActive(true);
            canvasManager.skeletonCountTmproText.text = "Skeletons Remaining: " + numSkeletonsRemaining;
        }

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void ReduceSkeletonCount()
    {
        numSkeletonsRemaining--;
        canvasManager.skeletonCountTmproText.text = "Skeletons Remaining: " + numSkeletonsRemaining;
    }

}
