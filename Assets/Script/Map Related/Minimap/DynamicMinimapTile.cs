using KarpysDev.KarpysUtils.ObjectPooling;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.Map_Related.Minimap
{
    public class DynamicMinimapTile : Image
    {
        private IReturnable<DynamicMinimapTile> m_InitialPool = null;
        private IPositionable m_Positionable = null;

        public void Initialize(IReturnable<DynamicMinimapTile> initialPool)
        {
            m_InitialPool = initialPool;
        }

        public Vector2 GetPosition()
        {
            return m_Positionable.Position;
        }

        public void AssignPositionable(IPositionable positionable)
        {
            m_Positionable = positionable;
        }
        
        public void Return()
        {
            m_InitialPool.Return(this);
        }

        public void Clear()
        {
            m_Positionable = null;
        }
    }
}