using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEntityIA:EntityBehaviour
{
    private int[] m_SpellIdPriority = null;
    private int m_TriggerSelfBuffCount = 0;

    protected BoardEntity m_Target = null;
    protected override void InitializeEntityBehaviour()
    {
        SelfBuffCount();
        ComputeSpellPriority();
        
        if(m_AttachedEntity.EntityGroup == EntityGroup.Enemy)
            GameManager.Instance.RegisterActiveEnemy(m_AttachedEntity);
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
        m_AttachedEntity.EntityEvent.OnBehave?.Invoke();
    }

    protected virtual void SetTarget()
    {
        List<BoardEntity> entities = m_AttachedEntity.EntityGroup == EntityGroup.Friendly ? GameManager.Instance.ActiveEnemiesOnBoard : GameManager.Instance.FriendlyOnBoard;

        if (entities.Count == 0)
        {
            m_Target = null;
            return;
        }

        m_Target = EntityHelper.GetClosestEntity(entities,m_AttachedEntity.EntityPosition);
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
            .Where(s => s.SpellTrigger.SpellPriority > 0)
            .OrderByDescending(s => s.SpellTrigger.SpellPriority)
            .ToList();

        if (buffs.Count == 0 || buffs[0].SpellTrigger.SpellPriority <= 0)
            return false;

        int buffId = 0;
        Vector2Int targetPosition = Vector2Int.zero;
        
        for (; buffId < buffs.Count; buffId++)
        {
            Zone allowedCastZone = buffs[buffId].TriggerData.AllowedCastZone;
            SpellCastUtils.GetSpellTargetOrigin(buffs[buffId],ref targetPosition);
            
            if (allowedCastZone.DisplayType != ZoneType.NONE)
            {
                if(!ZoneTileManager.IsInRange(buffs[buffId],m_Target.EntityPosition,allowedCastZone))
                    continue;
            }
            
            break;
        }

        if (buffId >= buffs.Count)
            return false;
        
        SpellCastUtils.CastSpellAt(buffs[buffId],targetPosition,m_AttachedEntity.EntityPosition);
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
        
        for (int i = 0; i < m_SpellIdPriority.Length; i++)
        {
            Vector2Int targetPosition = m_Target.EntityPosition;
            TriggerSpellData triggerSpellData = m_AttachedEntity.Spells[m_SpellIdPriority[i]] as TriggerSpellData;
            
            if (triggerSpellData.IsCooldownReady())
            {
                Zone allowedCastZone = triggerSpellData.TriggerData.AllowedCastZone;
                if (allowedCastZone.DisplayType != ZoneType.NONE)
                {
                    if(!ZoneTileManager.IsInRange(triggerSpellData,targetPosition,allowedCastZone))
                        continue;
                }
                
                SpellCastUtils.GetSpellTargetOrigin(triggerSpellData,ref targetPosition);
                
                if (ZoneTileManager.IsInRange(triggerSpellData,targetPosition,triggerSpellData.GetMainSelection().Zone) && SpellCastUtils.CanCastSpellAt(triggerSpellData, targetPosition))
                {
                    SpellCastUtils.CastSpellAt(triggerSpellData,targetPosition,m_AttachedEntity.EntityPosition);
                    return true;
                }
            }
        }

        return false;
    }
}