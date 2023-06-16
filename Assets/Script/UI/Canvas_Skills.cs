using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class Canvas_Skills : MonoBehaviour
    {
        [SerializeField] private SpellInterfaceController m_SpellInterface = null;
        [SerializeField] private SpellSelectionUI m_SpellSelection = null;

        private PlayerBoardEntity m_Player = null;
        private void Awake()
        {
            GameManager.Instance.A_OnEndPlayerTurn += RefreshCooldown;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnEndPlayerTurn -= RefreshCooldown;
        }

        public void RefreshTargetSkills(BoardEntity entity)
        {
            m_SpellInterface.SetSpellIcons(entity);
        }

        private void RefreshCooldown()
        {
            m_SpellInterface.UpdateAllCooldownVisual();
        }

        public void ShowSpellSelection(SpellIcon pointer)
        {
            m_SpellSelection.Show(pointer.SpellId,pointer.transform);
        }
    }
}
