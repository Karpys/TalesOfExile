using System;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.SkillTree
{
    public class SkillTreeController:MonoBehaviour
    {
        [SerializeField] private Transform m_GlobalCanvas = null;
        [SerializeField] private SkillTreeNodeController m_SkillTree = null;

        private bool m_IsOpen = false;
        
        public void Initialize(BoardEntity playerEntity)
        {
            m_SkillTree.Initialize(playerEntity);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                EnableSkillTree();
            }
        }

        private void EnableSkillTree()
        {
            if (m_IsOpen)
            {
                m_IsOpen = false;

                //Todo Camera Manager
                m_SkillTree.gameObject.SetActive(false);
                m_GlobalCanvas.gameObject.SetActive(true);
            }
            else
            {
                m_IsOpen = true;
                
                m_SkillTree.gameObject.SetActive(true);
                m_GlobalCanvas.gameObject.SetActive(false);
                //Todo Camera Manager
            }
        }
    }
}