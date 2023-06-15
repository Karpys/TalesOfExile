using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class SkillTreeNodeController : MonoBehaviour
    {
        private BaseSkillTreeNode m_CurrentNode = null;
        private BoardEntity m_AttachedEntity = null;

        private Dictionary<Vector2Int, SkillTreeConnexion> m_ConnexionDictionary = new Dictionary<Vector2Int, SkillTreeConnexion>();


        public BoardEntity AttachedEntity => m_AttachedEntity;
        public void Initialize(BoardEntity attachedEntity)
        {
            m_AttachedEntity = attachedEntity;
            
            SkillTreeConnexion[] connexion = transform.GetComponentsInChildren<SkillTreeConnexion>();

            m_ConnexionDictionary.Clear();

            for (int i = 0; i < connexion.Length; i++)
            {
                m_ConnexionDictionary.Add(connexion[i].ConnexionId,connexion[i]);
            }
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
                AddConnexion(m_CurrentNode);
            }
            else if(m_CurrentNode.IsUnlocked && m_CurrentNode.CanLock())
            {
                m_CurrentNode.Lock();
                RemoveConnexion(m_CurrentNode);
            }
        }
        
        public void SetCurrentNode(BaseSkillTreeNode baseSkillTreeNode)
        {
            m_CurrentNode = baseSkillTreeNode;
        }

        private void AddConnexion(BaseSkillTreeNode baseSkillTreeNode)
        {
            foreach (BaseSkillTreeNode connectedNode in baseSkillTreeNode.ConnectedNodes)
            {
                if (!connectedNode.IsUnlocked) continue;
                
                if(m_ConnexionDictionary.TryGetValue(SkillTreeUtils.GetConnexion(baseSkillTreeNode,connectedNode),out SkillTreeConnexion connexion))
                {
                    connexion.SetState(true);
                }
                else
                {
                    Debug.LogError("Cannot find connexion in Dicionary");
                }
            }
        }

        private void RemoveConnexion(BaseSkillTreeNode baseSkillTreeNode)
        {
            foreach (BaseSkillTreeNode connectedNode in baseSkillTreeNode.ConnectedNodes)
            {
                if (m_ConnexionDictionary.TryGetValue(SkillTreeUtils.GetConnexion(baseSkillTreeNode, connectedNode),out SkillTreeConnexion connexion))
                {
                    connexion.SetState(false);
                }
            }
        }
    }
}