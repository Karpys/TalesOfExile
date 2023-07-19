using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.UI.Pointer
{
    public class MissionLauncherButtonPointer : UIButtonPointer
    {
        [SerializeField] private QuestDisplayer m_Displayer = null;
        public override void Trigger()
        {
            //Todo : Spawn Portal at position and assign mapGroup
            Canvas_MissionSelection.Instance.CloseAll();
            MapGenerator.Instance.SetMapGroup(m_Displayer.Quest.MapGroup);            
            MapGenerator.Instance.LaunchMap();            
        }
    }
}