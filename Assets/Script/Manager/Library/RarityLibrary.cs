using System;
using KarpysDev.Script.Items;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    public class RarityLibrary : SingletonMonoBehavior<RarityLibrary>
    {
        [SerializeField] private Widget.GenericLibrary<RarityParameter, Rarity> Library = null;

        private void Awake()
        {
            Library.InitializeDictionary();
        }

        public RarityParameter GetParametersViaKey(Rarity type)
        {
            return Library.GetViaKey(type);
        }
    }

    [Serializable]
    public class RarityParameter
    {
        [SerializeField] private Sprite m_RaritySprite = null;
        [SerializeField] private GameObject m_RarityWorldFx = null;
        [SerializeField] private Color m_RarityColor = Color.white;
        [SerializeField] private int m_RarityModifierDrawCount = 0;

        public Sprite RaritySprite => m_RaritySprite;
        public GameObject WorldFx => m_RarityWorldFx;
        public Color RarityColor => m_RarityColor;
        public int ModifierDrawCount => m_RarityModifierDrawCount;
    }
}