using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightManager : SingletonMonoBehavior<LightManager>
{
    [SerializeField] private float m_MidLightedTiles = 0.5f;

    private List<LightTile> m_PreviousHighlightedTiles = new List<LightTile>();
    
    private List<LightSource> m_LightSources = new List<LightSource>();
    private void Start()
    {
        return;
        GameManager.Instance.A_OnNewTurnBegin += RebuildLights;
    }
    
    private void OnDestroy()
    {
        if(GameManager.Instance)
            GameManager.Instance.A_OnNewTurnBegin -= RebuildLights;
    }

    public void AddLightSource(LightSource source, bool rebuild = false)
    {
        m_LightSources.Add(source);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            RebuildLights();
        }
    }

    public void RemoveLightSource(LightSource source)
    {
        m_LightSources.Remove(source);
    }

    private void RebuildLights()
    {
        foreach (LightTile lightTile in m_PreviousHighlightedTiles)
        {
            lightTile.ApplyLight(false);
        }
        
        List<LightTile> newHighlightedTiles = new List<LightTile>();

        foreach (LightSource source in m_LightSources)
        {
            newHighlightedTiles.AddRange(source.ApplyLightV6());
        }

        foreach (LightTile highlightedTile in newHighlightedTiles)
        {
            highlightedTile.ApplyLight(true);
        }

        m_PreviousHighlightedTiles = newHighlightedTiles;
    }
}
