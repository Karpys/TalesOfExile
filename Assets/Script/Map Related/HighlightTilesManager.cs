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

    private List<GameObject> m_CurrentTiles = new List<GameObject>();

    private void Start()
    {
        GeneratePool();
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

    public void GenerateHighlightTiles(List<Vector2Int> tilesPosition,Vector2Int origin)
    {
        for (int i = 0; i < tilesPosition.Count; i++)
        {
            m_CurrentTiles[i].SetActive(true);
            m_CurrentTiles[i].transform.position = MapData.Instance.GetTilePosition(tilesPosition[i].x + origin.x,tilesPosition[i].y + origin.y);
        }

        for (int i = tilesPosition.Count; i < m_CurrentTiles.Count; i++)
        {
            m_CurrentTiles[i].SetActive(false);
        }
    }
}
