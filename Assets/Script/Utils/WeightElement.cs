using System;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class WeightElement<T> where T : Object
{
    [SerializeField] [Range(0,100)] private float m_Weight = 50;
    [SerializeField] private T m_Object = null;

    public float Weight => m_Weight;
    public Object Object => m_Object;
}