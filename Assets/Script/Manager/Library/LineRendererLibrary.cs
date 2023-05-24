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
        [SerializeField] private LineRendererAnimation m_StartAnimation = null;
        [SerializeField] private LineRendererAnimation m_TrailAnimation = null;
        [SerializeField] private LineRendererAnimation m_EndAnimation = null;

        public LineRendererAnimation StartAnimation => m_StartAnimation;
        public LineRendererAnimation TrailAnimation => m_TrailAnimation;
        public LineRendererAnimation EndAnimation => m_EndAnimation;
    }
    public enum LineRendererType
    {
        Lighning,
    }
}