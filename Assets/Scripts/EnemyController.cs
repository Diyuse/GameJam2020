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

        playerCoord = playerController.GetPlayerCoords();
        // Choose random starting coord
        System.Random rand = new System.Random();
        gridSize = grid.gridSize;
        int x, y;
        x = rand.Next(gridSize);
        y = rand.Next(gridSize);
        tileSize = grid.tileSize;

        // Check valid
        while (!CheckIfValidMove(x, y) || playerCoord.SequenceEqual(new int[]{ x,y})) {
            x = rand.Next(gridSize);
            y = rand.Next(gridSize);
        }

        row = x;
        col = y;

        transform.position = new Vector3(x * tileSize, 2, y * tileSize);

    }

    /// <summary>
    /// Moves enemy randomnly
    /// </summary>
    /// <returns></returns>
    public int[] moveEnemy()
    {
        playerCoord = playerController.GetPlayerCoords();
        int[] enemyCoord = GetEnmCoords();


        // Valid moves
        List<PlayerController.Direction> validMoves = new List<PlayerController.Direction>();
        List<PlayerController.Direction> directions = new List<PlayerController.Direction>();
        int newRow = row;
        int newCol = col;

        if (row > 0 && CheckIfValidMove(row - 1, col)) validMoves.Add(PlayerController.Direction.NORTH);
        if (row + 1 < gridSize && CheckIfValidMove(row + 1, col)) validMoves.Add(PlayerController.Direction.SOUTH);
        if (col + 1 < gridSize && CheckIfValidMove(row, col + 1)) validMoves.Add(PlayerController.Direction.EAST);
        if (col > 0 && CheckIfValidMove(row, col - 1)) validMoves.Add(PlayerController.Direction.WEST);
        if (playerCoord.SequenceEqual(enemyCoord)) validMoves.Clear();
        if (validMoves.Count != 0)
        {
            // Toward player
            foreach (PlayerController.Direction move in validMoves)
            {
                switch (move)
                {
                    case PlayerController.Direction.NORTH:
                        if (playerCoord[1] == enemyCoord[1] && playerCoord[0] < enemyCoord[0]) { 
                            directions.Add(PlayerController.Direction.NORTH);
                        }
                        break;
                    case PlayerController.Direction.SOUTH:
                        if (playerCoord[1] == enemyCoord[1] && playerCoord[0] > enemyCoord[0])
                        {
                            directions.Add(PlayerController.Direction.SOUTH);
                        }
                        break;
                    case PlayerController.Direction.EAST:
                        if (playerCoord[0] == enemyCoord[0] && playerCoord[1] > enemyCoord[1])
                        {
                            directions.Add(PlayerController.Direction.EAST);
                        }
                        break;
                    case PlayerController.Direction.WEST:
                        if (playerCoord[0] == enemyCoord[0] && playerCoord[1] < enemyCoord[1])
                        {
                            directions.Add(PlayerController.Direction.WEST);
                        }
                        break;
                }
            }
        }

        // Random move from possible dir
        System.Random rnd = new System.Random();
        Vector3 destination = new Vector3(0,0,0);
        PlayerController.Direction dir;
        if (directions.Count > 0)
        {
            dir = directions[rnd.Next(directions.Count)];

            switch (dir)
            {
                case PlayerController.Direction.NORTH:
                    destination = new Vector3(-tileSize, 0, 0);
                    newRow = row - 1;
                    break;
                case PlayerController.Direction.SOUTH:
                    destination = new Vector3(tileSize, 0, 0);
                    newRow = row + 1;
                    break;
                case PlayerController.Direction.EAST:
                    destination = new Vector3(0, 0, tileSize);
                    newCol = col + 1;
                    break;
                case PlayerController.Direction.WEST:
                    destination = new Vector3(0, 0, -tileSize);
                    newCol = col - 1;
                    break;
            }
        }
        else if (validMoves.Count > 0) {
            dir = validMoves[rnd.Next(validMoves.Count)];
            switch (dir)
            {
                case PlayerController.Direction.NORTH:
                    destination = new Vector3(-tileSize, 0, 0);
                    newRow = row - 1;
                    break;
                case PlayerController.Direction.SOUTH:
                    destination = new Vector3(tileSize, 0, 0);
                    newRow = row + 1;
                    break;
                case PlayerController.Direction.EAST:
                    destination = new Vector3(0, 0, tileSize);
                    newCol = col + 1;
                    break;
                case PlayerController.Direction.WEST:
                    destination = new Vector3(0, 0, -tileSize);
                    newCol = col - 1;
                    break;
            }
        }

        // translate
        transform.Translate(destination);
        // update current position
        col = newCol;
        row = newRow;


        // Check result
        List<int[]> validTilePos = new List<int[]>();

        // Valid spots to move to
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
            // Add effects

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

    public void spawnPoint(int row, int col)
    {
        this.row = row;
        this.col = col;
        transform.position = new Vector3(row * tileSize, 2, col * tileSize);
    }
}
