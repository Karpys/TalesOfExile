using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public class SpellBuyable:IUIBuyable
    {
        private TriggerSpellData m_TriggerSpellData = null;
        public float Price { get; set; }
        public string Id { get; set; }
        public Transform UIParent { get; set; }

        public Sprite GetIcon()
        {
            return m_TriggerSpellData.TriggerData.SpellIcon;
        }

        public void OnBuy()
        {
            GoldManager.Instance.ChangeGoldValue(-Price);
            GameManager.Instance.PlayerEntity.AddSpellToSpellList(new SpellInfo(m_TriggerSpellData.TriggerData, 1,
                SpellLearnType.Learned));
            m_TriggerSpellData = null;
            GameManager.Instance.PlayerEntity.UpdateSpellPriority();
        }
        
        public void DisplayBuyable()
        {
            m_TriggerSpellData.SpellTrigger.ComputeSpellData(GameManager.Instance.PlayerEntity);
            GlobalCanvas.Instance.GetSpellUIDisplayer().DisplaySpell(m_TriggerSpellData,UIParent);
        }

        public void HideBuyable()
        {
            GlobalCanvas.Instance.GetSpellUIDisplayer().HideSpell();
        }

        public void InitializeSpell(SpellInfo spellInfo)
        {
            m_TriggerSpellData = GameManager.Instance.PlayerEntity.RegisterSpell(spellInfo);
        }
    }
}