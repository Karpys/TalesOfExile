using System;
using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class StartSkillTreeNode : BaseSkillTreeNode
    {
        private void Start()
        {
            Unlock();
        }

        public override bool CanLock()
        {
            return false;
        }

        protected override void Apply()
        {
        }

        protected override void UnApply()
        {
        }
    }
}