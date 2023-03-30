using UnityEngine;

public static class DistanceUtils
{
    public static int GetSquareDistance(Vector2Int to, Vector2Int from)
    {
        int XDiff = Mathf.Abs(to.x - from.x);
        int YDiff = Mathf.Abs(to.y - from.y);

        if (XDiff > YDiff)
            return XDiff;

        return YDiff;
    }
}