using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField] private ZoneSelection m_LightProjection = null;
    [SerializeField] private ZoneSelection m_CircleReduction = null;
    [SerializeField] private float m_LightForce = 1;
    [SerializeField] private bool m_UseDistinct = false;
    private BoardEntity m_AttachedEntity = null;

    private List<Vector2Int> m_OuterSelection = new List<Vector2Int>();
    private List<List<Vector2Int>> m_LineTraces = new List<List<Vector2Int>>();
    void Start()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
        ComputeLineTrace();

    }
    
    private void ComputeLineTrace()
    {
        m_LineTraces.Clear();
        m_OuterSelection = ZoneTileManager.GetSelectionZone(m_LightProjection, Vector2Int.zero, m_LightProjection.Range);

        foreach (Vector2Int select in m_OuterSelection)
        {
            LinePath.NeighbourType = NeighbourType.Cross;
            m_LineTraces.Add(LinePath.GetPathTile(Vector2Int.zero, select));
            LinePath.NeighbourType = NeighbourType.Square;
        }
    }

    // Update is called once per frame
    public List<LightTile> ApplyLightV3()
    {
        List<LightTile> lightTiles = new List<LightTile>();
        List<LightTile> shadowTiles = new List<LightTile>();
        Vector2Int lightOrigin = m_AttachedEntity.EntityPosition;

        foreach (List<Vector2Int> lineTrace in m_LineTraces)
        {
            bool inShadow = false;
            foreach (Vector2Int pos in lineTrace)
            {
                Tile targetTile = MapData.Instance.GetTile(pos + lightOrigin);

                if (targetTile != null)
                {
                    lightTiles.Add(targetTile.WorldTile.LightTile);

                    if (!inShadow)
                    {
                        targetTile.WorldTile.LightTile.AddLight();
                        if (!targetTile.Walkable)
                        {
                            inShadow = true;
                        }
                    }
                    else
                    {
                        targetTile.WorldTile.LightTile.AddShadow();
                    }
                }
                else
                {
                    break;
                }
            }
        }

        lightTiles = lightTiles.Distinct().ToList();

        shadowTiles = lightTiles.Where(t => t.IsShadow || m_OuterSelection.Contains(t.Tile.TilePosition - lightOrigin)).ToList();
        
        foreach (LightTile tile in shadowTiles)
        {
            if (lightTiles.Contains(tile))
                lightTiles.Remove(tile);
            tile.OnShadow();
        }

        return lightTiles;
    }
}
