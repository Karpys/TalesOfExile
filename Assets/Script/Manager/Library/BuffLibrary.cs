using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    // public class BuffLibrary : SingletonMonoBehavior<BuffLibrary>
    // {
    //     [SerializeField] private Widget.GenericLibrary<Buff, BuffType> Library = null;
    //
    //     private void Awake()
    //     {
    //         Library.InitializeDictionary();
    //     }
    //
    //     public Buff AddBuffToViaKey(BuffType type,BoardEntity entity)
    //     {
    //         Buff buff = Library.GetViaKey(type);
    //
    //         if (buff != null)
    //         {
    //             return Instantiate(buff, entity.transform);
    //         }
    //         else
    //         {
    //             Debug.LogError("No Buff Type Found " + type);
    //             return null;
    //         }
    //     }
    // }

    public enum PassiveBuffType
    {
        None = -1,
        //Buff type// 0 => 100//
        RockThrowPassive = 2,
        //Debuff// 101 => 200//
        //Mark / Misc// 201 +//
    }
}