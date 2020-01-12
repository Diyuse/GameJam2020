using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Board : MonoBehaviour
{
    
    // References to the tiles
    public GameObject[,] myGrid;
    public Tile[,] myTiles;
    
    // Board state
    public bool boardIsFlipped;

    [Header("Grid Setup")]
    [SerializeField] public GameObject tilePrefab;
    [SerializeField] public Transform origin;
    private GameObject grid;
    [SerializeField] public float tileSize;
    [SerializeField] public int gridSize;
    [Header("Starting Position")]
    [SerializeField] public int startingRow;
    [SerializeField] public int startingCol; 
    [Header("Others")] 
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject flagPrefab;
    [SerializeField] public Material tileBaseMaterial;
    [SerializeField] public Material tileBorderMaterial;
    [SerializeField] public Material tileFlippedBaseMaterial;
    [SerializeField] public Material tileFlippedBorderMaterial;
    [SerializeField] public Material tileBaseColor;

    private Vector3 position;

    // Flags
    private int numberOfFlags;
    public Flag[] flags;

    public int NumberOfFlags { get => numberOfFlags; }



    /// <summary>
    /// Creates a hard-coded grid
    /// </summary>
    public void GenerateLevel(bool isTutorial)
    {
        tileBaseColor = tileBaseMaterial;
        if (!isTutorial)
        {
            boardIsFlipped = false;
            gridSize = 16;
            tileSize = 1;
            startingRow = 0;
            startingCol = 0;
            myGrid = new GameObject[gridSize, gridSize];
            myTiles = new Tile[gridSize, gridSize];
            grid = GameObject.FindWithTag("Grid");

            numberOfFlags = 2;
            flags = new Flag[numberOfFlags];

            var reader = new StreamReader(File.OpenRead("map.csv"));

            //Setting up grid
            for (int i = 0; i < gridSize; i++)
            {
                var line = reader.ReadLine();


                for (int j = 0; j < gridSize; j++)
                {

                    var values = line.Split(',');

                    //Getting the position to place the tile
                    position = new Vector3(i * tileSize, tileSize / 2, j * tileSize);

                    //Creating the tile
                    myGrid[i, j] = Instantiate(tilePrefab, position, Quaternion.identity, origin) as GameObject;

                    //Changing the name
                    myGrid[i, j].name = String.Format("Tile {0}:{1}", i, j);

                    //Setting the status to unchanged as default
                    myTiles[i, j] = new Tile(i, j);

                    if (values[j].Equals("1"))
                    {
                        LowerTile(i, j);
                    }

                    else if (values[j].Equals("2"))
                    {
                        DeleteTile(i, j);

                    }
                }
            }
            //Testing adding flags
            List<int[]> validTilePos = new List<int[]>();

            // Valid spots to move to
            for (int i=0; i < gridSize; i++)
            {
                for (int j=0; j < gridSize; j++)
                {
                    if ((myTiles[i, j].CurrentStatus == Tile.TileStatus.UNCHANGED && !boardIsFlipped ) || (myTiles[i, j].CurrentStatus == Tile.TileStatus.CHANGED && boardIsFlipped) && i != 0 && j != 0)
                    {
                        validTilePos.Add(new int[2] { i, j });
                    }
                }
            }
            
            System.Random rnd = new System.Random();
            int tileIndex1 = rnd.Next(validTilePos.Count);
            int tileIndex2 = rnd.Next(validTilePos.Count);

            Flag firstFlag = new Flag(validTilePos[tileIndex1][0], validTilePos[tileIndex1][1], Flag.FlagStatus.UP, this);
            Flag secondFlag = new Flag(validTilePos[tileIndex2][0], validTilePos[tileIndex2][1], Flag.FlagStatus.DOWN, this);

            flags[0] = firstFlag;
            flags[1] = secondFlag;

            //Setting the starting position of the player
            player.transform.position = origin.position + new Vector3(startingRow, tileSize + 1, startingCol);

        }

        // tutorial
        else{
        boardIsFlipped = false;
        gridSize = 3;
        tileSize = 1;
        startingRow = 2;
        startingCol = 0;
        myGrid = new GameObject[gridSize, gridSize];
        myTiles = new Tile[gridSize, gridSize];
        grid = GameObject.FindWithTag("Grid");

        numberOfFlags = 1;
        flags = new Flag[numberOfFlags];

        var reader = new StreamReader(File.OpenRead("tutorial.csv"));

        //Setting up grid
        for (int i = 0; i < gridSize; i++)
        {
            var line = reader.ReadLine();

            for (int j = 0; j < gridSize; j++)
            {
            
                var values = line.Split(',');

                //Getting the position to place the tile
                position = new Vector3(i * tileSize, tileSize / 2, j * tileSize);

                //Creating the tile
                myGrid[i, j] = Instantiate(tilePrefab, position, Quaternion.identity, origin) as GameObject;

                //Changing the name
                myGrid[i, j].name = String.Format("Tile {0}:{1}", i, j);

                //Setting the status to unchanged as default
                myTiles[i, j] = new Tile(i, j);

                if (values[j].Equals("1"))
                {
                    LowerTile(i, j);
                }

                else if (values[j].Equals("2"))
                {
                    DeleteTile(i, j);
                }
            }
        }
        //Testing adding flags
        
        Flag firstFlag = new Flag(0, 2, Flag.FlagStatus.UP, this);

        flags[0] = firstFlag;

        //Setting the starting position of the player
        player.transform.position = origin.position + new Vector3(startingRow, tileSize + 1, startingCol);
        }        
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


            Renderer rend = myGrid[row,col].GetComponent<Renderer>();
            rend.material = tileFlippedBorderMaterial;
            tileBaseMaterial = tileFlippedBaseMaterial;

        } else if (myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
        {
            myGrid[row,col].transform.Translate(0,tileSize,0);
            myTiles[row, col].SetStatus(Tile.TileStatus.UNCHANGED);
            Renderer rend = myGrid[row, col].GetComponent<Renderer>();
            rend.material = tileBorderMaterial;
            tileBaseMaterial = tileBaseColor;
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
