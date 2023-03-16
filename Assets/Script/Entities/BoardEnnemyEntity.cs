using System.Collections.Generic;
using System.Linq;
using TweenCustom;
using UnityEngine;

public class BoardEnnemyEntity : BoardEntity
{
    [SerializeField] private int m_Range = 1;

    private int[] m_SpellIdPriority = null;
    protected override void Start()
    {
        base.Start();
        ComputeSpellPriority();
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
    public override void EntityAction()
    {
        //Check if the path to the player is lower than the range//
        Vector2Int targetPosition = m_TargetMap.GetControlledEntityPosition();
        for (int i = 0; i < m_SpellIdPriority.Length; i++)
        {
            TriggerSpellData triggerSpellData = Spells[m_SpellIdPriority[i]] as TriggerSpellData;
            if (triggerSpellData.IsCooldownReady() && ZoneTileManager.IsInRange(triggerSpellData,targetPosition))
            {
                if (SpellCastUtils.CanCastSpellAt(triggerSpellData, targetPosition))
                {
                    CastSpellAt(triggerSpellData,targetPosition);
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
        //Movement Action//
        
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