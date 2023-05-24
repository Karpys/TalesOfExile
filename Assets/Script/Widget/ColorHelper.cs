using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Widget
{
    public static class ColorHelper
    {
        private static readonly Color MAX_LIFE_COLOR = new Color(0.098f,0.568f,0,1);
        private static readonly Color MIN_LIFE_COLOR = new Color(0.568f,0,0,1);
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

        public static List<Color> GetColorInSprite(Sprite sprite)
        {
            Texture2D tex = sprite.texture;
            Color[] colors = tex.GetPixels();

            return colors.Distinct().ToList();
        }

        public static Color GetLifeLerp(float ratio)
        {
            return Color.Lerp(MIN_LIFE_COLOR, MAX_LIFE_COLOR, ratio);
        }
    }
}
