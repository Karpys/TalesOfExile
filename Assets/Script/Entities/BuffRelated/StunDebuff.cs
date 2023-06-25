using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class StunDebuff : Buff
    {
        protected override void Apply()
        {
            Debug.Log("Apply debuff");
            m_Receiver.EntityStats.RootLockCount += 1;
            m_Receiver.EntityStats.SpellLockCount += 1;
            m_Receiver.EntityStats.MeleeLockCount += 1;
        }

        protected override void UnApply()
        {
            m_Receiver.EntityStats.RootLockCount -= 1;
            m_Receiver.EntityStats.SpellLockCount -= 1;
            m_Receiver.EntityStats.MeleeLockCount -= 1;
        }
    }
}