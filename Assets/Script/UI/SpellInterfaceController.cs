using System.Collections.Generic;
using UnityEngine;

public class SpellInterfaceController : MonoBehaviour
{
    //UI Part//
    [SerializeField] private SpellIcon m_SpellUI = null;
    [SerializeField] private RectTransform m_SpellLayout = null;
    [SerializeField] private SpellInterpretor m_Interpretor = null;
    private SpellIcon[] m_IconsHolder;

    public const int SPELL_DISPLAY_COUNT = 18;
    public SpellInterpretor Interpretor => m_Interpretor;
    //Spell Data Part//
    //List des spells attribue au spell ui icon//
    private void Awake()
    {
        //Initialize tiles depend on screen and tile size//
        m_IconsHolder = new SpellIcon[SPELL_DISPLAY_COUNT];
        for (int i = 0; i < SPELL_DISPLAY_COUNT; i++)
        {
            m_IconsHolder[i] = Instantiate(m_SpellUI, m_SpellLayout.transform);
            m_IconsHolder[i].Initialize(this);
            m_IconsHolder[i].SetSpellKey(i,i > (SPELL_DISPLAY_COUNT/2) - 1);
        }
    }

    public void SetSpellIcons(BoardEntity entity)
    {
        SpellData[] spellsToDisplay = entity.GetDisplaySpells();
        
        for (int i = 0; i < spellsToDisplay.Length; i++)
        {
            if(!(spellsToDisplay[i] is TriggerSpellData triggerSpell))
                continue;
            
            m_IconsHolder[i].SetSpell(triggerSpell);
        }

        for (int i = spellsToDisplay.Length; i < SPELL_DISPLAY_COUNT; i++)
        {
            m_IconsHolder[i].ClearIcon();
        }
    }

    public void UpdateAllCooldownVisual()
    {
        foreach (SpellIcon spellIcon in m_IconsHolder)
        {
            if (spellIcon.SpellData != null)
            {
                spellIcon.UpdateCooldownVisual();
            }
        }
    }
}
