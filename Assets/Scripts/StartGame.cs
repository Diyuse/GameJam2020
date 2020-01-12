using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]private GameObject boardObject;
    private Board board;

    private void Start()
    {
        board = boardObject.GetComponent<Board>();
        board.GenerateLevel(false);
    }
}
