using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    using Object = UnityEngine.Object;

    public class BoardEntityLife : MonoBehaviour
    {
        [SerializeField] private Object m_LifeDisplayerReference = null;

        private ILifeDisplayer m_LifeDisplayer = null;
        private BoardEntity m_Entity = null;
        private float m_MaxLife = 100f;
        private float m_Life = 100f;
        private float m_LifeRegeneration = 0;
        
        private float m_MaxShield = 0f;
        private float m_CurrentShield = 0f;
    
        public ILifeDisplayer LifeDisplayer => m_LifeDisplayer;
        public float Life => m_Life;
        public float MaxLife => m_MaxLife;
        public float MaxShield => m_MaxShield;
        public float CurrentShield => m_CurrentShield;

        private void Awake()
        {
            DefaultDisplay();
        }

        public void DefaultDisplay()
        {
            m_LifeDisplayer = m_LifeDisplayerReference as ILifeDisplayer;
        }

        private void Start()
        {
            GameManager.Instance.A_OnPreEndTurn += ApplyRegeneration;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnPreEndTurn -= ApplyRegeneration;
        }

        public void SetLifeDisplayer(ILifeDisplayer lifeDisplayer)
        {
            m_LifeDisplayer.DisableDisplay();
            m_LifeDisplayer = lifeDisplayer;
            m_LifeDisplayer.EnableDisplay();
        }

        public void Initialize(float maxLife, float life, float lifeRegeneration,BoardEntity entity)
        {
            m_MaxLife = maxLife;
            m_Life = life;
            m_LifeRegeneration = lifeRegeneration;
            m_Entity = entity;
            
            m_LifeDisplayer.UpdateLifeDisplay();
        }

        public void TakeDamage(float value)
        {
            float tempValue = value;
            
            if (m_CurrentShield > 0)
            {
                value -= Mathf.Min(m_CurrentShield, value);
                m_CurrentShield -= tempValue;
                m_LifeDisplayer.UpdateShieldDisplay();
                
                if (m_CurrentShield <= 0)
                {
                    m_MaxShield = 0;
                    m_LifeDisplayer.HideShieldDisplay();
                }
            }
            
            ChangeLifeValue(-value);
        }
        public void ChangeLifeValue(float value)
        {
            m_Life += value;
            if (m_Life > m_MaxLife)
                m_Life = m_MaxLife;
        
            m_LifeDisplayer.UpdateLifeDisplay();
            
            
            if (m_Life <= 0)
                m_Entity.TriggerDeath();
        }
        
        public void ChangeMaxLifeValue(float value)
        {
            m_MaxLife += value;
            m_LifeDisplayer.UpdateLifeDisplay();
        }
        
        public void SetToMaxLife()
        {
            m_Life = m_MaxLife;
            m_LifeDisplayer.UpdateLifeDisplay();
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

        public void AddShield(float value)
        {
            m_CurrentShield += value;
            m_MaxShield += value;
            m_LifeDisplayer.EnableShieldDisplay();
            m_LifeDisplayer.UpdateShieldDisplay();
        }
    }
}
