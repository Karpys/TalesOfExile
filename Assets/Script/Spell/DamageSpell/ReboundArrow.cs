using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class ReboundArrow : ProjectileAutoTrigger
    {
        private Zone m_ZoneStrike = null;
        private int m_Rebound = 0;

        public ReboundArrow(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion, ZoneType zoneType,
            int zoneRange,int rebound) : base(damageSpellData, baseWeaponDamageConvertion)
        {
            m_ZoneStrike = new Zone(zoneType, zoneRange);
            m_Rebound = rebound;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup, Vector2Int origin, 
            CastInfo castInfo)
        {
            List<BoardEntity> targetEntity = GameManager.Instance.GetEntityViaGroup(targetGroup);
            targetEntity.Remove(entity);
        
            List<BoardEntity> entityStriked = new List<BoardEntity>(){entity};
        
            entityStriked.AddRange(DistanceUtils.GetZoneContactEntity(m_ZoneStrike, targetEntity, entity.EntityPosition, m_Rebound));

            foreach (BoardEntity striked in entityStriked)
            {
                base.EntityHit(striked, spellData, targetGroup, origin, castInfo);
            }

            if (OnHitAnimation)
            {
                OnHitAnimation.TriggerFx(entity.WorldPosition, null, m_OriginPosition, entityStriked.Select(e => e.WorldPosition).ToList());
            }
        }

        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            return;
        }
    
    
    }
}