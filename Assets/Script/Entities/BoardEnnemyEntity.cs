using System.Collections.Generic;
using TweenCustom;
using UnityEngine;

public class BoardEnnemyEntity : BoardEntity
{
    [SerializeField] private int m_Range = 1;

    private int[] m_SpellPriorityCompute = null;


    private void ComputeSpellPriority()
    {
       // m_SpellPriorityCompute =
    }
    public override void EntityAction()
    {
        TriggerSpellData triggerSpellData = m_EntityData.m_SpellList.m_Spells[0] as TriggerSpellData;

        //Check if the path to the player is lower than the range//
        if (ZoneTileManager.IsInRange(triggerSpellData, m_TargetMap.GetControlledEntityPosition()))
        {
            CastSpellAt(triggerSpellData,m_TargetMap.GetControlledEntityPosition());
        }
        //Movement Action//
        return;
        
        List<Tile> path = PathFinding.FindTilePath(EntityPosition, m_TargetMap.GetControlledEntityPosition(),false);
        if (path.Count > m_Range)
        {
            //Move toward player//
            if (MapData.Instance.IsWalkable(path[0].TilePosition))
            {
                MoveTo(path[0].TilePosition);
            }
        }
        else if(path.Count < m_Range)
        {
            //Run Away from player if too close//
            Vector2Int targetPos = TileHelper.GetOppositePosition(EntityPosition, path[0].TilePosition);
            if (MapData.Instance.IsWalkable(targetPos))
                MoveTo(targetPos);
        }
    }

    protected override void Movement()
    {
        transform.DoKill();
        transform.DoMove( m_TargetMap.GetTilePosition(m_XPosition, m_YPosition),0.1f);
    }
    
    //Damage Related

    protected override void TriggerDeath()
    {
        GameManager.Instance.UnRegisterEntity(this);
        RemoveFromBoard();
        Destroy(gameObject);
    }
}