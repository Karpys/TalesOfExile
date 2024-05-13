using KarpysDev.KarpysUtils.ObjectPooling;
using UnityEngine.UI;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class DynamicMinimapTile : Image
    {
        private GameObjectPool<DynamicMinimapTile> m_InitialPool = null;

        public void Initialize(GameObjectPool<DynamicMinimapTile> initialPool)
        {
            m_InitialPool = initialPool;
        }

        public void Return()
        {
            m_InitialPool.Return(this);
        }
    }
}