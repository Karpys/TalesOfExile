using System;
using Object = UnityEngine.Object;

namespace KarpysDev.Script.Utils
{
    /// <summary>
    /// Use It when called multiple times and weight element count dont change
    /// </summary>
    [Serializable]
    public class StaticWeightElementDraw<T> : WeightElementDraw<T> where T : class
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
}