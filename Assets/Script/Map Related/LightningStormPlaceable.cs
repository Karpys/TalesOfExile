using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class LightningStormPlaceable : SpellMapPlaceable
    {
        private Vector2Int[] m_possibleCastPostion = null;

        public void SetPossibleCastPosition(List<Vector2Int> possiblePosition)
        {
            for (int i = 0; i < possiblePosition.Count; i++)
            {
                if (!MapData.Instance.Map.InMapBounds(possiblePosition[i]))
                {
                    possiblePosition.RemoveAt(i);
                    i--;
                }
            }
            m_possibleCastPostion = possiblePosition.ToArray();
        }

        protected override Vector2Int GetCastPosition()
        {
            return m_possibleCastPostion[Random.Range(0, m_possibleCastPostion.Length)];
        }
    }
}