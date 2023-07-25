using System;
using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class BaseEntityIA:EntityBehaviour
    {
        private int[] m_SpellIdPriority = null;
        private int m_TriggerSelfBuffCount = 0;

        protected BoardEntity m_Target = null;

        private TriggerSpellData[] m_SelfBuffs = Array.Empty<TriggerSpellData>();
        protected override void InitializeEntityBehaviour()
        {
            ComputeSpellPriority();
        
            if(m_AttachedEntity.EntityGroup == EntityGroup.Enemy)
                GameManager.Instance.RegisterActiveEnemy(m_AttachedEntity);
        }
        private void SelfBuffCount()
        {
            int count = m_AttachedEntity.UsableSpells.Count(s => s.Data.SpellType == SpellType.Buff);
            m_TriggerSelfBuffCount = count;
            m_SelfBuffs = m_AttachedEntity.UsableSpells.Where(s => s.Data.SpellType == SpellType.Buff).ToArray();
        }
    
    
        public void ComputeSpellPriority()
        {
            //Source : See Chat Gpt Conv "ComputeSpellPriority"//
            //Linq : https://learn.microsoft.com/fr-fr/dotnet/api/system.linq.enumerable.select?view=net-7.0
            Dictionary<TriggerSpellData, int> spellIndexes = m_AttachedEntity.UsableSpells
                .Select((spell, index) => new { Spell = spell, Index = index })
                .Where(s => s.Spell.Data.SpellType == SpellType.Trigger)
                .ToDictionary(s => s.Spell, s => s.Index);

            int[] sortedIndexes = m_AttachedEntity.UsableSpells
                .Where(s => s.Data.SpellType == SpellType.Trigger)
                .OrderByDescending(s => s.SpellTrigger.SpellPriority)
                .Select(s => spellIndexes[s])
                .ToArray();

            m_SpellIdPriority = sortedIndexes;

            SelfBuffCount();
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
                if (!triggerAction && m_TriggerSelfBuffCount != 0)
                {
                    triggerAction = SelfBuffAction();
                }

                if (!triggerAction)
                {
                    triggerAction = TriggerAction();
                }
        
                if (!triggerAction && m_AttachedEntity.EntityStats.RootLockCount <= 0)
                {
                    triggerAction = MovementAction();
                }
            }
        }

        private bool SelfBuffAction()
        {
            if (m_SelfBuffs.Length == 0)
                return false;
                    
            TriggerSpellData[] buffs = m_SelfBuffs.Where(s => s.IsCooldownReady() && s.SpellTrigger.SpellPriority > 0)
                .OrderByDescending(s => s.SpellTrigger.SpellPriority)
                .ToArray();

            if (buffs.Length == 0 || buffs[0].SpellTrigger.SpellPriority <= 0)
                return false;

            int buffId = 0;
            Vector2Int targetPosition = Vector2Int.zero;
        
            for (; buffId < buffs.Length; buffId++)
            {
                Zone allowedCastZone = buffs[buffId].TriggerData.AllowedCastZone;
                SpellCastUtils.GetSpellTargetOrigin(buffs[buffId],ref targetPosition);
            
                if (allowedCastZone.DisplayType != ZoneType.NONE)
                {
                    if(!ZoneTileManager.IsInRange(m_AttachedEntity.EntityPosition,m_Target.EntityPosition,allowedCastZone))
                        continue;
                }
            
                break;
            }

            if (buffId >= buffs.Length)
                return false;
        
            SpellCastUtils.CastSpellAt(buffs[buffId],targetPosition,m_AttachedEntity.EntityPosition);
            return true;
        }

        protected virtual bool MovementAction()
        {
            Tile closestTile = PathFinding.PathFinding.FindClosestTile(m_AttachedEntity.EntityPosition, m_Target.EntityPosition,false);

            int squareRange = DistanceUtils.GetSquareDistance(m_AttachedEntity.EntityPosition, m_Target.EntityPosition);
            
            if (squareRange >= m_AttachedEntity.EntityStats.CombatRange)
            {
                //Move toward player//
                if (closestTile.Walkable)
                {
                    m_AttachedEntity.MoveTo(closestTile.TilePosition);
                }
            }
            else if(squareRange < m_AttachedEntity.EntityStats.CombatRange)
            {
                Vector2Int targetPos = Vector2Int.zero;
                //Run Away from player if too close//
                if (m_AttachedEntity.EntityPosition == closestTile.TilePosition)
                {
                    targetPos = TileHelper.GetOppositePosition(m_AttachedEntity.EntityPosition, m_Target.EntityPosition);
                }
                else
                {
                    targetPos = TileHelper.GetOppositePosition(m_AttachedEntity.EntityPosition, closestTile.TilePosition);
                }
                
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
                TriggerSpellData triggerSpellData = m_AttachedEntity.UsableSpells[m_SpellIdPriority[i]];
            
                if (triggerSpellData.IsCooldownReady())
                {
                    Zone allowedCastZone = triggerSpellData.TriggerData.AllowedCastZone;
                    if (allowedCastZone.DisplayType != ZoneType.NONE)
                    {
                        if(!ZoneTileManager.IsInRange(m_AttachedEntity.EntityPosition,targetPosition,allowedCastZone))
                            continue;
                    }
                
                    SpellCastUtils.GetSpellTargetOrigin(triggerSpellData,ref targetPosition);
                
                    if (ZoneTileManager.IsInRange(m_AttachedEntity.EntityPosition,targetPosition,triggerSpellData.GetMainSelection().Zone) && SpellCastUtils.CanCastSpellAt(triggerSpellData, targetPosition))
                    {
                        SpellCastUtils.CastSpellAt(triggerSpellData,targetPosition,m_AttachedEntity.EntityPosition);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}