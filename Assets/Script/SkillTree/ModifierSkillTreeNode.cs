using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class ModifierSkillTreeNode : BaseSkillTreeNode
    {
        [SerializeField] private Modifier m_Modifier = null;
        protected override void Apply()
        {
            ModifierUtils.ApplyModifier(m_Modifier,m_SkillTreeNodeController.AttachedEntity);
        }

        protected override void UnApply()
        {
            ModifierUtils.UnapplyModifier(m_Modifier,m_SkillTreeNodeController.AttachedEntity);
        }
    }
}