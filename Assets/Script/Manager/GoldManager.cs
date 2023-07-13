using System;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget.ObjectPooling;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class GoldManager : SingletonMonoBehavior<GoldManager>
    {
        [SerializeField] private GoldWorldHolder m_GoldHolder = null;
        [SerializeField] private int m_InitialSize = 10;

        private GameObjectPool<GoldWorldHolder> m_GoldPool = null;

        private void Awake()
        {
            m_GoldPool = new GameObjectPool<GoldWorldHolder>(m_GoldHolder,transform, m_InitialSize,OnNewGoldHolder);
        }
        
        private void OnNewGoldHolder(GoldWorldHolder goldWorldHolder)
        {
            goldWorldHolder.Initialize(this);
        }

        public void SpawnGold(Vector3 position,Transform target)
        {
            m_GoldPool.Take().Animate(position,target);
        }

        public void Return(GoldWorldHolder goldHolder)
        {
            m_GoldPool.Return(goldHolder);
        }
    }
}