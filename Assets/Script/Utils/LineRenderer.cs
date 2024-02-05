using System.Collections.Generic;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.SpellFx;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    public static class LineRenderer
    {
        public static void LinePointsRenderer(List<Vector2Int> points, LineRendererType lineRendererType, float lineDuration,float additionalFadeDelay)
        {
            LineRendererParameters lineParameters = LineRendererLibrary.Instance.GetViaKey(lineRendererType);
            Transform mapDataTransform = MapData.Instance.transform;
            float lineDelay = 0f;

            Vector2 previousPointPosition = points[0];

            for (int i = 0; i < points.Count - 1; i++)
            {
                float lineAngle = SpriteUtils.GetRotateTowardPoint(previousPointPosition, new Vector2(points[i + 1].x,points[i + 1].y));
                float distance = Vector2.Distance(previousPointPosition, points[i + 1]);
                Vector2 normalizedDirection = (points[i + 1] - previousPointPosition);
                normalizedDirection.Normalize();

                int needed = (int) (distance + 0.25f);
                if (needed == 0)
                    needed = 1;

                Vector3 targetPosition = Vector3.zero;
            
                for (int j = 0; j < needed + 1; j++)
                {
                    targetPosition = previousPointPosition + normalizedDirection * j;

                    FxLineRendererAnimation lineRenderer = null;
                
                    if (j == needed)
                    {
                        lineRenderer = lineParameters.EndAnimation.TriggerFx(targetPosition,mapDataTransform) as FxLineRendererAnimation;
                    }
                    else if(j == 0)
                    {
                        lineRenderer = lineParameters.StartAnimation.TriggerFx(targetPosition,mapDataTransform) as FxLineRendererAnimation;
                    }
                    else
                    {
                        lineRenderer = lineParameters.TrailAnimation.TriggerFx(targetPosition,mapDataTransform) as FxLineRendererAnimation;
                    }
                
                    lineRenderer.LaunchLineRendererAnim(lineDelay,lineDuration,lineAngle);
                    lineDelay += additionalFadeDelay;
                }

                previousPointPosition = targetPosition;
            }
        }
    }
}
