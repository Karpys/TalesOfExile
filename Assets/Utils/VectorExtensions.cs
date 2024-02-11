using System;
using System.Collections;
using System.Collections.Generic;
using KarpysDev.KarpysUtils;
using KarpysDev.Script.Utils;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 xy(this Vector3 vec3)
    {
        return new Vector2(vec3.x, vec3.y);
    }

    public static Vector3 Vec2ToVec3(this Vector2 vec2)
    {
        return new Vector3(vec2.x, vec2.y, 0);
    }

    public static Vector3 MoveTowards(Vector3 current,Vector3 target, float speed,out bool onPoint)
    {
        onPoint = false;
        Vector3 nextPosition = (target - current).normalized * speed* Time.deltaTime;
        nextPosition = current + nextPosition;

        if ((target - nextPosition).normalized != (target - current).normalized)
        {
            nextPosition = target;
            onPoint = true;
        }

        return nextPosition;
    }

    public static Vector3 MoveTowardsVelocity(Vector3 current, Vector3 target, float speed, out bool onPoint)
    {
        onPoint = false;
        Vector3 nextPosition = (target - current).normalized * speed;

        if ((target - nextPosition).normalized != (target - current).normalized)
        {
            onPoint = true;
        }

        return nextPosition;
    }

    public static Vector2Int Project(this Vector2Int vec, int targetRange)
    {
        int currentRange = DistanceUtils.GetSquareDistance(Vector2Int.zero, vec);

        if (currentRange == 0)
        {
            vec.y = 1;
            currentRange = 1;
        }
        float needed = (float)targetRange / currentRange;
        vec = new Vector2Int(Mathf.CeilToInt(vec.x * needed),Mathf.CeilToInt(vec.y * needed));
        return vec;
    }
}
