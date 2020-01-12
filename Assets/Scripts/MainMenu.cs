using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(StartGame);
        continueButton.onClick.AddListener(ContinueGame);
        optionsButton.onClick.AddListener(Options);
        exitButton.onClick.AddListener(Exit);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveAllListeners();
        continueButton.onClick.RemoveAllListeners();
        optionsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
    
    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void StartGame()
    {
        GoToLevel("LevelSelector");
    }

    private void ContinueGame()
    {
        // GoToLevel("MainLevel");
    }

    private void Options()
    {
        // GoToLevel("Options");
    }

    private void Exit()
    {
        Application.Quit();
    }
}
