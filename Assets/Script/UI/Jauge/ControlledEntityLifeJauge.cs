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
            m_CurrentEntity.A_OnLifeUpdated -= UpdateJaugeFillValue;
        }

        private void SetLifeListenerToNewEntity(BoardEntity oldEntity,BoardEntity newEntity)
        {
            if(oldEntity)
                oldEntity.A_OnLifeUpdated -= UpdateJaugeFillValue;
            
            newEntity.A_OnLifeUpdated += UpdateJaugeFillValue;
            m_CurrentEntity = newEntity;
            
            EntityStats stats = m_CurrentEntity.m_EntityData.m_Stats;
            UpdateJaugeFillValue(stats.Life,stats.MaxLife);
        }

        protected override void UpdateJaugeFillValue(float currentValue, float maxValue)
        {
            base.UpdateJaugeFillValue(currentValue, maxValue);
            m_LifeText.text = currentValue + " / " + maxValue;
        }
    }
}