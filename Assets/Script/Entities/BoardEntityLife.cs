using System;
using TweenCustom;
using UnityEngine;
using UnityEngine.UI;

public class BoardEntityLife : MonoBehaviour
{
    [SerializeField] private SpriteRenderer m_LifeFill = null;
    [SerializeField] private Transform m_LifeDamageEffect = null; 
    
    private float m_MaxLife = 100f;
    private float m_Life = 100f;
    private float m_LifeRegeneration = 0;
    
    public Action<float,float> A_OnLifeUpdated;
    //Getter
    public float Life => m_Life;
    public float MaxLife => m_MaxLife;

    private void Start()
    {
        GameManager.Instance.A_OnPreEndTurn += ApplyRegeneration;
    }

    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnPreEndTurn -= ApplyRegeneration;
    }

    public void Initialize(float maxLife, float life, float lifeRegeneration)
    {
        m_MaxLife = maxLife;
        m_Life = life;
        m_LifeRegeneration = lifeRegeneration;

        UpdateLifeFill();
    }
    
    public void ChangeLifeValue(float value)
    {
        m_Life += value;
        if (m_Life > m_MaxLife)
            m_Life = m_MaxLife;
        
        A_OnLifeUpdated?.Invoke(m_Life,m_MaxLife);
        UpdateLifeFill();
    }

    public void ChangeMaxLifeValue(float value)
    {
        m_MaxLife += value;
        
        A_OnLifeUpdated?.Invoke(m_Life,m_MaxLife);
        UpdateLifeFill();
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

    private void UpdateLifeFill()
    {
        float ratio = m_Life / m_MaxLife;
        m_LifeFill.color = ColorHelper.GetLifeLerp(ratio);

        Vector3 targetScale = new Vector3(1, ratio, 1);
        m_LifeFill.transform.localScale = targetScale;


        m_LifeDamageEffect.DoKill();
        m_LifeDamageEffect.DoScale(targetScale, 0.25f).SetDelay(.5f).SetEase(Ease.EASE_OUT_SIN);
    }
}
