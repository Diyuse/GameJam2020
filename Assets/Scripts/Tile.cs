public class Tile
{
    public enum TileStatus
    {
        CHANGED,
        UNCHANGED,
        DELETED
    }

    private TileStatus currentStatus;

    /// <summary>
    /// Returns the current status of the tile, whether it is CHANGED, UNCHANGED or DELETED.
    /// </summary>
    public TileStatus CurrentStatus
    {
        get { return currentStatus; }
    }


    /// <summary>
    /// Default to UNCHANGED and NONE.
    /// </summary>
    public Tile(int row, int col)
    {
        currentStatus = TileStatus.UNCHANGED;
    }

    /// <summary>
    /// Set the status of the tile and flag on creation.
    /// </summary>
    /// <param name="status"></param>
    public Tile(TileStatus status)
    {
        currentStatus = status;
    }

    /// <summary>
    /// Change the status of a tile.
    /// </summary>
    /// <param name="status"></param>
    public void SetStatus(TileStatus status)
    {
        currentStatus = status;
    }

    
}
