using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class ProjectileDamageTrigger : DamageSpellTrigger
    {
        protected Vector3 m_OriginPosition = Vector3.zero; 
        public ProjectileDamageTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData){}

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            m_OriginPosition = MapData.Instance.GetTilePosition(spellTiles.CenterOrigin);
            base.Trigger(spellData, spellTiles, castInfo,efficiency);
        }

        protected override void TriggerOnHitFx(Vector3 entityPosition, Transform transform, params object[] args)
        {
            base.TriggerOnHitFx(m_OriginPosition, transform,m_OriginPosition,entityPosition);
        }

        protected override void TriggerTileHitFx(Vector3 tilePosition, Transform transform, params object[] args)
        {
            base.TriggerTileHitFx(m_OriginPosition, transform, m_OriginPosition,tilePosition);
        }
    }
}