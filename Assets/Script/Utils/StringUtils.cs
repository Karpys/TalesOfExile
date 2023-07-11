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
    public static class StringUtils
    {
        private static Assembly currentAssembly = null;

        private static Dictionary<string, Type> classDictionary = null;

        static StringUtils()
        {
            currentAssembly = AppDomain.CurrentDomain.GetAssemblies().First(a => a.FullName == "Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            
            classDictionary = new Dictionary<string, Type>();

            foreach (Type type in currentAssembly.GetTypes())
            {
                Debug.Log(type.Name);
                if(classDictionary.ContainsKey(type.Name))
                    continue;
                classDictionary.Add(type.Name,type);
            }
        }
        public static object[] StringsToObjects(string[] data)
        {
            object[] objects = new object[data.Length];

            for (int i = 0; i < data.Length; i++)
            {
                objects[i] = data[i];
            }

            return objects;
        }

        public static Type GetTypeViaClassName(string className)
        {
            if(classDictionary.TryGetValue(className,out var type))
            {
                return type;
            }

            return Type.GetType(className);
        }

        public static void GetConstructorsFields(Type targetClass,out string[] fieldsName,out FieldValue[] fieldsValues,int constructor,int ignoreParamCount)
        {
            if (targetClass.GetConstructors().Length <= constructor)
            {
                fieldsName = Array.Empty<string>();
                fieldsValues = Array.Empty<FieldValue>();
                return;
            }
                
            ConstructorInfo constructorInfo = targetClass.GetConstructors()[constructor];
            FieldValue fieldConstructor = new FieldValue(FieldType.Empty,"empty");
        
            fieldsName = new string[constructorInfo.GetParameters().Length - ignoreParamCount];
            fieldsValues = new FieldValue[fieldsName.Length];
            ParameterInfo[] infos = constructorInfo.GetParameters();
            
            for (var i = 0; i < infos.Length - ignoreParamCount; i++)
            {
                ParameterInfo parameterInfo = infos[i + ignoreParamCount];
                fieldsName[i] = char.ToUpper(parameterInfo.Name[0]) + parameterInfo.Name.Substring(1);//+ " " + parameterInfo.ParameterType;
                fieldsValues[i] = fieldConstructor.GetField(parameterInfo.ParameterType.ToString());
            }
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
    }
}
