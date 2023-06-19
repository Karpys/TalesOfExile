using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Spell.DamageSpell
{
    public class MineSpellTrigger : DamageSpellTrigger
    {
        private bool m_HasTrigger = false;
        private List<SpellAnimationActivator> m_SpellAnimations = new List<SpellAnimationActivator>(); 

        public MineSpellTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
        {}

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo, float efficiency = 1)
        {
            base.Trigger(spellData, spellTiles,castInfo,efficiency);

            if (m_HasTrigger)
            {
                MineExplosion();
                spellData.AttachedEntity.ForceDeath();
                return;
            }
        
            m_HasTrigger = true;
        }
    
        protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            if(m_HasTrigger)
                return;
            base.TileHit(tilePosition, spellData);
        }

        protected override void TriggerTileHitFx(Vector3 tilePosition, Transform transform, params object[] args)
        {
            m_SpellAnimations.Add(TileHitAnimation.TriggerFx(tilePosition,m_AttachedSpell.AttachedEntity.transform) as SpellAnimationActivator);
        }
    
        protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int origin, CastInfo castInfo)
        {
            if(m_HasTrigger)
                base.EntityHit(entity, spellData, origin,castInfo);
        }
    
        private void MineExplosion()
        {
            foreach (SpellAnimationActivator activator in m_SpellAnimations)
            {
                activator.Activate();
            }
        }
    }
}