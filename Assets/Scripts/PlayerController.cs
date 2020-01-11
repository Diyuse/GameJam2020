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

    public enum Direction{
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

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
        // controls for moving
        if (Input.GetKeyDown("w") || Input.GetKeyDown("up"))
        {
            Move(Direction.NORTH); //(row - 1, col)

        } else if (Input.GetKeyDown("a") || Input.GetKeyDown("left"))
        {
            Move(Direction.WEST); //(row, col-1)

        } else if (Input.GetKeyDown("s") || Input.GetKeyDown("down"))
        {
            Move(Direction.SOUTH); //(row + 1, col)
            
        } else if (Input.GetKeyDown("d") || Input.GetKeyDown("right"))
        {
            Move(Direction.EAST); //(row, col + 1);
        
        }

        // Flip
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
    /// <param name="direction"/>
    private void Move(Direction direction)
    {
        int nextRow = row;
        int nextCol = col;

        // Check for board bounds
        switch (direction){
            case Direction.NORTH:
                nextRow--;
                if (row <= 0) return;
                break;
            case Direction.SOUTH:
                nextRow++;
                if (row >= gridSize - 1) return;
                break;
            case Direction.EAST:
                nextCol++;
                if (col >= gridSize - 1) return;
                break;
            case Direction.WEST:
                nextCol--;
                if (col <= 0) return;
                break;
        }

        // Check if altitudes match
        if (board.boardIsFlipped 
            && board.myTiles[nextRow, nextCol].CurrentStatus != Tile.TileStatus.CHANGED)
            return;

        if (!board.boardIsFlipped
            && board.myTiles[nextRow, nextCol].CurrentStatus != Tile.TileStatus.UNCHANGED)
            return;        

        // valid moves
        
        // attempt to remove the tile
        TileRemoval();

        // lower the tile
        board.LowerTile(row, col);

        // destination position
        Vector3 destination = new Vector3(0,0,0);

        switch (direction){
            case Direction.NORTH:
                destination = new Vector3(-tileSize,0,0);
                break;
            case Direction.SOUTH:
                destination = new Vector3(tileSize, 0, 0);
                break;
            case Direction.EAST:
                destination = new Vector3(0, 0, tileSize);
                break;
            case Direction.WEST:
                destination = new Vector3(0, 0, -tileSize);
                break;
        }

        // translate
        transform.Translate(destination);

        // update current position
        this.col = nextCol;
        this.row = nextRow;

        // pick up a flag
        Flag flag = IsOnFlag();
        if (flag != null) {
            flag.CollectFlag();
        }

        // check if won
        if (HasWonGame()) {
            Debug.Log("Win!!");
        }
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
        
        foreach(Flag f in board.flags){
            if (!f.Collected) return false;
        }

        return true;
    }

    /// <summary>
    /// Check if the player is on a flag
    /// </summary>
    public Flag IsOnFlag(){

        Flag.FlagStatus match;
        if (board.boardIsFlipped) match = Flag.FlagStatus.DOWN;
        else match = Flag.FlagStatus.UP;

        foreach(Flag f in board.flags){
            if (f.Col == this.col && f.Row == this.row 
                && f.GetFlagStatus == match){
                return f;
            }
        }
        return null;
    }
}
