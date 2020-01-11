using UnityEngine;

public class Flag
{
    private int row;
    private int col;
    FlagStatus flagStatus;
    
    public enum FlagStatus
    {
        UP,
        DOWN
    }
    
    /// <summary>
    /// Instantiates a Flag GameObject
    /// </summary>
    public Flag(int row, int col, FlagStatus fs, Grid grid){
        this.row = row;
        this.col = col;
        this.flagStatus = fs;

        float tileSize = grid.tileSize;

        Vector3 position = new Vector3(row* tileSize, tileSize/2 + tileSize, col*tileSize);
        GameObject flag = GameObject.Instantiate(grid.flagPrefab, position, Quaternion.identity, grid.origin);
        
    }
}