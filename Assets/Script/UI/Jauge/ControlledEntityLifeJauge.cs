namespace KarpysDev.Script.UI.Jauge
{
    using UnityEngine.UI;
    using Entities;
    using Manager;
    using TMPro;
    using UnityEngine;

    public class ControlledEntityLifeJauge : MonoBehaviour,ILifeDisplayer
    {
        [SerializeField] private Image m_LifeJauge = null;
        [SerializeField] private TMP_Text m_LifeText = null;
        [SerializeField] private Image m_ShieldJauge = null;
        [SerializeField] private Transform m_ShieldContainer = null;
        [SerializeField] private TMP_Text m_ShieldText = null;
        
        private BoardEntityLife m_EntityLife = null;
        public BoardEntityLife EntityLife => m_EntityLife;

        private void Awake()
        {
            GameManager.Instance.A_OnControlledEntityChange += SetLifeListenerToNewEntity;
        }

        private void UpdateFill(float currentLife, float maxLife,Image targetJauge)
        {
            targetJauge.fillAmount = currentLife / maxLife;
        }
        
        private void SetLifeListenerToNewEntity(BoardEntity oldEntity,BoardEntity newEntity)
        {
            if (oldEntity)
            {
                oldEntity.Life.DefaultDisplay();
            }
            
            m_EntityLife = newEntity.Life;
            newEntity.Life.SetLifeDisplayer(this);
            UpdateLifeDisplay();
        }

        private void UpdateLifeFill(float currentLife, float maxLife)
        {
            UpdateFill(currentLife,maxLife,m_LifeJauge);
            m_LifeText.text = currentLife.ToString("0") + " / " + maxLife.ToString("0");
        }
        
        private void UpdateShieldFill(float currentShield, float maxShield)
        {
            UpdateFill(currentShield,maxShield,m_ShieldJauge);
            m_ShieldText.text = currentShield.ToString("0") + " / " + maxShield.ToString("0");
        }

        public void EnableDisplay()
        {
            return;
        }

        public void DisableDisplay()
        {
            return;
        }

        public void UpdateLifeDisplay()
        {
            UpdateLifeFill(m_EntityLife.Life,m_EntityLife.MaxLife);
        }

        public void UpdateShieldDisplay()
        {
            UpdateShieldFill(m_EntityLife.CurrentShield,m_EntityLife.MaxShield);
        }

        public void EnableShieldDisplay()
        {
            m_ShieldContainer.gameObject.SetActive(true);
        }

        public void HideShieldDisplay()
        {
            m_ShieldContainer.gameObject.SetActive(false);
        }
    }
}