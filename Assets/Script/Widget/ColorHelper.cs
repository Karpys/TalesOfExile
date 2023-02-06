
using System.Collections.Generic;
using UnityEngine;

public static class ColorHelper
{
    public static Color GetDamageBlendColor(Dictionary<SubDamageType, DamageSource> damageSources)
    {
        Vector3 totalColor = Vector3.zero;

        foreach (KeyValuePair<SubDamageType,DamageSource> damageSource in damageSources)
        {
            Color color = ColorLibraryManager.Instance.GetDamageColor(damageSource.Key);
            totalColor.x += color.r;
            totalColor.y += color.g;
            totalColor.z += color.b;
        }

        totalColor /= damageSources.Count;
        
        return new Color(totalColor.x,totalColor.y,totalColor.z,1);
    }
}
