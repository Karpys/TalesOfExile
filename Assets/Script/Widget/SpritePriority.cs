using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritePriority : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WorldTile m_OriginTile = null;
    [SerializeField] private SpriteRenderer m_Renderer = null;

    private void Start()
    {
        SetSpritePriority(-m_OriginTile.Tile.YPos);
    }

    private void SetSpritePriority(int priority)
    {
        m_Renderer.sortingOrder = priority;
    }
}
