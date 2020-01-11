using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Canvas canvas;

    private void Awake(){
        canvas.enabled = false;
    }

    private void Update(){
        if (Input.GetKeyDown("r")){
            Restart();
        }
        else if (Input.GetKeyDown("q") || Input.GetKeyDown(KeyCode.Escape)){
            Quit();
        }
    }

    public void WinGame(){
        canvas.enabled = true;
    }

    public void Restart(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(){
        Application.Quit();
    }
}
