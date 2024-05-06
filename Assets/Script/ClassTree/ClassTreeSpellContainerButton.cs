using KarpysDev.Script.UI.Pointer;
using UnityEngine;

namespace KarpysDev.Script.ClassTree
{
    public class ClassTreeSpellContainerButton : UIButtonPointer
    {
        [SerializeField] private ClassTreeSpellContainer m_SpellContainer = null;
        public override void OnLeftClick()
        {
            m_SpellContainer.OnLevelUp();
        }

        public override void OnRightClick()
        {
            m_SpellContainer.OnLevelDown();
        }
    }
}