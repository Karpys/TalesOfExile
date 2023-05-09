using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlaceable : MonoBehaviour
{
    protected Vector2Int m_Position = Vector2Int.zero;

    protected void Place(Vector2Int position)
    {
        transform.position = MapData.Instance.GetTilePosition(position);
        m_Position = position;
    }
}