using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public Button resumeButton;
    public Button quitButton;
    public GameObject pauseMenuPanel;
 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("PPPP");
            TogglePause();
        }
    }
    private void Start() {
        resumeButton.onClick.AddListener(ResumeButtonClicked);
        quitButton.onClick.AddListener(QuitButtonClicked);
    }

    public void ResumeButtonClicked()
    {
        TogglePause();
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pauseMenuPanel.SetActive(true);
            Time.timeScale = 0;

        }
        else
        {

            pauseMenuPanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
}