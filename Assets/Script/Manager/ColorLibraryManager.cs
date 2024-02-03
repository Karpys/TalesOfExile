using System;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.Manager
{
    using KarpysUtils;

    [Serializable]
    public class DamageTypeColor
    {
        public SubDamageType DamageType = SubDamageType.Physical;
        public Color DamageColor = Color.white; 
    }
    public class ColorLibraryManager:SingletonMonoBehavior<ColorLibraryManager>
    {
        [SerializeField] private Color m_HealColor = Color.green;
        public DamageTypeColor[] DamageTypeColors = new DamageTypeColor[1];
    
        public Color GetDamageColor(SubDamageType damageType)
        {
            foreach (DamageTypeColor damegeTypeColor in DamageTypeColors)
            {
                if (damageType == damegeTypeColor.DamageType)
                {
                    return damegeTypeColor.DamageColor;
                }
            }

            return Color.white;
        }

        public Color GetHealColor()
        {
            return m_HealColor;
        }
    }
}