using System;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    [CreateAssetMenu(fileName = "SpellBuyable", menuName = "SpellBuyable", order = 0)]
    public class SpellBuyableScriptable : ScriptableObject,IBuyable
    {
        [SerializeField] private TriggerSpellDataScriptable m_SpellData = null;
        [SerializeField] private float m_Price = 100;

        public float GetPrice()
        {
            return m_Price;
        }

        public void SetSpellData(TriggerSpellDataScriptable spellData)
        {
            m_SpellData = spellData;
        }
    }
    public interface IBuyable
    {
        public float GetPrice();
    }
}