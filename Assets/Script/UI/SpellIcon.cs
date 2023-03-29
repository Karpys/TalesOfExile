using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellIcon : MonoBehaviour
{
    //Spell Data to use//
    //Retrieve spell display//
    [SerializeField] private GameObject m_DisplayContainer = null;
    [SerializeField] private Image m_CdFillAmount = null;
    [SerializeField] private Image m_SpellIcon = null;
    [SerializeField] private Image m_SpellIconBorder = null;
    [SerializeField] private TMP_Text m_SpellKey = null;
    [SerializeField] private Button m_ButtonSpellTrigger = null;

    private SpellInterfaceController m_InterfaceController = null;
    
    private TriggerSpellData m_CurrentSpellData = null;
    private KeyCode m_SpellKeyCode = KeyCode.Alpha1;
    private bool m_IsControlKey = false;
    public TriggerSpellData SpellData => m_CurrentSpellData;

    private static int START_ID_KEYCODE = 49;

    private void Awake()
    {
        m_ButtonSpellTrigger.onClick.AddListener(TryUseSpell);
    }

    public void Initialize(SpellInterfaceController controller)
    {
        m_InterfaceController = controller;
    }

    private void TryUseSpell()
    {
        //Conditionnal Spell Check//
        if(m_CurrentSpellData == null)
            return;
        //Stun check ? Mana check ect//
        //Do it in entity class or exterior manager class//
        Debug.Log("Try Use Spell");
        m_InterfaceController.Interpretor.LaunchSpellQueue(m_CurrentSpellData);
    }

    private void Update()
    {
        if (CheckForUse())
        {
            TryUseSpell();
        }
    }

    private bool CheckForUse()
    {
        if (m_IsControlKey)
        {
            return Input.GetKeyDown(m_SpellKeyCode) && InputManager.Instance.IsControlPressed;
        }
        else
        {
            if (InputManager.Instance.IsControlPressed)
                return false;
            return Input.GetKeyDown(m_SpellKeyCode);
        }
    }

    public void SetSpellKey(int id,bool isControlKey)
    {
        m_IsControlKey = isControlKey;
        string controlKey = isControlKey ? "C" : "";
        id -= isControlKey ? 9 : 0;

        m_SpellKey.text = controlKey + (id + 1);
        m_SpellKeyCode = (KeyCode)START_ID_KEYCODE + id;
    }
    public void SetSpell(TriggerSpellData spell)
    {
        m_CurrentSpellData = spell;
        EnableIcon(true);
        m_SpellIcon.sprite = spell.TriggerData.m_SpellIcon;
        m_SpellIconBorder.sprite = spell.TriggerData.m_SpellIconBorder;
        UpdateCooldownVisual();
    }

    public void ClearIcon()
    {
        EnableIcon(false);
        m_CurrentSpellData = null;
        m_CdFillAmount.fillAmount = 0;
    }

    public void UpdateCooldownVisual()
    {
        m_CdFillAmount.fillAmount = m_CurrentSpellData.GetCooldownRatio();
    }

    public void EnableIcon(bool enable)
    {
        m_DisplayContainer.SetActive(enable);
    }
}
