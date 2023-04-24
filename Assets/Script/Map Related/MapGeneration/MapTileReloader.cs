using System;
using UnityEngine;

public class MapTileReloader : MonoBehaviour
{
    private MapGenerator m_Generator = null;
    private Vector2Int m_ReloadPosition = Vector2Int.zero;

    private DestroyObjectCleaner m_Cleaner = null;
    private void Start()
    {
        m_Generator = FindObjectOfType<MapGenerator>();
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
    }

    private void CheckForPlayerPosition()
    {
        if (GameManager.Instance.PlayerEntity.EntityPosition == m_ReloadPosition)
        {
            m_Generator.ReloadMap();
        } 
    }
}
