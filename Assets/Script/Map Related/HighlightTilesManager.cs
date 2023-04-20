using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTilesManager : SingletonMonoBehavior<HighlightTilesManager>
{
    // Start is called before the first frame update
    [SerializeField] private GameObject m_HighlightTile = null;
    [SerializeField] private int m_PoolSize = 50;

    // Update is called once per frame
    private int m_CurrentSorting = 1; 

    private List<SpriteRenderer> m_CurrentTiles = new List<SpriteRenderer>();

    private int m_LockTilesCount = 0;
    private void Start()
    {
        GeneratePool();
    }

    private void GeneratePool()
    {
        for (int i = 0; i < m_PoolSize; i++)
        {
            GameObject tile = Instantiate(m_HighlightTile,transform);
            m_CurrentTiles.Add(tile.GetComponent<SpriteRenderer>());
            tile.SetActive(false);
        }
    }

    public void HighlightTiles(List<Vector2Int> tilesPosition,Color? targetColor = null,bool isDynamicSelection = false)
    {
        for (int i = 0; i < tilesPosition.Count; i++)
        {
            Color color = targetColor ?? Color.white;
            SpriteRenderer renderer = m_CurrentTiles[i + m_LockTilesCount];
            renderer.gameObject.SetActive(true);
            renderer.transform.position = MapData.Instance.GetTilePosition(tilesPosition[i].x,tilesPosition[i].y);
            renderer.color = color;
            renderer.sortingOrder = m_CurrentSorting;
        }

        //Refresh unused tiles//
        if (isDynamicSelection)
        {
            for (int i = tilesPosition.Count + m_LockTilesCount; i < m_CurrentTiles.Count; i++)
            {
                m_CurrentTiles[i].gameObject.SetActive(false);
            }
        }
    }

    public void LockHighLightTiles(int count)
    {
        m_CurrentSorting += 1;
        m_LockTilesCount += count;
    }

    public void ResetHighlighTilesAndLock()
    {
        m_LockTilesCount = 0;
        m_CurrentSorting = 1;
        
        for (int i = 0 + m_LockTilesCount; i < m_CurrentTiles.Count; i++)
        {
            m_CurrentTiles[i].gameObject.SetActive(false);
        }
    }

    public void DebugHighlight(Vector2Int position,float alpha = 1)
    {
        GameObject obj = Instantiate(m_HighlightTile, MapData.Instance.GetTilePosition(position), Quaternion.identity, transform);
        obj.GetComponent<SpriteRenderer>().color = obj.GetComponent<SpriteRenderer>().color.setAlpha(alpha);
    }
}
