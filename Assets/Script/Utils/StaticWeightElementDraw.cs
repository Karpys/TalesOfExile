using UnityEngine;

/// <summary>
/// Use It when called multiple times and weight element count dont change
/// </summary>
[System.Serializable]
public class StaticWeightElementDraw<T> : WeightElementDraw<T> where T : Object
{
    private float m_CachedWeight = -1;
    protected override float GetTotalWeight()
    {
        if (m_CachedWeight != -1)
            return m_CachedWeight;
        
        m_CachedWeight = base.GetTotalWeight();
        return m_CachedWeight;
    }
}