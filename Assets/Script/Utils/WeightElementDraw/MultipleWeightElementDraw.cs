using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarpysDev.Script.Utils
{
    [Serializable]
    public class MultipleWeightElementDraw<T> : WeightElementDraw<T> where T : class
    {
        public List<T> MultipleDraw(int drawCount)
        {
            List<WeightElement<T>> weightsElement = m_WeightElement.ToList();
            List<T> elementDrawn = new List<T>();

            for (int i = 0; i < drawCount; i++)
            {
                if (weightsElement.Count == 0)
                {
                    Debug.LogError("Exceed multiple element drawn");   
                    break;
                }
            
                float totalWeight = GetElementsWeight(weightsElement);
                float drawWeight = Random.Range(0f, totalWeight);
                int elementId = 0;
                float currentWeight = 0;
            
                while (elementId < weightsElement.Count - 1)
                {
                    currentWeight += weightsElement[elementId].Weight;
                    if (drawWeight < currentWeight)
                    {
                        break;
                    }
                    else
                    {
                        elementId += 1;
                    }
                }
            
                elementDrawn.Add(weightsElement[elementId].Object);
                weightsElement.Remove(weightsElement[elementId]);
            }

            return elementDrawn;
        }

        private float GetElementsWeight(List<WeightElement<T>> elements)
        {
            float totalWeight = 0;
        
            foreach (WeightElement<T> element in elements)
            {
                totalWeight += element.Weight;
            }

            return totalWeight;
        }
    }
}