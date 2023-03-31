using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEntityIA:EntityBehaviour
{
    private int[] m_SpellIdPriority = null;
    private int m_TriggerSelfBuffCount = 0;

    protected BoardEntity m_Target = null;
    public BaseEntityIA(BoardEntity entity) : base(entity)
    {
        SelfBuffCount();
        ComputeSpellPriority();
    }
    public void SelfBuffCount()
    {
        int count = m_AttachedEntity.Spells.Count(s => s.Data.SpellType == SpellType.Buff);
        m_TriggerSelfBuffCount = count;
    }
    
    
    public void ComputeSpellPriority()
    {
        //Source : See Chat Gpt Conv "ComputeSpellPriority"//
        //Linq : https://learn.microsoft.com/fr-fr/dotnet/api/system.linq.enumerable.select?view=net-7.0
        Dictionary<TriggerSpellData, int> spellIndexes = m_AttachedEntity.Spells
            .Select((spell, index) => new { Spell = spell, Index = index })
            .Where(s => s.Spell.Data.SpellType == SpellType.Trigger)
            .ToDictionary(s => (TriggerSpellData)s.Spell, s => s.Index);

        int[] sortedIndexes = m_AttachedEntity.Spells
            .Where(s => s.Data.SpellType == SpellType.Trigger)
            .Cast<TriggerSpellData>()
            .OrderByDescending(s => s.SpellTrigger.SpellPriority)
            .Select(s => spellIndexes[s])
            .ToArray();

        m_SpellIdPriority = sortedIndexes;
    }

    public override void Behave()
    {
        SetTarget();
        EntityAction();
        m_AttachedEntity.ReduceAllCooldown();
    }

    protected virtual void SetTarget()
    {
        List<BoardEntity> entities = m_AttachedEntity.EntityGroup == EntityGroup.Friendly ? GameManager.Instance.ActiveEnemiesOnBoard : GameManager.Instance.FriendlyOnBoard;

        if (entities.Count == 0)
        {
            m_Target = null;
            return;
        }

        m_Target = entities.OrderBy(e => DistanceUtils.GetSquareDistance(m_AttachedEntity.EntityPosition, e.EntityPosition)).First();
    }

    protected virtual void EntityAction()
    {
        bool triggerAction = false;

        if (m_Target != null)
        {
            if (CanCastSpell())
            {
                
                if (!triggerAction && m_TriggerSelfBuffCount != 0)
                {
                    triggerAction = SelfBuffAction();
                }

                if (!triggerAction)
                {
                    triggerAction = TriggerAction();
                }
            }
        
            if (!triggerAction)
            {
                triggerAction = MovementAction();
            }
        }
    }

    private bool CanCastSpell()
    {
        IntSocket blockSpellCount = new IntSocket(0);
        m_AttachedEntity.EntityEvent.OnRequestBlockSpell?.Invoke(blockSpellCount);
        return blockSpellCount.Value <= 0;
    }

    private bool SelfBuffAction()
    {
        List<TriggerSpellData> buffs = m_AttachedEntity.Spells
            .Where(s => s.Data.SpellType == SpellType.Buff)
            .Cast<TriggerSpellData>()
            .Where(s => s.IsCooldownReady())
            .OrderByDescending(s => s.SpellTrigger.SpellPriority)
            .ToList();

        if (buffs.Count == 0 || buffs[0].SpellTrigger.SpellPriority <= 0)
            return false;
        
        Debug.Log("TRIGGER REGENERATION");
        Vector2Int targetPosition = m_Target.EntityPosition;
        m_AttachedEntity.CastSpellAt(buffs[0],targetPosition);
        return true;
    }

    protected virtual bool MovementAction()
    {
        List<Tile> path = PathFinding.FindTilePath(m_AttachedEntity.EntityPosition, m_Target.EntityPosition,false);
        if (path.Count > m_AttachedEntity.EntityStats.CombatRange)
        {
            //Move toward player//
            if (MapData.Instance.IsWalkable(path[0].TilePosition))
            {
                m_AttachedEntity.MoveTo(path[0].TilePosition);
            }
        }
        else if(path.Count < m_AttachedEntity.EntityStats.CombatRange)
        {
            //Run Away from player if too close//
            Vector2Int targetPos = TileHelper.GetOppositePosition(m_AttachedEntity.EntityPosition, path[0].TilePosition);
            if (MapData.Instance.IsWalkable(targetPos))
                m_AttachedEntity.MoveTo(targetPos);
        }
        
        return true;
    }
    private bool TriggerAction()
    {
        Vector2Int targetPosition = m_Target.EntityPosition;
        
        for (int i = 0; i < m_SpellIdPriority.Length; i++)
        {
            TriggerSpellData triggerSpellData = m_AttachedEntity.Spells[m_SpellIdPriority[i]] as TriggerSpellData;
            
            if (triggerSpellData.IsCooldownReady() && ZoneTileManager.IsInRange(triggerSpellData,targetPosition))
            {
                if (SpellCastUtils.CanCastSpellAt(triggerSpellData, targetPosition))
                {
                    m_AttachedEntity.CastSpellAt(triggerSpellData,targetPosition);
                    return true;
                }
            }
        }

        return false;
    }
}