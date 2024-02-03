using System;
using KarpysDev.Script.Widget;
using KarpysDev.Script.Widget.ObjectPooling;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    using KarpysUtils;

    public class FloatingTextManager : SingletonMonoBehavior<FloatingTextManager>
    {
        [SerializeField] private FloatingTextBurst m_FloatingPrefab = null;
        [SerializeField] private int m_InitialSize = 100;

        private GameObjectPool<FloatingTextBurst> m_TextPool = null;

        private void Awake()
        {
            m_TextPool = new GameObjectPool<FloatingTextBurst>(m_FloatingPrefab,transform, m_InitialSize,OnAddNewFloatingText);
        }

        private void OnAddNewFloatingText(FloatingTextBurst floatingTextBurst)
        {
            floatingTextBurst.Initialize(this);
        }

        public void Return(FloatingTextBurst floatingTextBurst)
        {
            m_TextPool.Return(floatingTextBurst);
        }

        public void SpawnFloatingText(Vector3 position,string value,Color? color = null,float delay = 0f)
        {
            Color targetColor = color ?? Color.white;
            FloatingTextBurst text = m_TextPool.Take();
            text.transform.position = position;
            text.LaunchFloat(value,targetColor,delay);
        }
    }
}
