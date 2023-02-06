using UnityEngine;

[System.Serializable]
public class DamageTypeColor
{
    public SubDamageType DamageType = SubDamageType.Physical;
    public Color DamageColor = Color.white; 
}
public class ColorLibraryManager:SingletonMonoBehavior<ColorLibraryManager>
{
    public DamageTypeColor[] DamageTypeColors = new DamageTypeColor[1];
    
    public Color GetDamageColor(SubDamageType damageType)
    {
        for (int i = 0; i < DamageTypeColors.Length; i++)
        {
            if (damageType == DamageTypeColors[i].DamageType)
            {
                return DamageTypeColors[i].DamageColor;
            }
        }
        
        return Color.white;
    }
}
