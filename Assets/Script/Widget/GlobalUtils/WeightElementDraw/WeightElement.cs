using System;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    [Serializable]
    public class WeightElement<T>
    {
        [SerializeField] [Range(0,100)] private float m_Weight = 50;
        [SerializeField] private T m_Object = default;

        public float Weight => m_Weight;
        public T Object => m_Object;
    }
}