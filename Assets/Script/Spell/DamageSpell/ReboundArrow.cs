using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell.SpellFx;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class ReboundArrow : ProjectileAutoTrigger
    {
        private Zone m_ZoneStrike = null;
        private int m_Rebound = 0;

        public ReboundArrow(DamageSpellScriptable damageSpellData,OriginType originType, float baseWeaponDamageConvertion, ZoneType zoneType,
            int zoneRange,int rebound) : base(damageSpellData, originType,baseWeaponDamageConvertion)
        {
            m_ZoneStrike = new Zone(zoneType, zoneRange);
            m_Rebound = rebound;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, 
            CastInfo castInfo)
        {
            List<BoardEntity> targetEntity = GameManager.Instance.GetEntityViaGroup(m_AttachedSpell.AttachedEntity.TargetEntityGroup);
            targetEntity.Remove(entity);
        
            List<BoardEntity> entityStriked = new List<BoardEntity>(){entity};
        
            entityStriked.AddRange(DistanceUtils.GetZoneContactEntity(m_ZoneStrike, targetEntity, entity.EntityPosition, m_Rebound));

            foreach (BoardEntity striked in entityStriked)
            {
                base.EntityHit(striked, spellData, origin, castInfo);
            }

            if (OnHitAnimation)
            {
                SpellAnimation spellAnim = OnHitAnimation.TriggerFx(m_OriginPosition);
                if (spellAnim is ISplitter splitter)
                {
                    splitter.SplitTargets = entityStriked.Select(e => e.WorldPosition).ToArray();
                }
                else
                {
                    Debug.LogError("Consider using a splitter animation");
                }
            }
        }

        protected override SpellAnimation CreateOnHitFx(Vector3 entityPosition, Transform transform)
        {
            return null;
        }
    
    
    }
}