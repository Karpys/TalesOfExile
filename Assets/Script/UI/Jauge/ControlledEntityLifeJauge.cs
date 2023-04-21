using TMPro;
using UnityEngine;

namespace Script.UI.Jauge
{
    public class ControlledEntityLifeJauge : BaseJauge
    {
        [SerializeField] private TMP_Text m_LifeText = null;
        private BoardEntity m_CurrentEntity = null;
        private void Awake()
        {
            GameManager.Instance.A_OnControlledEntityChange += SetLifeListenerToNewEntity;
        }

        private void OnDestroy()
        {
            if(m_CurrentEntity)
                m_CurrentEntity.Life.A_OnLifeUpdated -= UpdateJaugeFillValue;
        }

        private void SetLifeListenerToNewEntity(BoardEntity oldEntity,BoardEntity newEntity)
        {
            if(oldEntity)
                oldEntity.Life.A_OnLifeUpdated -= UpdateJaugeFillValue;
            
            m_CurrentEntity = newEntity;
            m_CurrentEntity.Life.A_OnLifeUpdated += UpdateJaugeFillValue;
            
            UpdateJaugeFillValue(m_CurrentEntity.Life.Life,m_CurrentEntity.Life.MaxLife);
        }

        protected override void UpdateJaugeFillValue(float currentValue, float maxValue)
        {
            base.UpdateJaugeFillValue(currentValue, maxValue);
            m_LifeText.text = currentValue + " / " + maxValue;
        }
    }
}