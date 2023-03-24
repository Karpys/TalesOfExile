using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class BoardEnemyEntity : BoardEntity
{
    [SerializeField] private int m_Range = 1;

    private int[] m_SpellIdPriority = null;
    private bool m_IsActive = false;

    private const int X_ACTIVE_TOLERANCE = 19;
    private const int Y_ACTIVE_TOLERANCE = 11;

    private int m_TriggerSelfBuffCount = 0;
    protected override void Start()
    {
        base.Start();
        ComputeSpellPriority();
        m_TriggerSelfBuffCount = SelfBuffCount();
    }
    
    private int SelfBuffCount()
    {
        int count = Spells.Count(s => s.Data.SpellType == SpellType.Buff);
        return count;
    }
    
    private void ComputeSpellPriority()
    {
        //Source : See Chat Gpt Conv "ComputeSpellPriority"//
        //Linq : https://learn.microsoft.com/fr-fr/dotnet/api/system.linq.enumerable.select?view=net-7.0
        Dictionary<TriggerSpellData, int> spellIndexes = Spells
            .Select((spell, index) => new { Spell = spell, Index = index })
            .Where(s => s.Spell.Data.SpellType == SpellType.Trigger)
            .ToDictionary(s => (TriggerSpellData)s.Spell, s => s.Index);

        int[] sortedIndexes = Spells
            .Where(s => s.Data.SpellType == SpellType.Trigger)
            .Cast<TriggerSpellData>()
            .OrderByDescending(s => s.SpellTrigger.SpellPriority)
            .Select(s => spellIndexes[s])
            .ToArray();

        m_SpellIdPriority = sortedIndexes;
    }
    //ACTION PART
    public override void EntityAction()
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
        
        ReduceAllCooldown();
    }

    private bool CanCastSpell()
    {
        IntSocket blockSpellCount = new IntSocket(0);
        m_EntityEvent.OnRequestBlockSpell?.Invoke(blockSpellCount);
        return blockSpellCount.Value <= 0;
    }

    private bool SelfBuffAction()
    {
        List<TriggerSpellData> buffs = Spells
            .Where(s => s.Data.SpellType == SpellType.Buff)
            .Cast<TriggerSpellData>()
            .Where(s => s.IsCooldownReady())
            .OrderByDescending(s => s.SpellTrigger.SpellPriority)
            .ToList();

        if (buffs.Count == 0 || buffs[0].SpellTrigger.SpellPriority <= 0)
            return false;
        
        Debug.Log("TRIGGER REGENERATION");
        Vector2Int targetPosition = m_TargetMap.GetControlledEntityPosition();
        CastSpellAt(buffs[0],targetPosition);
        return true;
    }

    private bool MovementAction()
    {
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

        return true;
    }
    private bool TriggerAction()
    {
        Vector2Int targetPosition = m_TargetMap.GetControlledEntityPosition();
        
        for (int i = 0; i < m_SpellIdPriority.Length; i++)
        {
            TriggerSpellData triggerSpellData = Spells[m_SpellIdPriority[i]] as TriggerSpellData;
            
            if (triggerSpellData.IsCooldownReady() && ZoneTileManager.IsInRange(triggerSpellData,targetPosition))
            {
                if (SpellCastUtils.CanCastSpellAt(triggerSpellData, targetPosition))
                {
                    CastSpellAt(triggerSpellData,targetPosition);
                    return true;
                }
            }
        }

        return false;
    }

    private void CheckForActive()
    {
        Vector2Int entityPos = GameManager.Instance.ControlledEntity.EntityPosition;

        int XDiff = Mathf.Abs(entityPos.x - m_XPosition);
        int YDiff = Mathf.Abs(entityPos.y - m_YPosition);

        if (XDiff < X_ACTIVE_TOLERANCE && YDiff < Y_ACTIVE_TOLERANCE)
            m_IsActive = true;
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
    

    public override float GetMainWeaponDamage()
    {
        return 35;
    }
}