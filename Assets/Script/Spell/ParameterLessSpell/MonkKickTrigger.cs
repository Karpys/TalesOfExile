using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.DamageSpell;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell.ParameterLessSpell
{
    public class MonkKickTrigger : RushTrigger
    {
        private int m_StunDuration = 0;
        public MonkKickTrigger(DamageSpellScriptable damageSpellData,int stunDuration) : base(damageSpellData)
        {
            m_StunDuration = stunDuration;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, origin, castInfo);

            Vector2Int oppositionPosition = TileHelper.GetOppositePosition(entity.EntityPosition, m_AttachedSpell.AttachedEntity.EntityPosition);

            if (!MapData.Instance.IsWalkable(oppositionPosition))
            {
                entity.Buffs.AddBuff(new StunDebuff(m_AttachedSpell.AttachedEntity, entity, BuffType.StunDebuff,
                    BuffGroup.Debuff, m_StunDuration, 1), VisualEffectType.StunStars);
            }
        }
    }
}