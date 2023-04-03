using UnityEngine;

public class ChestLootable : StaticLootable
{
    [SerializeField] private int m_DrawCount = 10;
    [SerializeField] private InventoryPoolObject m_ChestObjectPool = null;
    private WorldTile m_WorldTile = null;

    private void Start()
    {
        m_WorldTile = GetComponent<WorldTile>();
        ComputeLoot();
    }

    protected override void ComputeLoot()
    {
        base.ComputeLoot();
        m_LootObjects = LootLibrary.Instance.ItemRequest(m_ChestObjectPool,m_DrawCount);
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