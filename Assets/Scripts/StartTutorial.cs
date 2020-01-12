using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class StartTutorial : MonoBehaviour
{
    [SerializeField]private GameObject boardObject;
    private Board board;

    private void Start()
    {
        board = boardObject.GetComponent<Board>();
        board.GenerateLevel(true);
    }
}
