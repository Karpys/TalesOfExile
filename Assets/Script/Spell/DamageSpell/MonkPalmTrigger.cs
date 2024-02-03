using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    using KarpysUtils.TweenCustom;

    public class MonkPalmTrigger : WeaponDamageTrigger
    {
        private int m_KnockDistance = 0;
        private int m_StunDuration = 0;

        public MonkPalmTrigger(DamageSpellScriptable damageSpellData, float baseWeaponDamageConvertion,int knockDistance,int stunDuration) : base(damageSpellData, baseWeaponDamageConvertion)
        {
            m_KnockDistance = knockDistance;
            m_StunDuration = stunDuration;
        }
        
        private List<BoardEntity> m_EntityHits = new List<BoardEntity>();
        
        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            m_EntityHits.Clear();
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            if (m_EntityHits.Contains(entity))
            {
                return;
            }
        
            m_EntityHits.Add(entity);
            
            for (int i = 0; i < m_KnockDistance; i++)
            {
                Vector2Int opposite = TileHelper.GetOppositePositionFrom(entity.EntityPosition, spellData.AttachedEntity.EntityPosition);
            
            
                if (MapData.Instance.IsWalkable(opposite))
                {
                    entity.MoveTo(opposite,false);
                    m_SpellAnimDelay = 0.1f;
                }
                else
                {
                    entity.GiveBuff(BuffType.StunDebuff, m_StunDuration, 1, m_AttachedSpell.AttachedEntity);
                    base.EntityHit(entity, spellData,origin,castInfo);
                    
                    BoardEntity collideEntity = MapData.Instance.GetEntityAt(opposite, spellData.AttachedEntity.TargetEntityGroup);
                    if (collideEntity)
                    {
                        collideEntity.GiveBuff(BuffType.StunDebuff, m_StunDuration, 1, m_AttachedSpell.AttachedEntity);
                        base.EntityHit(collideEntity,spellData,origin,castInfo);    
                    }
                    
                    break;
                }
            }
            
            UpdateEntityPosition(entity);
        }
        
        private void UpdateEntityPosition(BoardEntity entity)
        {
            entity.transform.DoMove(MapData.Instance.GetTilePosition(entity.EntityPosition), 0.1f);
        }

        public override string[] GetDescriptionParts()
        {
            string[] description = new string[2];
            
            if (m_DamageSources.TryGetValue(m_DamageSpellParams.InitialSourceDamage.DamageType,
                    out DamageSource initialDamageSource))
            {
                description[0] = initialDamageSource.ToDescription();
            }

            description[1] = m_KnockDistance.ToString();
            return description;
        }
    }
}