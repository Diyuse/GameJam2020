using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private int row;
    private int col;
    private float tileSize;
    private int gridSize;
    private int[] playerCoord;

    private Board grid;
    public GameObject gridGameObject;

    private PlayerController playerController;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        grid = gridGameObject.GetComponent<Board>();


        // Choose random starting coord
        System.Random rand = new System.Random();
        gridSize = grid.gridSize;
        int x, y;
        x = rand.Next(gridSize);
        y = rand.Next(gridSize);
        tileSize = grid.tileSize;

        transform.position = new Vector3(x * tileSize, 2, y * tileSize);

        // Check valid
        while (!CheckIfValidMove(x, y)) {
            x = rand.Next(gridSize);
            y = rand.Next(gridSize);
        }

        row = x;
        col = y;
        
    }

    // Update is called once per frame
    public int[] moveEnemy()
    {
        playerCoord = playerController.GetPlayerCoords();
        // Random move
        switch (Move())
        {
            case 0:
                // Up
                if (row > 0 && CheckIfValidMove(row - 1, col))
                {
                    transform.Translate(-tileSize, 0, 0); //Move the player by the same amount as the tile size
                    row--; //Keep track of the position of the player
                }
                break;

            case 1:
                // Down
                if (row + 1 < gridSize && CheckIfValidMove(row + 1, col))
                {
                    transform.Translate(tileSize, 0, 0);
                    row++;
                }
                break;

            case 2:
                // Left
                if (col > 0 && CheckIfValidMove(row, col - 1))
                {
                    transform.Translate(0, 0, -tileSize);
                    col--;
                }
                break;

            case 3:
                // Right
                if (col + 1 < gridSize && CheckIfValidMove(row, col + 1))
                {
                    transform.Translate(0, 0, tileSize);
                    col++;
                }
                break;

            default:
                break;
        }

        // Check result
        System.Random rnd = new System.Random();
        List<int[]> validTilePos = new List<int[]>();

        // Valid spots to move
        for (int i=0; i < gridSize; i++)
        {
            for (int j=0; j < gridSize; j++)
            {
                if ((grid.myTiles[i, j].CurrentStatus == Tile.TileStatus.UNCHANGED && !grid.boardIsFlipped ) || (grid.myTiles[i, j].CurrentStatus == Tile.TileStatus.CHANGED && grid.boardIsFlipped) && i != row && j != col)
                {
                    validTilePos.Add(new int[2] { i, j });
                }
            }
        }

        // Upon collision with PLAYER, PLAYER gets teleported to random valid spot
        if (playerCoord.SequenceEqual(GetEnmCoords()))
        {
            int[] tilePos = validTilePos[rnd.Next(validTilePos.Count)];
            playerController.transform.position = new Vector3(tilePos[0] * tileSize, 2, tilePos[1] * tileSize);
            return tilePos;
        }
        else return new int[] { };
    }

    

    /// <summary>
    /// Get enemy position
    /// </summary>
    /// <returns></returns>
    private int[] GetEnmCoords()
    {
        return new int[2] { row, col };
    }

    /// <summary>
    /// Choose a direction to move
    /// </summary>
    /// <returns></returns>
    private int Move()
    {
        System.Random rand = new System.Random();
        return rand.Next(4);
    }


    /// <summary>
    /// Valid tile to move to
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns></returns>
    private bool CheckIfValidMove(int row, int col)
    {
        if (grid.boardIsFlipped)
        {
            if (grid.myTiles[row, col].CurrentStatus == Tile.TileStatus.CHANGED)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else if (!grid.boardIsFlipped)
        {
            if (grid.myTiles[row, col].CurrentStatus == Tile.TileStatus.UNCHANGED)
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
}
