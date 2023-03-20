using UnityEngine;

public abstract class Buff : MonoBehaviour
{
    [SerializeField] protected BuffGroup m_BuffGroup = BuffGroup.Neutral;
    [SerializeField] protected BuffCooldown m_BuffCooldown = BuffCooldown.Cooldown;

    protected BoardEntity m_AttachedEntity = null;
    protected int m_Cooldown = 0;
    protected float m_BuffValue = 0;

    private bool m_IgnoreCoodldownOnInit = false;

    public virtual void InitializeBuff(BoardEntity entity, int cooldown, float buffValue, object[] args = null)
    {
        if (entity.EntityGroup == EntityGroup.Enemy)
            m_IgnoreCoodldownOnInit = true;
        
        m_AttachedEntity = entity;
        m_Cooldown = cooldown;
        m_BuffValue = buffValue;
        m_AttachedEntity.Buffs.AddBuff(this);
        Apply();
    }
    protected abstract void Apply();
    protected abstract void UnApply();

    public void ReduceCooldown()
    {
        if (m_IgnoreCoodldownOnInit)
            m_IgnoreCoodldownOnInit = false;
        
        if (m_BuffCooldown == BuffCooldown.Cooldown)
        {
            m_Cooldown -= 1;

            if (m_Cooldown <= 0)
            {
                m_AttachedEntity.Buffs.RemoveBuff(this);
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
}