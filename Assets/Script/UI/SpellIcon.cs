using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    //Spell Data to use//
    //Retrieve spell display//
    [SerializeField] private Image m_SpellIcon = null;
    [SerializeField] private Image m_SpellIconBorder = null;
    [SerializeField] private TMP_Text m_SpellKey = null;
    [SerializeField] private Button m_ButtonSpellTrigger = null;

    private SpellData m_CurrentSpellData = null;
    private KeyCode m_SpellKeyCode = KeyCode.Alpha1;

    private static int START_ID_KEYCODE = 97;

    private void Awake()
    {
        m_ButtonSpellTrigger.onClick.AddListener(TryUseSpell);
    }

    private void TryUseSpell()
    {
        //Conditionnal Spell Check//
        if(m_CurrentSpellData == null)
            return;
        //Stun check ? Mana check ect//
        //Do it in entity class or exterior manager class//
        Debug.Log("Try Use Spell");
        SpellInterpretor.Instance.LaunchSpellQueue(m_CurrentSpellData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(m_SpellKeyCode))
        {
            TryUseSpell();
        }
    }

    public void SetSpellKey(int id)
    {
        m_SpellKey.text = id + 1+ "";
        // m_SpellKeyCode = (KeyCode)START_ID_KEYCODE + id;
    }
    public void SetSpell(SpellData spell)
    {
        m_CurrentSpellData = spell;
        EnableIcon(true);
        m_SpellIcon.sprite = spell.m_Data.m_SpellIcon;
        m_SpellIconBorder.sprite = spell.m_Data.m_SpellIconBorder;
    }

    public void EnableIcon(bool enable)
    {
        
        m_SpellIcon.gameObject.SetActive(enable);
        m_SpellIconBorder.gameObject.SetActive(enable);
    }
}
