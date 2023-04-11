using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Linq;

namespace Bresenhams
{
    // Author: Jason Morley (Source: http://www.morleydev.co.uk/blog/2010/11/18/generic-bresenhams-line-algorithm-in-visual-basic-net/)
    /// <summary>
    /// The Bresenham algorithm collection
    /// </summary>
    public static class Bresenhams
    {
        private static void Swap<T>(ref T lhs, ref T rhs) { T temp; temp = lhs; lhs = rhs; rhs = temp; }

        public static List<Vector2Int> GetPath(Vector2Int from, Vector2Int to)
        {
            return GetPath(from.x, from.y,to.x, to.y);
        }
        public static List<Vector2Int> GetPath(int x0, int y0, int x1, int y1)
        {
            Vector2Int startPath = new Vector2Int(x0, y0);
            List<Vector2Int> points = new List<Vector2Int>();

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep) { Swap<int>(ref x0, ref y0); Swap<int>(ref x1, ref y1); }
            if (x0 > x1) { Swap<int>(ref x0, ref x1); Swap<int>(ref y0, ref y1); }
            int dX = (x1 - x0), dY = Math.Abs(y1 - y0), err = (dX / 2), ystep = (y0 < y1 ? 1 : -1), y = y0;

            for (int x = x0; x <= x1; ++x)
            {
                int px = steep ? y : x;
                int py = steep ? x : y;
                points.Add(new Vector2Int(px, py));

                err = err - dY;
                if (err < 0) { y += ystep;  err += dX; }
            }

            points.Remove(startPath);
            return points;
        }
    }
}