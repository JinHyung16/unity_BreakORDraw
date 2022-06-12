[System.Serializable]
public class Map
{
    public int Stage;
    public int Brick;
    public int Block;
    public int Flag;

    public Map(Map m)
    {
        Stage = m.Stage;
        Brick = m.Brick;
        Block = m.Block;
        Flag = m.Flag;
    }
}
