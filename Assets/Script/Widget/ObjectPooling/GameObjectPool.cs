using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Widget.ObjectPooling
{
    public class GameObjectPool<T> where T : MonoBehaviour
    {
        private readonly Queue<T> m_Pool;

        private Transform m_Parent = null;
        private T m_PoolObjectPrefab = null;

        public GameObjectPool(T poolPrefab,Transform parent,int initialSize)
        {
            m_Parent = parent;
            m_PoolObjectPrefab = poolPrefab;
            m_Pool = new Queue<T>(initialSize);
            for (int i = 0; i < initialSize; i++)
            {
                m_Pool.Enqueue(AddPoolObject());
            }
        }
        
        public T Take()
        {
            if (m_Pool.Count > 0)
            {
                T obj = m_Pool.Dequeue();
                obj.gameObject.SetActive(true);
                return m_Pool.Dequeue();
            }
            else
            {
                return AddPoolObject();
            }
        }

        private T AddPoolObject()
        {
            return GameObject.Instantiate(m_PoolObjectPrefab,m_Parent);
        }

        public void Return(T obj)
        {
            obj.gameObject.SetActive(false);
            m_Pool.Enqueue(obj);
        }
    }
}