using UnityEngine;

[System.Serializable]
public class WeightElementDraw<T> where T:Object
{
    [SerializeField] private WeightElement<T>[] m_WeightElement = null;
    
    public T Draw()
    {
        float totalWeight = 0;

        foreach (WeightElement<T> element in m_WeightElement)
        {
            totalWeight += element.Weight;
        }

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
        
        return (T)m_WeightElement[elementId].Object;
    }
}