using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    public class BuffLibrary : SingletonMonoBehavior<BuffLibrary>
    {
        [SerializeField] private GenericLibrary<Buff, BuffType> Library = null;

        private void Awake()
        {
            Library.InitializeDictionary();
        }

        public Buff AddBuffToViaKey(BuffType type,BoardEntity entity)
        {
            Buff buff = Library.GetViaKey(type);

            if (buff != null)
            {
                return Instantiate(buff, entity.transform);
            }
            else
            {
                Debug.LogError("No Buff Type Found " + type);
                return null;
            }
        }
    }

    [Serializable]
    public class BuffKey
    {
        public Buff Buff = null;
        public BuffType Type = BuffType.None;
    }
    public enum BuffType
    {
        None = -1,
        //Buff type// 0 => 100//
        ModifierBuff = 0,
        RegenerationBuff = 1,
        RockThrowBuff = 2,
        FireHandBuff = 3,
        OnKillFlameBurst = 4,
        IcePrisonBuff = 5,
        HolyAttack = 6,
        //Debuff// 101 => 200//
        SilenceDebuff = 101,
        BurnDotDebuff = 102,
        StunDebuff = 103,
        //Mark / Misc// 201 +//
        SkeletonCurse = 201,
    }
}