using System;
using UnityEngine;

public class Canvas_Skills : SingletonMonoBehavior<Canvas_Skills>
{
    [SerializeField] private SpellInterfaceController m_SpellInterface = null;

    private void Awake()
    {
        GameManager.Instance.A_OnEnPlayerTurn += RefreshCooldown;
    }

    private void OnDestroy()
    {
        GameManager.Instance.A_OnEnPlayerTurn -= RefreshCooldown;
    }

    public void SetTargetSkills(BoardEntity entity)
    {
        m_SpellInterface.SetSpellIcons(entity);
    }

    private void RefreshCooldown()
    {
        m_SpellInterface.UpdateAllCooldownVisual();
    }
}
