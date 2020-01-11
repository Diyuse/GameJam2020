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
    
    public Flag(int row, int col, FlagStatus fs){
        this.row = row;
        this.col = col;
        this.flagStatus = fs;
    }
}