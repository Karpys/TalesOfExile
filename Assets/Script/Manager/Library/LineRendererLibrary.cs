using System;
using KarpysDev.Script.Spell.SpellFx;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Manager.Library
{
    public class LineRendererLibrary: SingletonMonoBehavior<LineRendererLibrary>
    {
        [SerializeField] private GenericLibrary<LineRendererParameters, LineRendererType> m_LineRendererLibrary = null;

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
        [SerializeField] private Fx_LineRendererAnimation m_StartAnimation = null;
        [SerializeField] private Fx_LineRendererAnimation m_TrailAnimation = null;
        [SerializeField] private Fx_LineRendererAnimation m_EndAnimation = null;

        public Fx_LineRendererAnimation StartAnimation => m_StartAnimation;
        public Fx_LineRendererAnimation TrailAnimation => m_TrailAnimation;
        public Fx_LineRendererAnimation EndAnimation => m_EndAnimation;
    }
    public enum LineRendererType
    {
        Lighning,
    }
}