using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    private int row;
    private int col;
    private int gridSize;
    private float tileSize;
    private bool justFlipped;

    private Board board;
    public GameObject boardGameObject;

    private void Start()
    {
        board = boardGameObject.GetComponent<Board>();

        row = board.startingRow;
        col = board.startingCol;
        gridSize = board.gridSize;
        tileSize = board.tileSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && row > 0)
        {
            if (CheckIfValidMove(row - 1, col))
            {
                //When w is pressed, check if the player is within the grid
                TileRemoval(); // Check if the tile will be removed and does it
                transform.Translate(-tileSize,0,0); //Move the player by the same amount as the tile size
                board.LowerTile(row, col); //Lower the tile of the original position
                row--; //Keep track of the position of the player
            }
        } else if (Input.GetKeyDown("a") && col > 0)
        {
            if (CheckIfValidMove(row, col-1))
            {
                TileRemoval();
                transform.Translate(0, 0, -tileSize);
                board.LowerTile(row, col);
                col--;
            }
        } else if (Input.GetKeyDown("s") && row < gridSize - 1)
        {
            if (CheckIfValidMove(row + 1, col))
            {
                TileRemoval();
                transform.Translate(tileSize, 0, 0);
                board.LowerTile(row, col);
                row++;
            }
        } else if (Input.GetKeyDown("d") && col < gridSize - 1)
        {
            if (CheckIfValidMove(row, col + 1))
            {
                TileRemoval();
                transform.Translate(0, 0, tileSize);
                board.LowerTile(row, col);
                col++;
            }
        }

        //Flip
        if (Input.GetKeyDown("space"))
        {
            if (board.myTiles[row, col].CurrentStatus != Tile.TileStatus.DELETED)
            {
                board.Flip();
                board.LowerTile(row, col);
                justFlipped = true;
            }
        }
    }

    /// <summary>
    /// Checks if the tile in the input direction is valid of not.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    private bool CheckIfValidMove(int row, int col)
    {
        if (board.boardIsFlipped)
        {
            if (board.myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }else if (!board.boardIsFlipped)
        {
            if (board.myTiles[row, col].CurrentStatus == Tile.TileStatus.UNCHANGED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Removes the tile if it's a movement following a flip.
    /// </summary>
    private void TileRemoval()
    {
        if (justFlipped)
        {
            board.DeleteTile(row, col);
            justFlipped = false;
        }
    }

    /// <summary>
    /// Get the player's current coordinates
    /// </summary>
    public int[] GetPlayerCoords(){
        int[] coords = {this.row, this.col};
        return coords;
    }

    /// <summary>
    /// Check if the player has won the game/level
    /// </summary>
    public bool HasWonGame(){
        return false;
    }

    /// <summary>
    /// Check if the player is on a flag
    /// </summary>
    public bool IsOnFlag(){

        foreach(Flag f in board.flags){
            if (f.Col == this.col && f.Row == this.row 
                && f.GetFlagStatus == Flag.FlagStatus.UP){
                return true;
            }
        }
        return false;
    }
}
