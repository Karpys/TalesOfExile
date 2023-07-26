using KarpysDev.Script.UI;
using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class MissionManBehaviour : PnjBehaviour
    {
        private MissionSelectionManager m_MissionSelectionManager = null;

        protected override void InitializeEntityBehaviour()
        {
            base.InitializeEntityBehaviour();
            m_MissionSelectionManager = MissionSelectionManager.Instance;
        }

        protected override void OnPlayerExit()
        {
            m_MissionSelectionManager.Close();
        }

        protected override void OnPlayerEnterEntity()
        {
            m_MissionSelectionManager.Open(m_AttachedEntity.EntityPosition);
        }
    }
}