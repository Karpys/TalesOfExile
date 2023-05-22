using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellInterfaceController : UIPointer
{
    //UI Part//
    [SerializeField] private SpellIcon m_SpellUI = null;
    [SerializeField] private RectTransform m_SpellLayout = null;
    [SerializeField] private SpellInterpretor m_Interpretor = null;
    
    private SpellIcon[] m_IconsHolder;

    public const int SPELL_DISPLAY_COUNT = 18;
    public SpellInterpretor Interpretor => m_Interpretor;

    private SpellIcon m_CurrentPointer = null;
    //Spell Data Part//
    //List des spells attribue au spell ui icon//
    private void Awake()
    {
        //Initialize tiles depend on screen and tile size//
        m_IconsHolder = new SpellIcon[SPELL_DISPLAY_COUNT];
        for (int i = 0; i < SPELL_DISPLAY_COUNT; i++)
        {
            m_IconsHolder[i] = Instantiate(m_SpellUI, m_SpellLayout.transform);
            m_IconsHolder[i].Initialize(this, i);
            m_IconsHolder[i].SetSpellKey(i,i > (SPELL_DISPLAY_COUNT/2) - 1);
        }

        m_CurrentPointer = m_IconsHolder[0];
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (m_CurrentPointer.PointerUp)
            {
                m_CurrentPointer.TryUseSpell();
            }
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

    public void SetPointer(SpellIcon pointer)
    {
        m_CurrentPointer = pointer;
    }

    protected override void OnEnter()
    {
        GlobalCanvas.Instance.SetCanvasPointer(this, UICanvasType.SpellIcons);
    }

    protected override void OnExit(){}
}
