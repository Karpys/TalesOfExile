using System;
using UnityEngine;

public class BuffLibrary : SingletonMonoBehavior<BuffLibrary>
{
    [SerializeField] private GenericObjectLibrary<Buff, BuffType> Library = null;

    private void Awake()
    {
        Library.InitializeDictionary();
    }

    public Buff GetBuffViaKey(BuffType type,BoardEntity entity)
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

[System.Serializable]
public class BuffKey
{
    public Buff Buff = null;
    public BuffType Type = BuffType.None;
}
public enum BuffType
{
    None,
    ModifierBuff,
    RegenerationBuff,
    SilenceDebuff,
    RockThrowBuff,
}
