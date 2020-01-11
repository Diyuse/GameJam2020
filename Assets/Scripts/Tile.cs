public class Tile
{
    public enum TileStatus
    {
        CHANGED,
        UNCHANGED
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

    public void setStatus(TileStatus status)
    {
        currentStatus = status;
    }
}
