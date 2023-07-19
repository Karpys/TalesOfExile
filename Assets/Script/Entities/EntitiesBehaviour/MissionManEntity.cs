using KarpysDev.Script.UI;
using UnityEngine;

namespace KarpysDev.Script.Entities.EntitiesBehaviour
{
    public class MissionManEntity : PnjEntity
    {
        private Canvas_MissionSelection m_MissionSelectionCanvas = null;

        protected override void InitializeEntityBehaviour()
        {
            base.InitializeEntityBehaviour();
            m_MissionSelectionCanvas = Canvas_MissionSelection.Instance;
        }

        protected override void OnPlayerExit()
        {
            m_MissionSelectionCanvas.Close();
        }

        protected override void OnPlayerAroundEntity()
        {
            if(!m_MissionSelectionCanvas.IsOpen)
                m_MissionSelectionCanvas.Open();
        }
    }
}