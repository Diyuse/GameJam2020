using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Vector3 moveVector;

    
    public Canvas canvas;

    public Board board;

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
        else if (Input.GetKeyDown("t")){
            board.GenerateLevel(true);
        }
        else if (Input.GetKeyDown("l")){
            board.GenerateLevel(false);
        } else if (Input.GetKeyDown(("m")))
        {
            SceneManager.LoadScene("MainMenu");
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
