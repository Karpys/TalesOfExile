using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class DefaultSkillTreeNode : BaseSkillTreeNode
    {
        protected override void Apply()
        {
            Debug.Log("Apply Default Node");
        }

        protected override void UnApply()
        {
            Debug.Log("UnApply Default Node");
        }
    }
}