using UnityEngine;

[System.Serializable]
public class WeightEnumDraw<T> where T:struct
{
    [SerializeField] private WeightEnum<T>[] m_WeightEnum = null;

    public T Draw()
    {
        float totalWeight = GetTotalWeight();
        float drawWeight = Random.Range(0f, totalWeight);
        int elementId = 0;
        float currentWeight = 0;
        
        while (elementId < m_WeightEnum.Length - 1)
        {
            currentWeight += m_WeightEnum[elementId].Weight;
            if (drawWeight < currentWeight)
            {
                break;
            }
            else
            {
                elementId += 1;
            }
        }
        
        return m_WeightEnum[elementId].Enum;
    }

    protected virtual float GetTotalWeight()
    {
        float totalWeight = 0;
        
        foreach (WeightEnum<T> element in m_WeightEnum)
        {
            totalWeight += element.Weight;
        }

        return totalWeight;
    }
}