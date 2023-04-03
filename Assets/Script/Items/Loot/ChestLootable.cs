using UnityEngine;

public class ChestLootable : StaticLootable
{
    private WorldTile m_WorldTile = null;

    private void Start()
    {
        m_WorldTile = GetComponent<WorldTile>();
        ComputeLoot();
    }

    protected override void ComputeLoot()
    {
        base.ComputeLoot();
        m_LootObjects.Add(LootLibrary.Instance.GetDropTest());
    }

    protected override Vector2Int GetOriginPosition()
    {
        return m_WorldTile.Tile.TilePosition;
    }

    protected override Tile GetOriginTile()
    {
        return m_WorldTile.Tile;
    }

    public void OpenChest()
    {
        SpawnLoot();   
    }
}