using System.Collections.Generic;
using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    [CreateAssetMenu(menuName = "InventoryPoolObject", fileName = "InventoryPoolObject/NewPool", order = 0)]
    public class InventoryPoolObjectScriptable : ScriptableObject
    {
        [SerializeField] private InventoryPoolObject m_InventoryPoolObject = null;

        public List<Item> Draw(ItemDraw itemDraw)
        {
            return m_InventoryPoolObject.Draw(itemDraw);
        }
    }
}