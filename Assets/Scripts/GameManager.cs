using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public Vector3 moveVector;

    
    public Canvas canvas;

    private void Awake(){
        canvas.enabled = false;
    }

    public void WinGame(){
        canvas.enabled = true;
    }
}
