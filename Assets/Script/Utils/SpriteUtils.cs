using TweenCustom;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    public static class SpriteUtils
    {
        public static void RotateTowardPoint(Vector3 originPosition,Vector3 point,Transform rotateVisual,float offset = 0)
        {
            Vector3 direction = point - originPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
            rotateVisual.transform.rotation = Quaternion.Euler(new Vector3(0,0,angle + offset));
        }
    
        public static float GetRotateTowardPoint(Vector3 originPosition,Vector3 point)
        {
            Vector3 direction = point - originPosition;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    
        public static float GetRotateTowardPoint(Vector2Int originPosition,Vector2Int point)
        {
            Vector2Int direction = point - originPosition;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }

        public static BaseTween FadeAndDestroy(this SpriteRenderer renderer, Color targetColor, float duration,GameObject attachedGameObject)
        {
            return renderer.DoColor(targetColor, duration).OnComplete(() => Object.Destroy(attachedGameObject));
        }
    }
}
