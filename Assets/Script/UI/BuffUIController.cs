using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.UI
{
    public class BuffUIController : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup m_Layout = null;
        [SerializeField] private float m_LayoutSpacing = 0;
        [SerializeField] private Transform m_BuffContainer = null;
        [SerializeField] private BuffUI m_BuffUIPrefab = null;
        [SerializeField] private Transform m_DebuffContainer = null;
        [SerializeField] private BuffUI m_DebuffUIPrefab = null;
        [SerializeField] private BuffUIDisplayer m_Displayer = null;
        
        private List<BuffUI> m_BuffUIs = new List<BuffUI>();
        private int m_BuffCount = 0;
        private void Start()
        {
            GameManager.Instance.A_OnControlledEntityChange += OnControlledEntityChange;
            
            if (GameManager.Instance && GameManager.Instance.ControlledEntity)
            {
                OnControlledEntityChange(null,GameManager.Instance.ControlledEntity);
            }
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnControlledEntityChange += OnControlledEntityChange;
        }
    
        private void OnControlledEntityChange(BoardEntity old, BoardEntity newEntity)
        {
            ClearBuffUIs();
            InitializeBuffs(newEntity);

            if (old)
            {
                old.Buffs.OnAddBuff -= OnAddBuff;
                old.Buffs.OnRemoveBuff -= OnRemoveBuff;
                old.Buffs.OnCdReduced -= OnCdReduced;
            }

            newEntity.Buffs.OnAddBuff += OnAddBuff;
            newEntity.Buffs.OnRemoveBuff += OnRemoveBuff;
            newEntity.Buffs.OnCdReduced += OnCdReduced;
        }

        private void InitializeBuffs(BoardEntity newEntity)
        {
            EntityBuffs buffs = newEntity.Buffs;

            foreach (Buff buff in buffs.Buffs)
            {
                CreateBuffUI(buff);
            }
        }
    
        private void ClearBuffUIs()
        {
            for (int i = m_BuffUIs.Count - 1; i >= 0; i--)
            {
                Destroy(m_BuffUIs[i].gameObject);
            }
            m_BuffUIs.Clear();
            m_BuffCount = 0;
        }

        private void CreateBuffUI(Buff buff)
        {
            if(buff.BuffCooldown != BuffCooldown.Cooldown)
                return;

            BuffUI buffUI = null;
            
            if (buff.BuffGroup == BuffGroup.Buff)
            {
                m_BuffCount += 1;
                buffUI = Instantiate(m_BuffUIPrefab, m_BuffContainer);
            }
            else
            {
                buffUI = Instantiate(m_DebuffUIPrefab, m_DebuffContainer);   
            }
            buffUI.Initialize(buff,m_Displayer);
            m_BuffUIs.Add(buffUI);
            UpdateLayoutSpacing();
        }

        private void UpdateLayoutSpacing()
        {
            if (m_BuffCount == 0)
            {
                m_Layout.spacing = 0;
            }
            else
            {
                m_Layout.spacing = m_LayoutSpacing;
            }
        }
    
        private void RemoveBuffUI(Buff buff)
        {
            BuffUI targetBuffUI = m_BuffUIs.FirstOrDefault(b => b.AttachedBuff == buff);

            if (targetBuffUI)
            {
                m_BuffUIs.Remove(targetBuffUI);
                Destroy(targetBuffUI.gameObject);

                if (targetBuffUI.AttachedBuff.BuffGroup == BuffGroup.Buff)
                {
                    m_BuffCount -= 1;
                }
            }
            UpdateLayoutSpacing();
        }
    
        private void OnAddBuff(Buff buff)
        {
            CreateBuffUI(buff);
        }
    
        private void OnRemoveBuff(Buff buff)
        {
            RemoveBuffUI(buff);
        }
    
        private void OnCdReduced()
        {
            foreach (BuffUI buffUI in m_BuffUIs)
            {
                buffUI.UpdateText();
            }
        }
    }
}