using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public static GameObject[,] myGrid;
    public static Tile[,] myTiles;
    public static bool boardIsFlipped;

    [Header("Grid Setup")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform origin;
    private static GameObject grid;
    [SerializeField] public static float tileSize;
    [SerializeField] public static int gridSize;
    [Header("Starting Position")]
    [SerializeField] public static int startingRow;
    [SerializeField] public static int startingCol; 
    [Header("Others")] 
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject flagPrefab;

    private Vector3 position;

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
                myTiles[i,j] = new Tile(Tile.TileStatus.UNCHANGED);
                
                //Should try to incorporate the creation of the cube with the actual tile class
                //Can just instantiate here and construct it with the instantiate object
            }
        }
        
        //Testing adding flags
        //myTiles[7,7].SetFlagStatus(Tile.FlagStatus.UP);
        Flag firstFlag = new Flag(7,7,Flag.FlagStatus.UP);
        Flag secondFlag = new Flag(0,0,Flag.FlagStatus.DOWN);

        position = new Vector3(7* tileSize, tileSize/2 + tileSize, 7*tileSize);
        GameObject flag1 = GameObject.Instantiate(flagPrefab, position, Quaternion.identity, origin);
        
        //myTiles[0,7].SetFlagStatus(Tile.FlagStatus.DOWN);
        position = new Vector3(0, tileSize/2 - (2 *tileSize), 7*tileSize);
        GameObject flag2 = GameObject.Instantiate(flagPrefab, position, Quaternion.identity, origin);

        //Setting the starting position of the player
        player.transform.position = origin.position + new Vector3(startingRow, tileSize+1, startingCol);
    }

    /// <summary>
    /// Changes the state of a tile at row:col.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public static void LowerTile(int row, int col)
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
    public static void Flip()
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
    public static void DeleteTile(int row, int col)
    {
        myTiles[row, col].SetStatus(Tile.TileStatus.DELETED);
        myGrid[row, col].SetActive(false);
    }

}
