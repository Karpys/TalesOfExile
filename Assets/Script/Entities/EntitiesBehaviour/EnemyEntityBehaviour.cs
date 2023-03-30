using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyEntityBehaviour:EntityBehaviour
{
    private BoardEnemyEntity AttachedEnemy => m_AttachedEntity as BoardEnemyEntity;
    
    private int[] m_SpellIdPriority = null;
    private int m_TriggerSelfBuffCount = 0;
    
    private bool m_IsActive = false;

    private const int X_ACTIVE_TOLERANCE = 19;
    private const int Y_ACTIVE_TOLERANCE = 11;
    
    public void SelfBuffCount()
    {
        int count = m_AttachedEntity.Spells.Count(s => s.Data.SpellType == SpellType.Buff);
        m_TriggerSelfBuffCount = count;
    }
    
    private void CheckForActive()
    {
        Vector2Int entityPos = GameManager.Instance.ControlledEntity.EntityPosition;

        int XDiff = Mathf.Abs(entityPos.x - m_AttachedEntity.EntityPosition.x);
        int YDiff = Mathf.Abs(entityPos.y - m_AttachedEntity.EntityPosition.y);

        if (XDiff < X_ACTIVE_TOLERANCE && YDiff < Y_ACTIVE_TOLERANCE)
            m_IsActive = true;
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
        if (!m_IsActive)
        {
            CheckForActive();

            if (!m_IsActive)
                return;
        }
        
        bool triggerAction = false;

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
        
        m_AttachedEntity.ReduceAllCooldown();
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
        Vector2Int targetPosition = m_AttachedEntity.Map.GetControlledEntityPosition();
        m_AttachedEntity.CastSpellAt(buffs[0],targetPosition);
        return true;
    }

    private bool MovementAction()
    {
        List<Tile> path = PathFinding.FindTilePath(m_AttachedEntity.EntityPosition, m_AttachedEntity.Map.GetControlledEntityPosition(),false);
        if (path.Count > AttachedEnemy.Range)
        {
            //Move toward player//
            if (MapData.Instance.IsWalkable(path[0].TilePosition))
            {
                m_AttachedEntity.MoveTo(path[0].TilePosition);
            }
        }
        else if(path.Count < AttachedEnemy.Range)
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
        Vector2Int targetPosition = m_AttachedEntity.Map.GetControlledEntityPosition();
        
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

    public EnemyEntityBehaviour(BoardEntity entity) : base(entity) {}
}