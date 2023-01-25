using UnityEngine;

namespace Script.Utils
{
    public static class BoardUtils
    {
        //Used for run away (for the moment)//
        public static Vector2Int GetOppositePosition(Vector2Int pivotPos, Vector2Int oppositeBasePos)
        {
            Vector2Int pivoToOppo = new Vector2Int(oppositeBasePos.x - pivotPos.x, oppositeBasePos.y - pivotPos.y);
            return pivotPos - pivoToOppo;
        }
    }
}