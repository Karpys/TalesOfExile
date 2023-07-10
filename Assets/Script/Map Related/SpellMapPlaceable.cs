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
        [SerializeField] private bool m_IsCast = false;

        private TriggerSpellData m_AttachedSpell = null;
    
        public void Initialize(BoardEntity entity,Vector2Int position,BehaveTiming behaveTiming,int behaveCount)
        {
            Place(position);
            m_AttachedSpell = entity.RegisterSpell(m_SpellInfo);

            behaveTiming = EntityHelper.GetBehaveTiming(entity,behaveTiming);
            m_BehaviourTrigger.InitBehaviourTrigger(this,behaveTiming,behaveCount);
        }

        protected virtual Vector2Int GetCastPosition()
        {
            return m_Position;
        }

        public void Behave()
        {
            if(m_IsCast)
            {
                SpellCastUtils.CastSpellAt(m_AttachedSpell,GetCastPosition(),m_Position,false);
            }
            else
            {
                SpellCastUtils.TriggerSpellAt(m_AttachedSpell,GetCastPosition(),m_Position);
            }
        }
    }
}