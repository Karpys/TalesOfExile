using UnityEngine;

public class BuffLibrary : SingletonMonoBehavior<BuffLibrary>
{
    public BuffKey[] Library = null;
    
    public Buff GetBuffViaKey(BuffType type,BoardEntity entity)
    {
        foreach (BuffKey buffKey in Library)
        {
            if (buffKey.Type == type)
            {
                Buff buff = Instantiate(buffKey.Buff,entity.transform);
                return buff;
            }
        }

        Debug.LogError("No Buff Type Found " + type);
        return null;
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
}
