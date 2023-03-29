using System.Collections.Generic;
using UnityEngine;

public class BlightTile : WorldTile
{
    [SerializeField] private ZoneSelection m_OuterZoneSelection = null;
    [SerializeField] private TileSet m_BranchTileSet = null;
    [SerializeField] private int m_BranchCount = 1;
    [SerializeField] private WorldTile m_BlightSpawner = null;

    private WorldTile[] m_Spawners = null;

    public void Initalize(Map map)
    {
        List<Vector2Int> m_outerSelection = ZoneTileManager.GetSelectionZone(m_OuterZoneSelection, m_AttachedTile.TilePosition, m_OuterZoneSelection.Range);
        List<WorldTile> branchTiles = new List<WorldTile>();
        List<SpriteRenderer> branchRenderers = new List<SpriteRenderer>();
        m_Spawners = new WorldTile[m_BranchCount];

        for (int i = 0; i < m_BranchCount; i++)
        {
            int randomOuterSelection = Random.Range(0, m_outerSelection.Count);
            Vector2Int outerPosition = MapData.Instance.MapClampedPosition(m_outerSelection[randomOuterSelection]);

            LinePath.NeighbourType = NeighbourType.Cross;
            List<WorldTile> branchPath = new List<WorldTile>();
            branchPath.Add(this);
            branchPath.AddRange(LinePath.GetPathTile(Tile.TilePosition,outerPosition).ToTile().ToWorldTile());
            LinePath.NeighbourType = NeighbourType.Square;

            foreach (WorldTile branchTile in branchPath)
            {
                branchRenderers.Add(map.InsertVisualTile(m_BranchTileSet.TilePrefab,branchTile).Renderer);
            }
            
            InsertBranchExtremity(branchPath,i,map);
            
            m_outerSelection.Remove(outerPosition);
            branchTiles.AddRange(branchPath);
        }
        
        TileHelper.GenerateTileSet(branchTiles,branchRenderers,m_BranchTileSet.TileMap,MapData.Instance);
    }

    private void InsertBranchExtremity(List<WorldTile> branchPath,int id,Map map)
    {
        WorldTile lastWorldTile = branchPath[branchPath.Count - 1];
        WorldTile blightSpawner = map.PlaceTileAt(m_BlightSpawner, lastWorldTile.Tile.XPos, lastWorldTile.Tile.YPos);
        branchPath[branchPath.Count - 1] = blightSpawner;
    }
}