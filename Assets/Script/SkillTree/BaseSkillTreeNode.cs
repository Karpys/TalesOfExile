using System.Linq;
using KarpysDev.Script.UI.Pointer;
using TweenCustom;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.SkillTree
{
    public abstract class BaseSkillTreeNode : UIPointer
    {
        [Header("Base Skill Tree Node")]
        [SerializeField] protected SkillTreeNodeController m_SkillTreeNodeController = null;
        [Header("Visual References")] 
        [SerializeField] protected Image m_NodeSprite = null;
        [SerializeField] protected Image m_NodeOutlineValidation = null;

        [Header("References")]
        [SerializeField] private int m_SkillTreeNodeId = 0;
        [SerializeField] private BaseSkillTreeNode[] m_ConnectedNodes = null;

        private bool m_IsUnlocked = false;

        //Visual//
        private static float NODE_OUTLINE_BASE_COLOR = 0.43f;
        private static float NODE_SPRITE_ALPHA = 0.5f;
        
        //Getter//
        public int NodeId => m_SkillTreeNodeId;
        public bool IsUnlocked => m_IsUnlocked;
        private int ConnectedUnlocked => m_ConnectedNodes.Where(n => n.IsUnlocked).ToArray().Length;
        private BaseSkillTreeNode[] ConnetectedConnectedNodes => m_ConnectedNodes.Where(n => n.IsUnlocked).ToArray();
        public BaseSkillTreeNode[] ConnectedNodes => m_ConnectedNodes;

        public void Unlock()
        {
            m_IsUnlocked = true;
            SetVisualUnlock();
            Apply();
        }

        public virtual bool CanUnlock()
        {
            foreach (BaseSkillTreeNode baseSkillTreeNode in m_ConnectedNodes)
            {
                if (baseSkillTreeNode.IsUnlocked)
                    return true;
            }

            return false;
        }

        public void Lock()
        {
            if(!CanLock())
                return;
            
            m_IsUnlocked = false;
            SetVisualLock();
            UnApply();
        }
        
        public virtual bool CanLock()
        {
            //Check if a node can be unlearned//
            foreach (BaseSkillTreeNode baseSkillTreeNode in ConnetectedConnectedNodes)
            {
                if (baseSkillTreeNode.ConnectedUnlocked == 1)
                {
                    if(baseSkillTreeNode as StartSkillTreeNode)
                        continue;
                    
                    Debug.Log(baseSkillTreeNode.name);   
                    return false;
                }
            }
            
            return true;
        }
        
        protected virtual void VisualUnlock()
        {
            m_NodeSprite.DoColor(new Color(1, 1, 1,1), 0.3f);
            m_NodeOutlineValidation.DoColor(new Color(1, 1, 1,1), 0.3f);
        }

        protected virtual void SetVisualUnlock()
        {
            m_NodeSprite.transform.DoKill();
            m_NodeOutlineValidation.transform.DoKill();
            m_NodeSprite.color = Color.white;
            m_NodeOutlineValidation.color = Color.white;
        }
        
        protected virtual void VisualLock()
        {
            m_NodeSprite.DoColor(new Color(1, 1, 1,NODE_SPRITE_ALPHA), 0.3f);
            m_NodeOutlineValidation.DoColor(new Color(NODE_OUTLINE_BASE_COLOR, NODE_OUTLINE_BASE_COLOR, NODE_OUTLINE_BASE_COLOR,1), 0.3f);
        }
        
        protected virtual void SetVisualLock()
        {
            m_NodeSprite.transform.DoKill();
            m_NodeOutlineValidation.transform.DoKill();
            m_NodeSprite.color = new Color(1, 1, 1, NODE_SPRITE_ALPHA);
            m_NodeOutlineValidation.color = new Color(NODE_OUTLINE_BASE_COLOR, NODE_OUTLINE_BASE_COLOR, NODE_OUTLINE_BASE_COLOR, 1);
        }

        protected abstract void Apply();
        protected abstract void UnApply();
        
        protected override void OnEnter()
        {
            m_SkillTreeNodeController.SetCurrentNode(this);
            
            if(!m_IsUnlocked)
                VisualUnlock();
        }

        protected override void OnExit()
        {
            if(!m_IsUnlocked)
                VisualLock();
        }
    }
}