using System;
using UnityEngine;

public class Flag
{
    private int row;
    private int col;
    private FlagStatus flagStatus;
    private bool collected = false;

    public int Row { get => row; }
    public int Col { get => col; }
    public FlagStatus GetFlagStatus { get => flagStatus; }
    public bool Collected { get => collected; }

    private GameObject flagGameObject;

    public enum FlagStatus
    {
        UP,
        DOWN
    }
    
    /// <summary>
    /// Instantiates a Flag GameObject
    /// </summary>
    public Flag(int row, int col, FlagStatus fs, Board grid){
        this.row = row;
        this.col = col;
        this.flagStatus = fs;

        float tileSize = grid.tileSize;
        
        Vector3 position = new Vector3();
        switch(fs){
        case FlagStatus.UP:
            position = new Vector3(row* tileSize, tileSize/2 + tileSize, col*tileSize);
            break;
        case FlagStatus.DOWN:
            position = new Vector3(row* tileSize, -1*(tileSize/2 + tileSize), col*tileSize);
            break;
        }
        
        this.flagGameObject = GameObject.Instantiate(grid.flagPrefab, position, Quaternion.identity, grid.origin);
        
    }

    /// <summary>
    /// Attempt to collect the flag
    /// </summary>
    public bool CollectFlag()
    {
        if (collected)
        {
            return false;
        }
        collected = true;

        // Make the flag invisible, animation is welcome
        GameObject.Destroy(this.flagGameObject);

        return true;
    }
}