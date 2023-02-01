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

    public void SetSpellKey(int id)
    {
        m_SpellKey.text = id + "";
    }
    public void SetSpellIcon(Sprite icon,Sprite border)
    {
        EnableIcon(true);
        m_SpellIcon.sprite = icon;
        m_SpellIconBorder.sprite = border;
    }

    public void EnableIcon(bool enable)
    {
        m_SpellIcon.gameObject.SetActive(enable);
        m_SpellIconBorder.gameObject.SetActive(enable);
    }
}
