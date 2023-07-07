using System.Collections.Generic;
using UnityEngine;

namespace KarpysDev.Script.Map_Related
{
    public class LightningStormPlaceable : SpellMapPlaceable
    {
        private List<Vector2Int> m_possibleCastPostion = new List<Vector2Int>();

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
            m_possibleCastPostion = possiblePosition;
        }
    }
}