using System;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Widget.ObjectPooling;
using Script.Data;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    public class GoldManager : SingletonMonoBehavior<GoldManager>
    {
        [SerializeField] private PlayerDataHolder m_PlayerDataHolder = null;
        [SerializeField] private GoldWorldHolder m_GoldHolder = null;
        [SerializeField] private int m_InitialSize = 10;

        private GameObjectPool<GoldWorldHolder> m_GoldPool = null;
        public static string GOLD_ICON = " <sprite name=\"GoldIcon\">";

        public Action OnGoldUpdated = null;
        private void Awake()
        {
            m_GoldPool = new GameObjectPool<GoldWorldHolder>(m_GoldHolder,transform, m_InitialSize,OnNewGoldHolder);
        }
        
        private void OnNewGoldHolder(GoldWorldHolder goldWorldHolder)
        {
            goldWorldHolder.Initialize(this);
        }

        public void SpawnGoldAmount(Vector3 position,Transform target,int goldCount,float goldValue)
        {
            goldValue /= goldCount;

            for (int i = 0; i < goldCount; i++)
            {
                m_GoldPool.Take().Launch(position,target,goldValue);
            }
        }

        public void ChangeGoldValue(float goldValue)
        {
            m_PlayerDataHolder.ChangeGoldValue(goldValue);
            OnGoldUpdated?.Invoke();
        }
        public void Return(GoldWorldHolder goldHolder)
        {
            m_GoldPool.Return(goldHolder);
        }

        public bool CanBuy(float goldPrice)
        {
            return goldPrice <= m_PlayerDataHolder.PlayerData.GoldCount;
        }
    }
}