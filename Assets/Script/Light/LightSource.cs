using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private ZoneSelection m_LightProjection = null;
    [SerializeField] private ZoneSelection m_CircleReduction = null;
    [SerializeField] private float m_LightForce = 1;

    private BoardEntity m_AttachedEntity = null;
    void Start()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
    }

    // Update is called once per frame
    public List<LightTile> ApplyLight()
    {
        List<LightTile> lightTile = new List<LightTile>();
        List<LightTile> forceShadowTile = new List<LightTile>();
        
        List<Vector2Int> lightable = ZoneTileManager.GetSelectionZone(m_LightProjection,
            m_AttachedEntity.EntityPosition, m_LightProjection.Range);

        Vector2Int lightOrigin = m_AttachedEntity.EntityPosition;
        Tile originTile = MapData.Instance.Map.Tiles[lightOrigin.x, lightOrigin.y];

        List<Tile> closeTile = TileHelper.GetNeighbours(originTile, NeighbourType.Square, MapData.Instance);
        originTile.WorldTile.LightTile.AddLight();
        lightTile.Add(originTile.WorldTile.LightTile);

        for (int i = closeTile.Count - 1; i > 0; i--)
        { 
            closeTile.RemoveAt(i);
        }

        foreach (Tile tile in closeTile)
        {
            lightTile.Add(tile.WorldTile.LightTile);
        }
        
        
        foreach (Vector2Int pos in lightable)
        {
            Vector2Int clampedPos = MapData.Instance.MapClampedPosition(pos);

            List<WorldTile> worldTiles = LinePath.GetPathTile(lightOrigin, clampedPos).Select(s => s.WorldTile).ToList();

            if(worldTiles.Count == 0)
                continue;
            
            //if (!worldTiles[0].Tile.Walkable)
               //worldTiles = PreciseLight(closeTile,originTile,lightOrigin,clampedPos);

            bool inShadow = false;
            foreach (WorldTile worldTile in worldTiles)
            {
                lightTile.Add(worldTile.LightTile);
                
                if (!inShadow)
                {
                    worldTile.LightTile.AddLight();
                }
                else
                {
                    worldTile.LightTile.AddShadow();
                    //forceShadowTile.Add(worldTile.LightTile);
                }

                if (!worldTile.Tile.Walkable)
                {
                    inShadow = true;
                }
            }
        }

        List<Vector2Int> circleSelection =
            ZoneTileManager.GetSelectionZone(m_CircleReduction, lightOrigin, m_CircleReduction.Range);

        List<LightTile> outerCircle = lightTile.Where(t => !circleSelection.Contains(t.Tile.TilePosition)).ToList();

        foreach (LightTile tile in outerCircle)
        {
            tile.ResetLight();
        }
        // foreach (LightTile tile in forceShadowTile)
        // {
        //     if (lightTile.Contains(tile))
        //     {
        //         lightTile.Remove(tile);
        //     }
        // }

        return lightTile;
    }

    private List<WorldTile> PreciseLight(List<Tile> closeTile,Tile originTile,Vector2Int origin,Vector2Int clampedPos)
    {
        //Apply Light Based on closest tiles//
        Tile clampedTile = MapData.Instance.GetTile(clampedPos);
        float closestDistance = Vector3.Distance(originTile.WorldTile.transform.position,clampedTile.WorldTile.transform.position);;
        foreach (Tile tile in closeTile)
        {
            if(!tile.Walkable)
                continue;
                
            float currentDist = Vector3.Distance(clampedTile.WorldTile.transform.position,
                tile.WorldTile.transform.position); 
            if (currentDist < closestDistance)
            {
                origin = tile.TilePosition;
                closestDistance = currentDist;
            }
        }

        LinePath.NeighbourType = NeighbourType.Cross;
        List<WorldTile> worldTiles = LinePath.GetPathTile(origin, clampedPos).Select(s => s.WorldTile).ToList();
        LinePath.NeighbourType = NeighbourType.Square;

        return worldTiles;
    }
}
