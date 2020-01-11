﻿using System;
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

    private void Start()
    {
        row = GridCreator.startingRow;
        col = GridCreator.startingCol;
        gridSize = GridCreator.gridSize;
        tileSize = GridCreator.tileSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("w") && row > 0)
        {
            if (CheckIfValidMove(row - 1, col))
            {
                //When w is pressed, check if the player is within the grid
                transform.Translate(-tileSize,0,0); //Move the player by the same amount as the tile size
                GridCreator.LowerTile(row, col); //Lower the tile of the original position
                row--; //Keep track of the position of the player
            }
        } else if (Input.GetKeyDown("a") && col > 0)
        {
            if (CheckIfValidMove(row, col-1))
            {
                transform.Translate(0, 0, -tileSize);
                GridCreator.LowerTile(row, col);
                col--;
            }
        } else if (Input.GetKeyDown("s") && row < gridSize - 1)
        {
            if (CheckIfValidMove(row + 1, col))
            {
                transform.Translate(tileSize, 0, 0);
                GridCreator.LowerTile(row, col);
                row++;
            }
        } else if (Input.GetKeyDown("d") && col < gridSize - 1)
        {
            if (CheckIfValidMove(row, col + 1))
            {
                transform.Translate(0, 0, tileSize);
                GridCreator.LowerTile(row, col);
                col++;
            }
        }
        
        //Flip
        if (Input.GetKeyDown("space"))
        {
            GridCreator.Flip();
            GridCreator.LowerTile(row, col);
        }
    }

    private bool CheckIfValidMove(int row, int col)
    {
        if (GridCreator.boardIsFlipped)
        {
            if (GridCreator.myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }else if (!GridCreator.boardIsFlipped)
        {
            if (GridCreator.myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        return true;
    }
}