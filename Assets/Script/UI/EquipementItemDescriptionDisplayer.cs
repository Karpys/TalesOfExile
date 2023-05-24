using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Items;
using KarpysDev.Script.Utils;
using TMPro;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class EquipementItemDescriptionDisplayer : ItemDescriptionDisplayer
    {
        [Header("Equipement Parameters")]
        [SerializeField] private AdaptUILayoutSize m_ModifierLayout = null;
        [SerializeField] private TMP_Text m_ModifierTextPrefab = null; 

        public override void Initialize(Item item)
        {
            EquipementItem equipementItem = (EquipementItem) item;
            Modifier[] modifiers = equipementItem.ItemModifiers;

            for (int i = 0; i < modifiers.Length; i++)
            {
                TMP_Text modifierText = Instantiate(m_ModifierTextPrefab, m_ModifierLayout.transform);
                modifierText.text = ModifierUtils.GetModifierDescription(modifiers[i]);
            }
        
            m_ModifierLayout.AdaptSize();
            base.Initialize(item);
        }
    }
}