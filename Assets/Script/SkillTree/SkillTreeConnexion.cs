using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.SkillTree
{
    public class SkillTreeConnexion : MonoBehaviour
    {
        [SerializeField] private Vector2Int m_ConnexionId = Vector2Int.zero;
        [SerializeField] private Image m_ConnexionImage = null;

        private const float DISABLE_COLOR = 0.4f;
        public Vector2Int ConnexionId => m_ConnexionId;
        public void AssignId(Vector2Int id)
        {
            m_ConnexionId = id;
        }

        public void SetState(bool enable)
        {
            Color newColor = enable ? Color.white : new Color(DISABLE_COLOR, DISABLE_COLOR, DISABLE_COLOR);
            m_ConnexionImage.color = newColor;
        }
    }
}