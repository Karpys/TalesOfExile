using UnityEngine;

public static class ColorUtils
{
    public static void Alpha(this SpriteRenderer renderer,float a)
    {
        Color col = renderer.color;
        renderer.color = new Color(col.r, col.g, col.b, a);
    }
    
    public static void RoundColor(this SpriteRenderer renderer,float a)
    {
        renderer.color = new Color(a, a, a,renderer.color.a);
    }
}
