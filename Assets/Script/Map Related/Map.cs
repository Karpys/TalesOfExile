using System.Collections.Generic;

public class Map
{
    public int Height = 0;
    public int Width = 0;
    public Tile[][] Tiles;
    
    //Entities related//
    public List<BoardEntity> EntitiesOnBoard = new List<BoardEntity>();
}