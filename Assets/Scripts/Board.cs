﻿using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    
    // References to the tiles
    public GameObject[,] myGrid;
    public Tile[,] myTiles;
    
    // Board state
    public bool boardIsFlipped;

    [Header("Grid Setup")]
<<<<<<< HEAD:Assets/Scripts/GridCreator.cs
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform origin;
    private static GameObject grid;
    [SerializeField] public static float tileSize;
    [SerializeField] public static int gridSize;    
=======
    [SerializeField] public GameObject tilePrefab;
    [SerializeField] public Transform origin;
    private GameObject grid;
    [SerializeField] public float tileSize;
    [SerializeField] public int gridSize;
>>>>>>> master:Assets/Scripts/Board.cs
    [Header("Starting Position")]
    [SerializeField] public int startingRow;
    [SerializeField] public int startingCol; 
    [Header("Others")] 
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject flagPrefab;

    private Vector3 position;

    // Flags
    private const int NUMBER_OF_FLAGS = 2;
    public Flag[] flags = new Flag[NUMBER_OF_FLAGS];

    /// <summary>
    /// Creates a hard-coded grid
    /// </summary>
    private void Awake()
    {
        boardIsFlipped = false;
        gridSize = 8;
        tileSize = 1;
        startingRow = 0;
        startingCol = 0;
        myGrid = new GameObject[gridSize, gridSize];
        myTiles = new Tile[gridSize, gridSize];
        grid = GameObject.FindWithTag("Grid");
        
        //Setting up grid
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                //Getting the position to place the tile
                position = new Vector3(i* tileSize, tileSize/2, j*tileSize);
                
                //Creating the tile
                myGrid[i,j] = Instantiate(tilePrefab, position, Quaternion.identity, origin) as GameObject;
                
                //Changing the name
                myGrid[i,j].name = String.Format("Tile {0}:{1}", i, j);
                
                //Setting the status to unchanged as default
                myTiles[i,j] = new Tile(i,j);
                
                //Should try to incorporate the creation of the cube with the actual tile class
                //Can just instantiate here and construct it with the instantiate object
            }
        }
        
        //Testing adding flags
        
        Flag firstFlag = new Flag(7,7,Flag.FlagStatus.UP, this);
        Flag secondFlag = new Flag(0,0,Flag.FlagStatus.DOWN, this);

        flags[0] = firstFlag;
        flags[1] = secondFlag;

        //Setting the starting position of the player
        player.transform.position = origin.position + new Vector3(startingRow, tileSize+1, startingCol);
    }

    /// <summary>
    /// Changes the state of a tile at row:col.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void LowerTile(int row, int col)
    {
        if (myTiles[row, col].CurrentStatus == Tile.TileStatus.UNCHANGED)
        {
            myGrid[row,col].transform.Translate(0,-tileSize,0);
            myTiles[row, col].SetStatus(Tile.TileStatus.CHANGED);
        } else if (myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
        {
            myGrid[row,col].transform.Translate(0,tileSize,0);
            myTiles[row, col].SetStatus(Tile.TileStatus.UNCHANGED);
        }
    }

    /// <summary>
    /// Flips the state of the board.
    /// </summary>
    public void Flip()
    {
        if (boardIsFlipped)
        {
            boardIsFlipped = false;
            grid.transform.localScale = new Vector3(1,1,1);
        }
        else
        {
            boardIsFlipped = true;
            grid.transform.localScale = new Vector3(1,-1,1);
        }
    }

    /// <summary>
    /// Sets the tile of row:col to DELETED and disable the object in the scene.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public void DeleteTile(int row, int col)
    {
        myTiles[row, col].SetStatus(Tile.TileStatus.DELETED);
        myGrid[row, col].SetActive(false);
    }

}