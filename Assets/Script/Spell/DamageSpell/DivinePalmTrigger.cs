namespace KarpysDev.Script.Spell.DamageSpell
{
    using Entities;
    using Entities.BuffRelated;
    using KarpysUtils;
    using Manager.Library;
    using Map_Related;
    using SpellConfig;
    using SpellFx;
    using UnityEngine;

    public class DivinePalmTrigger : DamageSpellTrigger,IActivator,IConfig
    {
        private int m_RootDuration = 0;
        private bool m_ActiveMovement = false;

        public bool MoveAtOrigin => m_ActiveMovement && m_ShouldMoveAtOrigin;
        public bool CanDisplayConfig => m_ActiveMovement;
        private bool m_ShouldMoveAtOrigin = false;
        
        public DivinePalmTrigger(DamageSpellScriptable damageSpellData,int rootDuration) : base(damageSpellData)
        {
            m_RootDuration = rootDuration;
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo, float efficiency = 1)
        {
            if (MoveAtOrigin && MapData.Instance.IsWalkable(spellTiles.FirstOrigin))
            {
                spellData.AttachedEntity.MoveTo(spellTiles.FirstOrigin);
            }
            
            base.Trigger(spellData, spellTiles, castInfo, efficiency);
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

        public void Enable()
        {
            m_ActiveMovement = true;
        }

        public void Disable()
        {
            m_ActiveMovement = false;
        }
        
        public void DisplayConfig(ConfigMonitor monitor)
        {
            monitor.DisplayBool(value => m_ShouldMoveAtOrigin = value,m_ShouldMoveAtOrigin,"Always move at origin");
        }
    }
}