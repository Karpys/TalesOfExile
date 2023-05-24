using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Entities.BuffRelated;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class BuffUIController : MonoBehaviour
    {
        [SerializeField] private Transform m_BuffContainer = null;
        [SerializeField] private BuffUI m_BuffUIPrefab = null;

        private List<BuffUI> m_BuffUIs = new List<BuffUI>();
        private void Start()
        {
            GameManager.Instance.A_OnControlledEntityChange += OnControlledEntityChange;
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
        }

        private void CreateBuffUI(Buff buff)
        {
            if(buff.BuffCooldown != BuffCooldown.Cooldown)
                return;
        
            BuffUI buffUI = Instantiate(m_BuffUIPrefab, m_BuffContainer);
            buffUI.Initialize(buff);
            m_BuffUIs.Add(buffUI);
        }
    
        private void RemoveBuffUI(Buff buff)
        {
            BuffUI targetBuffUI = m_BuffUIs.FirstOrDefault(b => b.AttachedBuff == buff);

            if (targetBuffUI)
            {
                m_BuffUIs.Remove(targetBuffUI);
                Destroy(targetBuffUI.gameObject);
            }
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