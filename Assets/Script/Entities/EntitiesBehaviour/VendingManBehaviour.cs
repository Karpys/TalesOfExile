using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class VendingManBehaviour : PnjBehaviour
    {
        private VendingBoardEntity m_VendingEntity = null;
        protected override void InitializeEntityBehaviour()
        {
            base.InitializeEntityBehaviour();
            
            if (m_AttachedEntity is VendingBoardEntity vendingEntity)
            {
                m_VendingEntity = vendingEntity;
            }
            else
            {
                Debug.LogError("Vending Man failed cast");
            }
        }
        
        protected override void OnPlayerEnterEntity()
        {
            m_VendingEntity.Open();
        }
        
        protected override void OnPlayerExit()
        {
            m_VendingEntity.Close();
        }
    }
}