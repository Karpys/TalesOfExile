using KarpysDev.Script.UI;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class VendingBoardEntity : DefaultBoardEntity
    {
        [SerializeField] private Canvas_Shop m_ShopCanvas = null;

        public void Open()
        {
            m_ShopCanvas.Open();
        }

        public void Close()
        {
            m_ShopCanvas.Close();
        }
    }
}