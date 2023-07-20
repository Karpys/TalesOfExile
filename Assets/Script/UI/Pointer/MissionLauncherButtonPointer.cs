using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.UI.Pointer
{
    public class MissionLauncherButtonPointer : UIButtonPointer
    {
        [SerializeField] private QuestDisplayer m_Displayer = null;
        public override void Trigger()
        {
            MissionSelectionManager.Instance.Close();
            MissionSelectionManager.Instance.ClearExistentPortal();
            MapDataLibrary.Instance.AddMissionLauncher(MissionSelectionManager.Instance.GetSpawnPosition,m_Displayer.Quest.QuestPortalIcon,
                m_Displayer.Quest);
        }
    }
}