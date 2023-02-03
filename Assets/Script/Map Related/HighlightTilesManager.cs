using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightTilesManager : SingletonMonoBehavior<HighlightTilesManager>
{
    // Start is called before the first frame update
    [SerializeField] private GameObject m_HighlightTile = null;
    [SerializeField] private int m_PoolSize = 50;

    public ZoneSelection m_SelectionTest = null;
    public int range = 3;
    // Update is called once per frame

    private List<GameObject> m_CurrentTiles = new List<GameObject>();

    private int m_LockTilesCount = 0;
    private void Start()
    {
        GeneratePool();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            HighlightTiles(ZoneTileManager.Instance.GetSelectionZone(m_SelectionTest,Vector2Int.zero,range));
        }
    }

    private void GeneratePool()
    {
        for (int i = 0; i < m_PoolSize; i++)
        {
            GameObject tile = Instantiate(m_HighlightTile,transform);
            m_CurrentTiles.Add(tile);
            tile.SetActive(false);
        }
    }

    public void HighlightTiles(List<Vector2Int> tilesPosition)
    {
        for (int i = 0; i < tilesPosition.Count; i++)
        {
            m_CurrentTiles[i + m_LockTilesCount].SetActive(true);
            m_CurrentTiles[i + m_LockTilesCount].transform.position = MapData.Instance.GetTilePosition(tilesPosition[i].x,tilesPosition[i].y);
        }

        for (int i = tilesPosition.Count + m_LockTilesCount; i < m_CurrentTiles.Count; i++)
        {
            m_CurrentTiles[i].SetActive(false);
        }
    }

    public void LockHighLightTiles(int count)
    {
        m_LockTilesCount += count;
    }

    public void ResetHighlighTilesAndLock()
    {
        m_LockTilesCount = 0;
        
        for (int i = 0 + m_LockTilesCount; i < m_CurrentTiles.Count; i++)
        {
            m_CurrentTiles[i].SetActive(false);
        }
    }
}
