using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class StunDebuff : Buff
    {
        protected override void Apply()
        {
            m_Receiver.EntityStats.AddStunLock(1);
        }

        protected override void UnApply()
        {
            m_Receiver.EntityStats.AddStunLock(-1);
        }
    }
}