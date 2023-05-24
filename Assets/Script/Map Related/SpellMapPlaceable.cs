using KarpysDev.Script.Entities;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class SpellMapPlaceable : MapPlaceable,IBehave
    {
        [SerializeField] private BehaviourTrigger m_BehaviourTrigger = null;
        [SerializeField] private SpellInfo m_SpellInfo = null;

        private TriggerSpellData m_AttachedSpell = null;
    
        public void Initialize(BoardEntity entity,Vector2Int position,BehaveTiming behaveTiming,int behaveCount)
        {
            Place(position);
            m_AttachedSpell = entity.RegisterSpell(m_SpellInfo) as TriggerSpellData;

            behaveTiming = EntityHelper.GetBehaveTiming(entity,behaveTiming);
            m_BehaviourTrigger.InitBehaviourTrigger(this,behaveTiming,behaveCount);
        }

        public void Behave()
        {
            SpellCastUtils.TriggerSpellAt(m_AttachedSpell,m_Position,m_Position);
        }
    }
}