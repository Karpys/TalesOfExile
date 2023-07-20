using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public class MapTileReloader : MonoBehaviour
    {
        private Vector2Int m_ReloadPosition = Vector2Int.zero;

        private DestroyObjectCleaner m_Cleaner = null;
        private bool m_HasBeenUsed = false;
        private void Start()
        {
            m_Cleaner = new DestroyObjectCleaner(gameObject);
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnEndTurn -= CheckForPlayerPosition;
        }

        public void Initialize(Vector2Int reloadPosition)
        {
            GameManager.Instance.A_OnEndTurn += CheckForPlayerPosition;
            m_ReloadPosition = reloadPosition;
            transform.position = MapData.Instance.GetTilePosition(reloadPosition);
        }

        private void CheckForPlayerPosition()
        {
            if (m_HasBeenUsed)
                return;
                    
            if (GameManager.Instance.PlayerEntity.EntityPosition == m_ReloadPosition)
            {
                OnPlayerOnTile();
                m_HasBeenUsed = true;
            } 
        }

        protected virtual void OnPlayerOnTile()
        {
            MapGenerator.Instance.NextMap();
        }
    }
}
