using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Spell.DamageSpell;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    using Entities;

    public static class StringUtils
    {
        public static object[] StringsToObjects(string[] data)
        {
            object[] objects = new object[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                objects[i] = data[i];
            }

            return objects;
        }

        public static string[] ToSingleArray(this string value)
        {
            string[] array = new string[1];
            array[0] = value;
            return array;
        }

        public static int ToInt(this string value)
        {
            return int.Parse(value);
        }

        public static float ToFloat(this string value)
        {
            return float.Parse(value,CultureInfo.InvariantCulture);
        }

        public static string GetDescription(string baseDescription,string[] dynamicValues)
        {
            for (int i = 0; i < dynamicValues.Length; i++)
            {
                baseDescription = baseDescription.Replace("&" + i, dynamicValues[i]);
            }

            baseDescription = ReplaceColorTag(baseDescription);

            if (baseDescription == "")
            {
                baseDescription = "Description not implemented";
            }

            return baseDescription;
        }

        public static string ToColorString(this SubDamageType subDamageType)
        {
            return "<color=#" + ColorLibraryManager.Instance.GetDamageColor(subDamageType).ToColorString() + ">";
        }

        public static string ToColorString(this Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        public static string ToColorTag(this string colorTag)
        {
            return "<color=#" + colorTag + ">";
        }

        //Todo : Find better way to acheive that
        private static string ReplaceColorTag(string baseDescription)
        {
            baseDescription = baseDescription.Replace("FIRE", ToColorString(SubDamageType.Fire));
            baseDescription = baseDescription.Replace("COLD", ToColorString(SubDamageType.Cold));
            baseDescription = baseDescription.Replace("PHYSICAL", ToColorString(SubDamageType.Physical));
            baseDescription = baseDescription.Replace("LIGHTNING", ToColorString(SubDamageType.Lightning));
            baseDescription = baseDescription.Replace("ENDCOLOR","</color>");
            return baseDescription;
        }

        public static string ToWeaponDescription(this List<DamageSource> groupSource, WeaponTarget target,
            BoardEntity entity)
        {
            string weaponDamageDescription = "";
            int weaponCount = 0;

            switch (target)
            {
                case WeaponTarget.AllWeapons:
                    weaponCount = entity.EntityStats.WeaponItems.Count;
                    break;
                case WeaponTarget.MainWeapon:
                    weaponCount = 1;
                    break;
                case WeaponTarget.OffWeapon:
                    weaponCount = 1;
                    break;
                case WeaponTarget.Unarmed:
                    weaponCount = 1;
                    break;
                default:
                    weaponCount = 1;
                    break;
            }

            for (int i = 0; i < weaponCount; i++)
            {
                if (i >= groupSource.Count)
                {
                    Debug.LogError("More weapon than group source ?");   
                    break;
                }

                if (i != 0)
                    weaponDamageDescription += " + ";
                weaponDamageDescription += groupSource[i].ToDescription();
            }

            return weaponDamageDescription;
        }
    }
}
