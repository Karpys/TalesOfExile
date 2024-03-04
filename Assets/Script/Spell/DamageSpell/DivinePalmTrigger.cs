using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class DivinePalmTrigger : DamageSpellTrigger
    {
        private int m_RootDuration = 0;
        public DivinePalmTrigger(DamageSpellScriptable damageSpellData,int rootDuration) : base(damageSpellData)
        {
            m_RootDuration = rootDuration;
        }

        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, Vector2Int origin, CastInfo castInfo)
        {
            base.EntityHit(entity, spellData, origin, castInfo);
            Buff rootDebuff = BuffLibrary.Instance.GetBuffViaBuffType(BuffType.RootDebuff,
                m_AttachedSpell.AttachedEntity, entity, BuffGroup.Debuff, m_RootDuration, 1);
            entity.Buffs.AddBuff(rootDebuff);
        }

        protected override SpellAnimation CreateOriginFx(Vector3 originPosition, Transform transform)
        {
            SpellAnimation anim = base.CreateOriginFx(originPosition, transform);

            if (anim is ISizeable sizeable)
            {
                sizeable.SetSize(m_AttachedSpell.GetFirstActionSelectionSize() - 1);
            }

            return anim;
        }
    }
}