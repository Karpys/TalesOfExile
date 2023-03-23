using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : SingletonMonoBehavior<LightManager>
{
    [SerializeField] private float m_MidLightedTiles = 0.5f;

    private List<LightTile> m_PreviousHighlightedTiles = new List<LightTile>();
    
    private List<LightSource> m_LightSources = new List<LightSource>();
    private void Start()
    {
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
        
        if(rebuild)
            RebuildLights();
    }

    public void RemoveLightSource(LightSource source)
    {
        m_LightSources.Remove(source);
    }

    private void RebuildLights()
    {
        List<LightTile> newHighlightedTiles = new List<LightTile>();

        foreach (LightSource source in m_LightSources)
        {
            newHighlightedTiles.AddRange(source.ApplyLight());
        }

        foreach (LightTile lightTile in m_PreviousHighlightedTiles)
        {
            if(!newHighlightedTiles.Contains(lightTile))
                lightTile.ApplyLight(true,m_MidLightedTiles);
        }

        foreach (LightTile lightTile in newHighlightedTiles)
        {
            lightTile.ApplyLight(true,1);
        }

        m_PreviousHighlightedTiles = newHighlightedTiles;
    }
}
