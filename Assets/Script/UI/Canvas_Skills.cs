using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class Canvas_Skills : MonoBehaviour
    {
        [SerializeField] private SpellInterfaceController m_SpellInterface = null;

        private void Awake()
        {
            GameManager.Instance.A_OnEndPlayerTurn += RefreshCooldown;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnEndPlayerTurn -= RefreshCooldown;
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
}
