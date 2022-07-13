using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseController : MonoBehaviour
{
    bool isPaused = false;
    bool isFinished = false;

    [SerializeField] GameObject panel;
    [SerializeField] GameObject scoreBoard;
    [SerializeField] Button resumeButton;

    private void Start()
    {
        Time.timeScale = 1;
        isPaused = false;
        isFinished = false;
        resumeButton.interactable = true;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad >= 41f && !isFinished)
        {
            LoadEndMenu();
        }
        if (Input.GetButtonDown("Cancel") && !isFinished)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void LoadEndMenu()
    {
        isFinished = true;
        resumeButton.interactable = false;
        PauseGame();
    }

    void PauseGame()
    {
        isPaused = true;
        panel.SetActive(true);
        scoreBoard.SetActive(false);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        panel.SetActive(false);
        scoreBoard.SetActive(true);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNewGame()
    {
        SceneManager.LoadScene(1);
    }
}
