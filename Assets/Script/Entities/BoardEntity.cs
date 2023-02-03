﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardEntity : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected int m_XPosition = 0;
    [SerializeField] protected int m_YPosition = 0;
    [SerializeField] protected List<SpellData> m_Spells = new List<SpellData>();

    protected MapData m_TargetMap = null;

    public Vector2Int EntityPosition => new Vector2Int(m_XPosition, m_YPosition);
    public List<SpellData> Spells => m_Spells;

    protected virtual void Start()
    {
        Debug.Log("New entity created: " + gameObject.name + "at :" + EntityPosition);
        
        //Initi Serialize Spells//
        RegisterStartSpells();
    }
    
    //Board Related
    public void Place(int x, int y, MapData targetMap)
    {
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap = targetMap;
        transform.position = targetMap.GetTilePosition(x, y);
        m_TargetMap.Map.Tiles[x, y].Walkable = false;
    }

    public virtual void MoveTo(int x,int y)
    {
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = true;
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = false;
        //OnMove ?//
        Movement();
    }
    
    public void MoveTo(Vector2Int pos)
    {
        MoveTo(pos.x,pos.y);
    }

    protected virtual void Movement()
    {
        transform.position = m_TargetMap.GetTilePosition(m_XPosition, m_YPosition);
    }
    
    //Entity Behaviour Related//
    private void RegisterStartSpells()
    {
        for (int i = 0; i < m_Spells.Count; i++)
        {
            RegisterSpell(m_Spells[i]);
        }
    }

    protected void RegisterSpell(SpellData spell)
    {
        spell.AttachedEntity = this;
    }
}