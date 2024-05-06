using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.ClassTree
{
    public class ClassTreeController : MonoBehaviour
    {
        private PlayerBoardEntity m_Player = null;
        private bool m_IsInitialized = false;

        public PlayerBoardEntity Player => m_Player;
        public void Initialize()
        {
            if(m_IsInitialized)
                return;
            
            m_IsInitialized = true;
            m_Player = GameManager.Instance.PlayerEntity;

            ClassTreeSpellContainer[] containers = GetComponentsInChildren<ClassTreeSpellContainer>();

            foreach (ClassTreeSpellContainer classTreeSpellContainer in containers)
            {
                classTreeSpellContainer.Initialize(this);
            }
        }
    }
}