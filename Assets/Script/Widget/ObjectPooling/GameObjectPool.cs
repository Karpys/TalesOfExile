using System;
using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Widget.ObjectPooling
{
    public class GameObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> m_Pool;

        private Action<T> A_OnAddPoolObject = null;
        private Transform m_Parent = null;
        private T m_PoolObjectPrefab = null;

        public GameObjectPool(T poolPrefab,Transform parent,int initialSize,Action<T> onAddPoolObject)
        {
            m_Parent = parent;
            m_PoolObjectPrefab = poolPrefab;
            A_OnAddPoolObject = onAddPoolObject;
            m_Pool = new Queue<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                InitPoolObject();
            }
        }
        
        public GameObjectPool(T poolPrefab,Transform parent,int initialSize)
        {
            m_Parent = parent;
            m_PoolObjectPrefab = poolPrefab;
            m_Pool = new Queue<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                InitPoolObject();
            }
        }
        
        public T Take()
        {
            if (m_Pool.Count > 0)
            {
                T obj = m_Pool.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }
            else
            {
                return AddPoolObject();
            }
        }

        private T AddPoolObject()
        {
            T obj = GameObject.Instantiate(m_PoolObjectPrefab, m_Parent);
            A_OnAddPoolObject?.Invoke(obj);
            return obj;
        }

        private void InitPoolObject()
        {
            T obj = GameObject.Instantiate(m_PoolObjectPrefab, m_Parent); 
            A_OnAddPoolObject.Invoke(obj);
            obj.gameObject.SetActive(false);
            m_Pool.Enqueue(obj);
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            m_Pool.Enqueue(obj);
        }
    }
}