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

    private Visibility m_visibility = null;
    private List<LightTile> m_LightTiles = new List<LightTile>();
    void Start()
    {
        m_AttachedEntity = GetComponent<BoardEntity>();
        ComputeLineTrace();
        m_visibility = new MyVisibility(BlockLight, SetVisible, GetDistance);
    }

    private bool BlockLight(int x,int y)
    {
        Tile tile = MapData.Instance.GetTile(new Vector2Int(x, y));
        if (tile == null)
        {
            return true;
        }
        else
        {
            return !tile.Walkable;
        }
    }
    
    private void SetVisible(int x,int y)
    {
        Tile tile = MapData.Instance.GetTile(new Vector2Int(x, y));
        
        if(tile != null)
            m_LightTiles.Add(tile.WorldTile.LightTile);
    }

    private int GetDistance(int x,int y)
    {
        return (int) Vector3.Distance(new Vector3(x, y), new Vector3(0, 0));
    }
    
    private void ComputeLineTrace()
    {
        m_LineTraces.Clear();
        
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(0, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(1, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(2, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(3, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(4, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(5, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(-1, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(-2, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(-3, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(-4, 10)));
        m_LineTraces.Add(Bresenhams.Bresenhams.GetPath(new Vector2Int(0, 0), new Vector2Int(-5, 10)));
        /*m_OuterSelection = ZoneTileManager.GetSelectionZone(m_LightProjection, Vector2Int.zero, m_LightProjection.Range);

        foreach (Vector2Int select in m_OuterSelection)
        {
            LinePath.NeighbourType = NeighbourType.Cross;
            m_LineTraces.Add(LinePath.GetPathTile(Vector2Int.zero, select));
            LinePath.NeighbourType = NeighbourType.Square;
        }*/
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

    public List<LightTile> ApplyLightV4()
    {
        List<LightTile> lightTiles = new List<LightTile>();
        Vector2Int lightOrigin = m_AttachedEntity.EntityPosition;
        
        //Left Right Octan//
        foreach (List<Vector2Int> lineTrace in m_LineTraces)
        {
            bool inShadow = false;

            int xDiff = Mathf.Abs(lineTrace[lineTrace.Count - 1].x);
            int allowedStep = xDiff;
            int allowedStepCooldown = xDiff > 0 ? 10/(xDiff) : 0;
            int lastXPos = lightOrigin.x;
            int forcedStepCount = 0;
            int allowedStepCdTimer = 0;

            foreach (Vector2Int lightCheckPosition in lineTrace)
            {
                //Check For Normal AllowedStep
                Vector2Int lightCheckClamped = new Vector2Int(Math.Min(lightOrigin.x + lightCheckPosition.x + forcedStepCount, xDiff + lightOrigin.x), lightCheckPosition.y + lightOrigin.y);
                
                if (lastXPos != lightCheckClamped.x)
                {
                    allowedStep -= 1;
                    lastXPos = lightCheckClamped.x;
                    allowedStepCdTimer = allowedStepCooldown; 
                }
                
                Tile tile = MapData.Instance.GetTile(lightCheckClamped);

                if (tile == null)
                    break;
                
                if (!tile.Walkable)
                {
                    if (allowedStep > 0 && allowedStepCdTimer <= 0)
                    {
                        //ForceStep//
                        allowedStep -= 1;
                        forcedStepCount += 1;
                        
                        Tile tileForce = MapData.Instance.GetTile(lightCheckClamped + new Vector2Int(1,0));

                        if (!tileForce.Walkable)
                        {
                            inShadow = true;
                        }
                        else
                        {
                            if (inShadow)
                            {
                                tileForce.WorldTile.LightTile.AddShadow();
                            }
                            else
                            {
                                tileForce.WorldTile.LightTile.AddLight();
                            }
                            lightTiles.Add(tileForce.WorldTile.LightTile);
                        }
                    }
                    else
                    {
                        inShadow = true;
                    }
                }

                allowedStepCdTimer -= 1;
                lightTiles.Add(tile.WorldTile.LightTile);

                if (inShadow)
                {
                    tile.WorldTile.LightTile.AddShadow();
                }
                else
                {
                    tile.WorldTile.LightTile.AddLight();
                }
            }
        }

        lightTiles = lightTiles.Distinct().ToList();
        for (int i = 0; i < lightTiles.Count; i++)
        {
            LightTile lightTile = lightTiles[i];
            if (lightTile.IsShadow)
            {
                lightTile.ResetLight();
                lightTiles.Remove(lightTile);
                i--;
            }
        }


        return lightTiles;
    }
    public List<LightTile> ApplyLightV5()
    {
        m_LightTiles.Clear();
        
        m_visibility.Compute(m_AttachedEntity.EntityPosition,10);

        return m_LightTiles;
    }
}
