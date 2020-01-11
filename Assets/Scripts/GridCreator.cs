using System;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    public static GameObject[,] myGrid;
    public static Tile[,] myTiles;
    public static bool boardIsFlipped;
    [Header("Grid Setup")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private Transform origin;
    [SerializeField] public static float tileSize;
    [SerializeField] public static int gridSize;
    [Header("Starting Position")]
    [SerializeField] public static int startingRow;
    [SerializeField] public static int startingCol;
    [Header("Ending Position")]
    [SerializeField] private int endRow;
    [SerializeField] private int endCol;
    [Header("Others")] [SerializeField] private GameObject player;
    private void Awake()
    {
        boardIsFlipped = false;
        gridSize = 8;
        tileSize = 1;
        startingRow = 0;
        startingCol = 0;
        myGrid = new GameObject[gridSize, gridSize];
        myTiles = new Tile[gridSize, gridSize];
        
        //Setting up grid
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                //Getting the position to place the tile
                Vector3 position = new Vector3(i* tileSize, tileSize/2, j*tileSize);
                
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

        //Setting the starting position of the player
        player.transform.position = origin.position + new Vector3(startingRow, tileSize+1, startingCol);
    }

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

    public static void Flip()
    {
        Debug.Log("Board has been flipped");
    }

}
