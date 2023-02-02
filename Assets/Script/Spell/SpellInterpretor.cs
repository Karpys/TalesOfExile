using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterpretor:MonoBehaviour
{
    // [SerializeField] private SpellDataScriptable m_SpellData = null;
    //
    // private SpellTarget m_SpellTarget = new SpellTarget();
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.H))
    //     {
    //         m_SpellTarget.m_TargetZones = ComputeSpellZone();
    //     }
    //     
    //     if (m_SpellTarget.m_TargetZones.Count != 0)
    //     {
    //         Vector2Int origin = Vector2Int.zero;
    //         if (m_SpellTarget.m_Origin == SpellOrigin.Hero)
    //         {
    //             origin = MapData.Instance.GetPlayerPosition();
    //         }
    //         else
    //         {
    //             //Mouse Position Clamp with max range//
    //         }
    //         HighlightTilesManager.Instance.GenerateHighlightTiles(m_SpellTarget.m_TargetZones,origin);
    //     }
    // }
}

// public class SpellTarget
// {
//     public List<Vector2Int> m_TargetZones = new List<Vector2Int>();
//     public SpellOrigin m_Origin = SpellOrigin.Hero;
// }