using System;
using UnityEngine;

public class RarityLibrary : SingletonMonoBehavior<RarityLibrary>
{
    [SerializeField] private GenericObjectLibrary<RarityParameter, Rarity> Library = null;

    private void Awake()
    {
        Library.InitializeDictionary();
    }

    public RarityParameter GetParametersViaKey(Rarity type)
    {
        return Library.GetViaKey(type);
    }
}

[System.Serializable]
public class RarityParameter
{
    [SerializeField] private Sprite m_RaritySprite = null;
    [SerializeField] private GameObject m_RarityWorldFx = null;
    [SerializeField] private Color m_RarityColor = Color.white;

    public Sprite RaritySprite => m_RaritySprite;
    public GameObject WorldFx => m_RarityWorldFx;
    public Color RarityColor => m_RarityColor;
}