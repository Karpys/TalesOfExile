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
        foreach (LightTile lightTile in m_PreviousHighlightedTiles)
        {
            lightTile.ApplyLight(false);
        }
        
        List<LightTile> newHighlightedTiles = new List<LightTile>();

        foreach (LightSource source in m_LightSources)
        {
            newHighlightedTiles.AddRange(source.ApplyLightV2());
        }

        newHighlightedTiles = newHighlightedTiles.Distinct().ToList();

        foreach (LightTile lightTile in newHighlightedTiles)
        {
            if (lightTile.IsShadow)
            {
                lightTile.ApplyLight(false);
            }
            else
            {
                lightTile.ApplyLight(true);
            }
        }
        
        m_PreviousHighlightedTiles = newHighlightedTiles;
        return;
        for (int i = 0; i < newHighlightedTiles.Count; i++)
        {
            if (newHighlightedTiles[i].IsShadow)
            {
                newHighlightedTiles[i].ApplyLight(false);
                newHighlightedTiles.Remove(newHighlightedTiles[i]);
                i--;
            }
        }

        /*foreach (LightTile lightTile in m_PreviousHighlightedTiles)
        {
            if(!newHighlightedTiles.Contains(lightTile))
                lightTile.ApplyLight(false);
        }*/

        foreach (LightTile lightTile in newHighlightedTiles)
        {
            lightTile.ApplyLight(true);
        }

    }
}
