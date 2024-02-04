using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell.SpellFx;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class IceShardSplitTrigger : ProjectileDamageTrigger
    {
        private Zone m_ZoneStrike = null;
        private int m_SplitCount = 0;
        public IceShardSplitTrigger(DamageSpellScriptable damageSpellData,OriginType originType,ZoneType zoneType,int range,int splitCount) : base(damageSpellData,originType)
        {
            m_ZoneStrike = new Zone(zoneType, range);
            m_SplitCount = splitCount;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            List<BoardEntity> targetEntity = GameManager.Instance.GetEntityViaGroup(m_AttachedSpell.AttachedEntity.TargetEntityGroup);
            targetEntity.Remove(entity);
        
            List<BoardEntity> entityStriked = new List<BoardEntity>(){entity};
        
            entityStriked.AddRange(DistanceUtils.GetClosestEntityAround(m_ZoneStrike, targetEntity, entity.EntityPosition, m_SplitCount));

            foreach (BoardEntity striked in entityStriked)
            {
                base.EntityHit(striked, spellData, origin, castInfo);
            }

            if (OnHitAnimation)
            {
                SpellAnimation spellAnimation = OnHitAnimation.TriggerFx(m_AttachedSpell.AttachedEntity.WorldPosition);

                if (spellAnimation is ISplitter splitter)
                {
                    splitter.SplitTargets = entityStriked.Select(e => e.WorldPosition).ToArray();
                }
            }
        }

        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            return null;
        }
    }
}