using UnityEngine;

namespace Script.UI
{
    public class Canvas_Skills : SingletonMonoBehavior<Canvas_Skills>
    {
        [SerializeField] private SpellInterfaceController m_SpellInterface = null;

        public void SetTargetSkills(BoardEntity entity)
        {
            m_SpellInterface.SetSpellIcons(entity);
        }
    }
}