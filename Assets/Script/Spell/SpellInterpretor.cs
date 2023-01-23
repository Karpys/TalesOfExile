using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterpretor:MonoBehaviour
{
    [SerializeField] private SpellDataScriptable m_SpellData = null;
    
    private SpellTarget m_SpellTarget = new SpellTarget();
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            m_SpellTarget.m_TargetZones = ComputeSpellZone();
        }
        
        if (m_SpellTarget.m_TargetZones.Count != 0)
        {
            Vector2Int origin = Vector2Int.zero;
            if (m_SpellTarget.m_Origin == SpellOrigin.Hero)
            {
                origin = MapData.Instance.GetPlayerPosition();
            }
            else
            {
                //Mouse Position Clamp with max range//
            }
            HighlightTilesManager.Instance.GenerateHighlightTiles(m_SpellTarget.m_TargetZones,origin);
        }
    }

    private List<Vector2Int> ComputeSpellZone()
    {
        SpellDataScriptable spellZone = m_SpellData;
        List<Vector2Int> zones = new List<Vector2Int>();

        switch (spellZone.DisplayType)
        {
            case SpellDisplayType.Square:
                //Square Display
                for (int x = -spellZone.Range + 1; x < spellZone.Range; x++)
                {
                    for (int y = -spellZone.Range + 1; y < spellZone.Range ; y++)
                    {
                        zones.Add(new Vector2Int(x,y));
                    }
                }
                break;
            case SpellDisplayType.Circle:
                //Circle Display
                int range = spellZone.Range;
                Vector2Int middleZone = Vector2Int.zero;

                for (int x = -spellZone.Range + 1; x < spellZone.Range; x++)
                {
                    for (int y = -spellZone.Range + 1; y < spellZone.Range; y++)
                    {
                        Vector2Int vec = new Vector2Int(x, y);
                        if (Vector2Int.Distance(middleZone,vec) <= range - 0.66f)
                        {
                            zones.Add(vec);
                        }
                    }
                }
                break;
            default:
                break;
        }

        m_SpellTarget.m_Origin = spellZone.Origin;
        return zones;
    }
}

public class SpellTarget
{
    public List<Vector2Int> m_TargetZones = new List<Vector2Int>();
    public SpellOrigin m_Origin = SpellOrigin.Hero;
}