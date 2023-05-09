using System;
using UnityEngine;

public class PlaceableLibrary : SingletonMonoBehavior<PlaceableLibrary>
{
    [SerializeField] private GenericLibrary<MapPlaceable, PlaceableType> m_Library = null;

    private void Start()
    {
        m_Library.InitializeDictionary();
    }

    public MapPlaceable GetViaKey(PlaceableType placeableType)
    {
        return m_Library.GetViaKey(placeableType);
    }
}


public enum PlaceableType
{
    BurningGround
}
