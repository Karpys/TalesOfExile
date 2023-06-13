using System;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class SkillTreeNodeController : MonoBehaviour
    {
        private BaseSkillTreeNode m_CurrentNode = null;
        private BoardEntity m_AttachedEntity = null;

        public BoardEntity AttachedEntity => m_AttachedEntity;
        public void Initialize(BoardEntity attachedEntity)
        {
            m_AttachedEntity = attachedEntity;
        }
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                TrySelectedNodeAction();
            }
        }

        private void TrySelectedNodeAction()
        {
            if(m_CurrentNode == null || !m_CurrentNode.PointerUp)
                return;

            if (!m_CurrentNode.IsUnlocked && m_CurrentNode.CanUnlock())
            {
                m_CurrentNode.Unlock();
            }
            else if(m_CurrentNode.IsUnlocked && m_CurrentNode.CanLock())
            {
                m_CurrentNode.Lock();
            }
        }
        
        public void SetCurrentNode(BaseSkillTreeNode baseSkillTreeNode)
        {
            m_CurrentNode = baseSkillTreeNode;
        }
    }
}