using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Buff : MonoBehaviour
{
    [SerializeField] protected Transform m_VisualTransform = null;
    [SerializeField] protected BuffType m_BuffType = BuffType.None;
    [SerializeField] protected BuffGroup m_BuffGroup = BuffGroup.Neutral;
    [SerializeField] protected BuffCooldown m_BuffCooldown = BuffCooldown.Cooldown;

    [Header("Buff Visual Info")] 
    [SerializeField]protected BuffInfo m_BuffInfo;
    [Header("Enemy Specific")]
    [SerializeField] protected bool m_EnemyBuffIgnoreFirstCooldown = false;

    protected BoardEntity m_Caster = null;
    protected BoardEntity m_Receiver = null;
    protected int m_Cooldown = 0;
    protected float m_BuffValue = 0;

    private bool m_IgnoreCooldownOnInit = false;
    private string m_BuffKey = String.Empty;

    public BuffCooldown BuffCooldown => m_BuffCooldown;
    public BuffType BuffType => m_BuffType;
    public float BuffValue => m_BuffValue;
    public int Cooldown => m_Cooldown;
    public string BuffKey => m_BuffKey;
    public BuffInfo BuffInfo => m_BuffInfo;

    public virtual void InitializeBuff(BoardEntity caster,BoardEntity receiver, int cooldown, float buffValue, object[] args = null)
    {
        if (receiver.EntityGroup == EntityGroup.Enemy && m_EnemyBuffIgnoreFirstCooldown)
            m_IgnoreCooldownOnInit = true;
        
        m_Receiver = receiver;
        m_Caster = caster;
        m_Cooldown = cooldown;
        m_BuffValue = buffValue;
        m_Receiver.Buffs.AddBuff(this);
        Apply();
    }
    protected abstract void Apply();
    protected abstract void UnApply();

    public void ReduceCooldown()
    {
        if (m_IgnoreCooldownOnInit)
        {
            m_IgnoreCooldownOnInit = false;
            return;
        }
        
        if (m_BuffCooldown == BuffCooldown.Cooldown)
        {
            m_Cooldown -= 1;

            if (m_Cooldown <= 0)
            {
                m_Receiver.Buffs.RemoveBuff(this);
                UnApply();
                Destroy(gameObject);
            }
        }
    }

    public void SetBuffType(BuffGroup group,BuffCooldown cooldown)
    {
        m_BuffGroup = group;
        m_BuffCooldown = cooldown;
    }

    public void SetBuffKey(string buffKey)
    {
        m_BuffKey = buffKey;
    }

    public void EnableVisual(bool enable)
    {
        if(m_VisualTransform)
            m_VisualTransform.gameObject.SetActive(enable); 
    }

    public void RemovePassive()
    {
        m_Receiver.Buffs.RemoveBuff(this);
        UnApply();
        Destroy(gameObject);
    }
}

[System.Serializable]
public struct BuffInfo
{
    public Sprite BuffVisual;
}