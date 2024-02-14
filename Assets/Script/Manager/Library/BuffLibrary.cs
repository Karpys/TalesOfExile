using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;
    using Spell;

    public class BuffLibrary : SingletonMonoBehavior<BuffLibrary>
    {
        [SerializeField] private SpellInfo m_RockThrowSpellInfo = null;
        [SerializeField] private SpellInfo m_OnKillFlameSpellInfo = null;
        [SerializeField] private SpellInfo m_HolyAttackSpellInfo = null;

        [SerializeField] private GenericLibrary<BuffType, BuffInfo> m_BuffInfoLibrary = null;
        public SpellInfo RockThrowSpellInfo => m_RockThrowSpellInfo;
        public SpellInfo OnKillFlameSpellInfo => m_OnKillFlameSpellInfo;
        public SpellInfo HolyAttackSpellInfo => m_HolyAttackSpellInfo;

        private void Awake()
        {
            m_BuffInfoLibrary.InitializeDictionary();
        }

        public Buff GetBuffViaBuffType(BuffType type,BoardEntity caster,BoardEntity receiver,int duration,float value)
        {
            switch (type)
            {
                case BuffType.RegenerationBuff:
                    return new RegenerationBuff(caster, receiver, BuffType.RegenerationBuff, duration, value);
                case BuffType.RockThrowBuff:
                    return new RockThrowBuff(caster, receiver, BuffType.RockThrowBuff, duration, value,m_RockThrowSpellInfo);
                case BuffType.FireHandBuff:
                    return new FireHandBuff(caster, receiver, BuffType.FireHandBuff, duration, value,
                        SubDamageType.Fire);
                case BuffType.OnKillFlameBurst:
                    return new OnKillTriggerSpell(caster, receiver, BuffType.RegenerationBuff, duration, value,m_OnKillFlameSpellInfo);
                case BuffType.IcePrisonBuff:
                    return new IcePrisonBuff(caster, receiver, BuffType.RegenerationBuff, duration, value);
                case BuffType.HolyAttack:
                    return new HolyAttackBuff(caster, receiver, BuffType.RegenerationBuff, duration, value,m_HolyAttackSpellInfo);
                case BuffType.SilenceDebuff:
                    return new SilenceBuff(caster, receiver, BuffType.SilenceDebuff, duration, value);
                case BuffType.BurnDotDebuff:
                    return new DotDebuff(caster, receiver, BuffType.SilenceDebuff, duration, value,SubDamageType.Fire);
                case BuffType.StunDebuff:
                    return new StunDebuff(caster, receiver, BuffType.SilenceDebuff, duration, value);
                default:
                    Debug.LogError("Return default buff");
                    return new RegenerationBuff(caster, receiver, BuffType.RegenerationBuff, 0, 0);
            }
        }

        public BuffInfo GetBuffInfoViaType(BuffType buffType)
        {
            BuffInfo buffInfo = m_BuffInfoLibrary.GetViaKey(buffType);

            if (buffInfo is )
            {
                buffType.LogError("Buff info has not been implemented");
            }

            return buffInfo;
        }
    }

    public enum PassiveBuffType
    {
        None = -1,
        //Buff type// 0 => 100//
        RockThrowPassive = 2,
        //Debuff// 101 => 200//
        //Mark / Misc// 201 +//
    }
}