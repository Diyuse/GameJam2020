using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button easyButton;
    [SerializeField] private Button hardButton;
    
    private void OnEnable()
    {
        tutorialButton.onClick.AddListener(Tutorial);
        easyButton.onClick.AddListener(Easy);
        hardButton.onClick.AddListener(Hard);
    }

    private void OnDisable()
    {
        tutorialButton.onClick.RemoveAllListeners();
        easyButton.onClick.RemoveAllListeners();
        hardButton.onClick.RemoveAllListeners();
    }
    
    public void GoToLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private void Tutorial()
    {
        GoToLevel("Tutorial");
        
    }
    
    private void Easy()
    {
        GoToLevel("");
    }
    
    private void Hard()
    {
        GoToLevel("Prototype");
    }
}
