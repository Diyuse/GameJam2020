public class Tile
{
    public enum TileStatus
    {
        CHANGED,
        UNCHANGED,
        DELETED
    }

    public enum FlagStatus
    {
        NONE,
        UP,
        DOWN
    }

    private TileStatus currentStatus;
    private FlagStatus currentFlagStatus;

    /// <summary>
    /// Returns the current status of the tile, whether it is CHANGED, UNCHANGED or DELETED.
    /// </summary>
    public TileStatus CurrentStatus
    {
        get { return currentStatus; }
    }

    /// <summary>
    /// Returns the current status of the flag on the tile, whether it is NONE, UP or DOWN.
    /// </summary>
    public FlagStatus CurrentFlagStatus
    {
        get { return currentFlagStatus; }
    }

    /// <summary>
    /// Default to UNCHANGED and NONE.
    /// </summary>
    public Tile()
    {
        currentStatus = TileStatus.UNCHANGED;
        this.currentFlagStatus = FlagStatus.NONE;
    }

    /// <summary>
    /// Set the status of the tile and flag on creation.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="currentFlagStatus"></param>
    public Tile(TileStatus status, FlagStatus currentFlagStatus)
    {
        currentStatus = status;
        this.currentFlagStatus = currentFlagStatus;
    }

    /// <summary>
    /// Change the status of a tile.
    /// </summary>
    /// <param name="status"></param>
    public void SetStatus(TileStatus status)
    {
        currentStatus = status;
    }

    /// <summary>
    /// Change the status of a the flag.
    /// </summary>
    /// <param name="status"></param>
    public void SetFlagStatus(FlagStatus status)
    {
        currentFlagStatus = status;
    }
}
