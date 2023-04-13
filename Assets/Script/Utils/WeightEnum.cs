using UnityEngine;

[System.Serializable]
public class WeightEnum<T> where T : struct
{
    [SerializeField] [Range(0,100)] private float m_Weight = 50;
    [SerializeField] private T m_Enum;

    public float Weight => m_Weight;
    public T Enum => m_Enum;
}