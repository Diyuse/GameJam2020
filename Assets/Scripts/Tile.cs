public class Tile
{
    public enum TileStatus
    {
        CHANGED,
        UNCHANGED,
        DELETED
    }

    private TileStatus currentStatus;

    public TileStatus CurrentStatus
    {
        get { return currentStatus; }
    }

    public Tile(TileStatus status)
    {
        currentStatus = status;
    }

    public void SetStatus(TileStatus status)
    {
        currentStatus = status;
    }
}
