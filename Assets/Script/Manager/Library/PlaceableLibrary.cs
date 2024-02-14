using KarpysDev.Script.Map_Related;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    public class PlaceableLibrary : SingletonMonoBehavior<PlaceableLibrary>
    {
        [SerializeField] private GenericLibrary<PlaceableType,MapPlaceable> m_Library = null;

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
        BurningGround,
        LightningStormArea,
    }
}