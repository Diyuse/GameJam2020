using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class StartTutorial : MonoBehaviour
{
    [SerializeField]private GameObject boardObject;
    [SerializeField] private Text text;
    private Board board;
    private string playerInput;

    private void Start()
    {
        playerInput = "";
        text.text = "WELCOME TO THE TUTORIAL, PRESS 'D' TO MOVE RIGHT" ;
        board = boardObject.GetComponent<Board>();
        board.GenerateLevel(true);
    }

    private void Update()
    {
        if (playerInput == "dwwaww")
        {
            text.text = "HIT 'M' TO GO BACK TO THE MAIN MENU";
        } else
        if (playerInput == "dwwaw")
        {
            if (Input.GetKeyDown("w"))
            {
                text.text = "KEEP GOING";
                playerInput += "w";
            }
        } else if (playerInput == "dwwa")
        {
            if (Input.GetKeyDown("w"))
            {
                text.text = "YOU MAY HIT 'SPACE' AT ANY TIME TO FLIP THE WOOORLD";
                playerInput += "w";
            }
        } else if (playerInput == "dww")
        {
            if (Input.GetKeyDown("a"))
            {
                text.text = "NOW GO GET THE ORB OF LIGHT";
                playerInput += "a";
            }
        } else
        if (playerInput == "dw")
        {
            if (Input.GetKeyDown("w"))
            {
                text.text = "WATCH OUT THERE'S A WOLF, PRESS 'A' TO SEE THE EFFECT";
                playerInput += "w";
            }
        } else
        if (playerInput == "d")
        {
            if (Input.GetKeyDown("w"))
            {
                text.text = "KEEP GOING";
                playerInput += "w";
            }
        } else
        if (playerInput == "")
        {
            if (Input.GetKeyDown("d"))
            {
                text.text = "PRESS 'W' TO GO UP";
                playerInput += "d";
            }
        }
    }
}
