using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffUI : MonoBehaviour
{
    [SerializeField] private Image m_BuffVisual = null;
    [SerializeField] private TMP_Text m_BuffCooldown = null;
    
    private Buff m_AttachedBuff = null;

    public Buff AttachedBuff => m_AttachedBuff;

    public void Initialize(Buff buff)
    {
        m_AttachedBuff = buff;
        m_BuffVisual.sprite = buff.BuffInfo.BuffVisual;
        UpdateText();
    }

    public void UpdateText()
    {
        m_BuffCooldown.text = m_AttachedBuff.Cooldown + "";
    }
}