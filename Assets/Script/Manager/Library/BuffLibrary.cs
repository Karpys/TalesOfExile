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
        [SerializeField] private BuffInfo m_DefaultBuffInfo = null;
        [SerializeField] private GenericLibrary<BuffType, BuffInfo> m_BuffInfoLibrary = null;
        public SpellInfo RockThrowSpellInfo => m_RockThrowSpellInfo;
        public SpellInfo OnKillFlameSpellInfo => m_OnKillFlameSpellInfo;
        public SpellInfo HolyAttackSpellInfo => m_HolyAttackSpellInfo;

        private void Awake()
        {
            m_BuffInfoLibrary.InitializeDictionary();
        }

        public Buff GetBuffViaBuffType(BuffType type,BoardEntity caster,BoardEntity receiver,BuffGroup buffGroup,int duration,float value)
        {
            switch (type)
            {
                case BuffType.RegenerationBuff:
                    return new RegenerationBuff(caster, receiver, BuffType.RegenerationBuff, buffGroup,duration, value);
                case BuffType.RockThrowBuff:
                    return new RockThrowBuff(caster, receiver, BuffType.RockThrowBuff,buffGroup, duration, value,m_RockThrowSpellInfo);
                case BuffType.FireHandBuff:
                    return new FireHandBuff(caster, receiver, BuffType.FireHandBuff, buffGroup,duration, value,
                        SubDamageType.Fire);
                case BuffType.OnKillFlameBurst:
                    return new OnKillTriggerSpell(caster, receiver, BuffType.RegenerationBuff, buffGroup,duration, value,m_OnKillFlameSpellInfo);
                case BuffType.IcePrisonBuff:
                    return new IcePrisonBuff(caster, receiver, BuffType.RegenerationBuff,buffGroup, duration, value);
                case BuffType.HolyAttack:
                    return new HolyAttackBuff(caster, receiver, BuffType.RegenerationBuff, buffGroup,duration, value,m_HolyAttackSpellInfo);
                case BuffType.SilenceDebuff:
                    return new SilenceBuff(caster, receiver, BuffType.SilenceDebuff, buffGroup,duration, value);
                case BuffType.BurnDotDebuff:
                    return new DotDebuff(caster, receiver, BuffType.SilenceDebuff, buffGroup,duration, value,SubDamageType.Fire);
                case BuffType.StunDebuff:
                    return new StunDebuff(caster, receiver, BuffType.SilenceDebuff, buffGroup,duration, value);
                case BuffType.RootDebuff:
                    return new RootDebuff(caster, receiver, BuffType.RootDebuff, buffGroup, duration, value);
                default:
                    Debug.LogError("Return default buff");
                    return new RegenerationBuff(caster, receiver, BuffType.RegenerationBuff,buffGroup, 0, 0);
            }
        }

        public BuffInfo GetBuffInfoViaType(BuffType buffType)
        {
            BuffInfo info = m_BuffInfoLibrary.GetViaKey(buffType);
            return info ?? m_DefaultBuffInfo;
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