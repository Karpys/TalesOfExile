using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


/// <summary>
/// Use StaticWeightElementDraw if the size never change
/// </summary>
[System.Serializable]
public class WeightElementDraw<T> where T:class
{
    [SerializeField] protected WeightElement<T>[] m_WeightElement = null;
    public T Draw()
    {
        float totalWeight = GetTotalWeight();
        float drawWeight = Random.Range(0f, totalWeight);
        int elementId = 0;
        float currentWeight = 0;
        
        while (elementId < m_WeightElement.Length - 1)
        {
            currentWeight += m_WeightElement[elementId].Weight;
            if (drawWeight < currentWeight)
            {
                break;
            }
            else
            {
                elementId += 1;
            }
        }
        
        return m_WeightElement[elementId].Object;
    }

    protected virtual float GetTotalWeight()
    {
        float totalWeight = 0;
        
        foreach (WeightElement<T> element in m_WeightElement)
        {
            totalWeight += element.Weight;
        }

        return totalWeight;
    }
}