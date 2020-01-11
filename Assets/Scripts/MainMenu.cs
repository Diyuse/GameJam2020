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
        continueButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(StartGame);
        exitButton.onClick.AddListener(StartGame);
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
        // GoToLevel("MainLevel");
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
