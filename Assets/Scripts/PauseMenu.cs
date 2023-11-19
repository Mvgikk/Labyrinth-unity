using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public Button resumeButton;
    public Button quitButton;
    public Button menuButton;
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
        menuButton.onClick.AddListener(MenuButtonClicked);
    }

    public void ResumeButtonClicked()
    {
        TogglePause();
    }
    public void QuitButtonClicked()
    {
        Application.Quit();
    }

    public void MenuButtonClicked()
    {
       // SceneManager.LoadScene("MainMenu");
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