
using System;
using System.Collections.Generic;
using UnityEngine;

public static class LinePath
{
    public static NeighbourType NeighbourType = NeighbourType.Square;
    
    public static List<Vector2Int> GetPathTile(Vector2Int from, Vector2Int to,NeighbourType neighbourType)
    {
        NeighbourType = neighbourType;
        //Use Brensenhams found in rogue bassin for squareNeightboursType//
        if (NeighbourType == NeighbourType.Square)
        {
            return Bresenhams.Bresenhams.GetPath(from, to);
        }
        
        //Use Custom Algo for cross NeightboursType//
        
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int fromPosition = from;
        Vector2Int toPosition = to;

        Vector2Int Vec = toPosition - fromPosition;
        

        if (Mathf.Abs(Vec.y) > Mathf.Abs(Vec.x))
        {
            path = AroundY(path,fromPosition, toPosition, Vec);
        }
        else if (Mathf.Abs(Vec.x) > Mathf.Abs(Vec.y))
        {
            path = AroundX(path,fromPosition, toPosition, Vec);
        }
        else
        {
            path = AroundDiagonal(path, fromPosition, toPosition, Vec);
        }

        return path;
    }

    private static List<Vector2Int> AroundDiagonal(List<Vector2Int> path, Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec)
    {
        Vector2Int dir = GetDirection(Vec);
        
        while (fromPosition != toPosition)
        {
            if(NeighbourType == NeighbourType.Cross)
                path.Add(new Vector2Int(fromPosition.x,fromPosition.y + dir.y));
            
            fromPosition += dir;
            path.Add(fromPosition);
        }

        return path;
    }

    private static List<Vector2Int> AroundY(List<Vector2Int> path,Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec)
    {
        float currentRatio = 0.49f;
        Vector2Int dir = GetDirection(Vec);
        float ratioPerStep = (float)Math.Abs(Vec.x) / Math.Abs(Vec.y);

        while (fromPosition != toPosition)
        {
            currentRatio += ratioPerStep;

            
            if (currentRatio >= 1)
            {
                if(NeighbourType == NeighbourType.Cross)
                    path.Add(new Vector2Int(fromPosition.x + dir.x, fromPosition.y));
                //Move on X Axis//
                fromPosition += dir;
                currentRatio -= 1;
            }
            else
            {
                fromPosition += new Vector2Int(0,dir.y);
            }
            path.Add(fromPosition);
        }

        return path;
    }

    private static List<Vector2Int> AroundX(List<Vector2Int> path, Vector2Int fromPosition, Vector2Int toPosition, Vector2Int Vec)
    {
        float currentRatio = 0.49f;
        Vector2Int dir = GetDirection(Vec);
        float ratioPerStep =  (float)Math.Abs(Vec.y) / Math.Abs(Vec.x);

        while (fromPosition != toPosition)
        {
            currentRatio += ratioPerStep;
            if (currentRatio >= 1)
            {
                if (NeighbourType == NeighbourType.Cross)
                    path.Add(new Vector2Int(fromPosition.x, fromPosition.y + dir.y));
                //Move on Y Axis//
                fromPosition += dir;
                currentRatio -= 1;
            }
            else
            {
                fromPosition += new Vector2Int(dir.x,0);
            }
            path.Add(fromPosition);
        }

        return path;
    }

    private static Vector2Int GetDirection(Vector2Int Vec)
    {
        return new Vector2Int((int)Mathf.Sign(Vec.x), (int)Mathf.Sign(Vec.y));
    }
}
