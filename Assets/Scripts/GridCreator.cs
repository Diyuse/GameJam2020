using System;
using UnityEngine;

public class GridCreator : MonoBehaviour
{
    private GameObject[,] myGrid;
    private Tile[,] myTiles;
    [Header("Grid Setup")]
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private float tileSize;
    [SerializeField] private Transform origin;
    [SerializeField] private int gridSize;
    [Header("Starting Position")]
    [SerializeField] private int startingRow;
    [SerializeField] private int startingCol;
    [Header("Ending Position")]
    [SerializeField] private int endRow;
    [SerializeField] private int endCol;
    private void Awake()
    {
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
        
        // myGrid[0,0].transform.Translate(0,-1,0);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lowerTile(4,4);
        }
    }

    public void lowerTile(int row, int col)
    {
        if (myTiles[row, col].CurrentStatus == Tile.TileStatus.UNCHANGED)
        {
            myGrid[row,col].transform.Translate(0,-tileSize,0);
            myTiles[row, col].setStatus(Tile.TileStatus.CHANGED);
        } else if (myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
        {
            myGrid[row,col].transform.Translate(0,tileSize,0);
            myTiles[row, col].setStatus(Tile.TileStatus.UNCHANGED);
        }
    }
    
}
