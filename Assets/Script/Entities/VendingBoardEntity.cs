using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class VendingBoardEntity : DefaultBoardEntity
    {
        [SerializeField] private Canvas m_CanvasTransform = null;

        public void Open()
        {
            m_CanvasTransform.gameObject.SetActive(true);
        }

        public void Close()
        {
            m_CanvasTransform.gameObject.SetActive(false);
        }
    }
}