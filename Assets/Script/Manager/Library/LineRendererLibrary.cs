using System;
using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    using KarpysUtils;

    public class LineRendererLibrary: SingletonMonoBehavior<LineRendererLibrary>
    {
        [SerializeField] private GenericLibrary<LineRendererType,LineRendererParameters> m_LineRendererLibrary = null;

        private void Awake()
        {
            m_LineRendererLibrary.InitializeDictionary();
        }

        public LineRendererParameters GetViaKey(LineRendererType type)
        {
            return m_LineRendererLibrary.GetViaKey(type);
        }
    }


    [Serializable]
    public class LineRendererParameters
    {
        [SerializeField] private FxLineRendererAnimation m_StartAnimation = null;
        [SerializeField] private FxLineRendererAnimation m_TrailAnimation = null;
        [SerializeField] private FxLineRendererAnimation m_EndAnimation = null;

        public FxLineRendererAnimation StartAnimation => m_StartAnimation;
        public FxLineRendererAnimation TrailAnimation => m_TrailAnimation;
        public FxLineRendererAnimation EndAnimation => m_EndAnimation;
    }
    public enum LineRendererType
    {
        Lighning,
    }
}