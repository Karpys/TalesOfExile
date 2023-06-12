using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Utils;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class ModifierBuff : Buff
    {
        private Modifier m_Modifier = null;
        public override void InitializeAsBuff(BoardEntity caster,BoardEntity receiver, int cooldown, float buffValue, object[] args)
        {
            m_Modifier = args[0] as Modifier;
            base.InitializeAsBuff(caster,receiver, cooldown, buffValue, args);
        }

        protected override void Apply()
        {
            ModifierUtils.ApplyModifier(m_Modifier,m_Receiver);
            m_Receiver.ComputeAllSpells();
        }

        protected override void UnApply()
        {
            ModifierUtils.UnapplyModifier(m_Modifier,m_Receiver);
            m_Receiver.ComputeAllSpells();
        }
    }
}