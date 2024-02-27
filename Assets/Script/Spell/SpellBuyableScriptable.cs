using System;
using KarpysDev.Script.Entities;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    [CreateAssetMenu(fileName = "SpellBuyable", menuName = "SpellBuyable", order = 0)]
    public class SpellBuyableScriptable : ScriptableObject,IBuyableData
    {
        [SerializeField] private TriggerSpellDataScriptable m_SpellData = null;
        [SerializeField] private float m_Price = 100;

        public string Id => m_SpellData.SpellName;

        public void SetSpellData(TriggerSpellDataScriptable spellData)
        {
            m_SpellData = spellData;
        }

        public IUIBuyable ToUIBuyable(Transform uiParent)
        {
            SpellBuyable spellBuyable = new SpellBuyable();
            spellBuyable.UIParent = uiParent;
            spellBuyable.Price = m_Price;
            spellBuyable.Id = Id;
            spellBuyable.InitializeSpell(new SpellInfo(m_SpellData,1, 0,SpellLearnType.Learned));
            return spellBuyable;
        }
    }

    public interface IBuyableData
    {
        public string Id { get;}
        public IUIBuyable ToUIBuyable(Transform uiParent);
    }
    
    public interface IUIBuyable
    {
        public float Price { get; set;}
        public string Id { get; set;}

        public Sprite GetIcon();
        public void OnBuy();
        
        public Transform UIParent { get; set; }
        public void DisplayBuyable();
        public void HideBuyable();
    }
}