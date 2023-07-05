using KarpysDev.Script.Manager;
using UnityEngine;

namespace KarpysDev.Script.Map_Related.MapGeneration
{
    public class MapTileReloader : MonoBehaviour
    {
        private Vector2Int m_ReloadPosition = Vector2Int.zero;

        private DestroyObjectCleaner m_Cleaner = null;
        private void Start()
        {
            m_Cleaner = new DestroyObjectCleaner(gameObject);
        
            GameManager.Instance.A_OnEndTurn += CheckForPlayerPosition;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnEndTurn -= CheckForPlayerPosition;
        }

        public void SetReloadPosition(Vector2Int reloadPosition)
        {
            m_ReloadPosition = reloadPosition;
            transform.position = MapData.Instance.GetTilePosition(reloadPosition);
        }

        private void CheckForPlayerPosition()
        {
            if (GameManager.Instance.PlayerEntity.EntityPosition == m_ReloadPosition)
            {
                OnPlayerOnTile();
            } 
        }

        protected virtual void OnPlayerOnTile()
        {
            MapGenerator.Instance.NextMap();
        }
    }
}
