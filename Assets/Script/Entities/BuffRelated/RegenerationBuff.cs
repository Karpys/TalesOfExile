using KarpysDev.Script.Manager;

namespace KarpysDev.Script.Entities.BuffRelated
{
    using Manager.Library;

    public class RegenerationBuff : Buff
    {
        public RegenerationBuff(BoardEntity caster, BoardEntity receiver,BuffType buffType, int cooldown, float buffValue) : base(caster, receiver, buffType,cooldown, buffValue)
        {
        }

        public override void Apply()
        {
            GameManager.Instance.A_OnPreEndTurn += ApplyRegeneration;
            m_Receiver.EntityEvent.OnDeath += UnSub;
            //m_Receiver.Life.AddRegeneration(m_BuffValue);
        }

        private void ApplyRegeneration()
        {
            DamageManager.HealTarget(m_Receiver, m_BuffValue,true);
        }
        
        //Note: Need to sub to the onDeath event cause the if the entity die whith the buff => will not UnApply the buff and cause exeception error// 
        protected override void UnApply()
        {
            UnSub();
            m_Receiver.EntityEvent.OnDeath -= UnSub;
            //m_Receiver.Life.AddRegeneration(-m_BuffValue);
        }
        
        private void UnSub()
        {
            GameManager.Instance.A_OnPreEndTurn -= ApplyRegeneration;
        }
    }
}