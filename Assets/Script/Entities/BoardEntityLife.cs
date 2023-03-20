using System;
using UnityEngine;

public class BoardEntityLife : MonoBehaviour
{
    private float m_MaxLife = 100f;
    private float m_Life = 100f;
    private float m_LifeRegeneration = 0;
    private BoardEntity m_AttachedEntity = null;
    
    public Action<float,float> A_OnLifeUpdated;
    //Getter
    public float Life => m_Life;
    public float MaxLife => m_MaxLife;

    private void Start()
    {
        GameManager.Instance.A_OnEndTurn += ApplyRegeneration;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnEndTurn -= ApplyRegeneration;
    }

    public void Initalize(BoardEntity entity, float maxLife, float life, float lifeRegeneration)
    {
        m_AttachedEntity = entity;
        m_MaxLife = maxLife;
        m_Life = life;
        m_LifeRegeneration = lifeRegeneration;
    }
    
    public void ChangeLifeValue(float value)
    {
        m_Life += value;
        if (m_Life > m_MaxLife)
            m_Life = m_MaxLife;
        
        A_OnLifeUpdated?.Invoke(m_Life,m_MaxLife);
    }

    private void ApplyRegeneration()
    {
        if(m_LifeRegeneration == 0)
            return;
        
        ChangeLifeValue(m_LifeRegeneration);
    }

    public void AddRegeneration(float value)
    {
        m_LifeRegeneration += value;
    }
}
