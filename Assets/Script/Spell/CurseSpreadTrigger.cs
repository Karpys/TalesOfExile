using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Utils;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class CurseSpreadTrigger : SelectionSpellTrigger
    {
        private Zone m_ContactZone = null;
        private int m_MaxSpreadUnit = 0;

        public CurseSpreadTrigger(BaseSpellTriggerScriptable baseScriptable,ZoneType zoneType,int range,int maxSpreadUnit) : base(baseScriptable)
        {
            m_ContactZone = new Zone(zoneType, range);
            m_MaxSpreadUnit = maxSpreadUnit;
        }

        private List<BuffState> m_Curses = null;
        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int spellOrigin, CastInfo castInfo)
        {
            List<BoardEntity> targetEntities = GameManager.Instance.GetEntityViaGroup(m_AttachedSpell.AttachedEntity.TargetEntityGroup);
            targetEntities.Remove(entity);
            
            List<BoardEntity> contactEntity = DistanceUtils.GetClosestEntityAround(m_ContactZone, targetEntities,
                entity.EntityPosition, m_MaxSpreadUnit);

            //Todo: GetCurses List<Buff> curses
            m_Curses = entity.Buffs.GetCurseStates();

            foreach (BoardEntity entityHit in contactEntity)
            {
                foreach (BuffState buffState in m_Curses)
                {
                    //Todo:Copy buff//
                    // entityHit.Buffs.AddBuff(buffState.Buff.Copy());
                }
                
                base.EntityHit(entityHit,spellData,spellOrigin,castInfo);
            }
            
            base.EntityHit(entity, spellData, spellOrigin, castInfo);
        }
    }
}