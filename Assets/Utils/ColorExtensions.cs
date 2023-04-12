using UnityEngine;

public static class ColorExtensions
{
    public static Vector3 rgb(this Color color)
    {
        return new Vector3(color.r, color.g, color.b);
    }
    
    public static Color setAlpha(this Color color,float alpha)
    {
        return new Color(color.r,color.g,color.b,alpha);
    }

    public static Color ToColor(this Vector3 color)
    {
        return new Color(color.x,color.y,color.z);
    }
    
    public static Vector4 rgba(this Color color)
    {
        return new Vector4(color.r, color.g, color.b,color.a);
    }

    public static Color ToColor(this Vector4 color)
    {
        return new Color(color.x,color.y,color.z,color.w);
    }
}
