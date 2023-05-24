using System;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    [Serializable]
    public class FieldValue
    {
        public FieldType Type = FieldType.Int;
        public string Value = String.Empty;

        public FieldValue GetField(string type)
        {
            switch (type)
            {
                case "System.Int32":
                    return new FieldValue(FieldType.Int, "0");
                case "System.Single":
                    return new FieldValue(FieldType.Float, "0");
                case "System.String":
                    return new FieldValue(FieldType.String, "Empty");
                case "System.Boolean":
                    return new FieldValue(FieldType.Bool, "False");
                default:
                    //Try get enum//
                    Type enumType = StringUtils.GetTypeViaClassName(type);
                    if (enumType != null)
                    {
                        return new EnumFieldValue(FieldType.Enum, "0 0", type);
                    }else
                    {
                        Debug.LogError("Type has not been implemented");
                        return null;
                    }
            }
        }

        public FieldValue(FieldType type, string defaultValue)
        {
            Type = type;
            Value = defaultValue;
        }

        public object GetValue()
        {
            switch (Type)
            {
                case FieldType.Int:
                    return Value.ToInt();
                case FieldType.Float:
                    return Value.ToFloat();
                case FieldType.Enum:
                    Type enumType = StringUtils.GetTypeViaClassName(Value.Split()[0]);
                    return Enum.Parse(enumType, Value.Split()[1]);
                case FieldType.Bool:
                    return bool.Parse(Value);
                case FieldType.String:
                    return Value;
                default:
                    return null;
            }
        }
    }

    public class EnumFieldValue : FieldValue
    {
        public Type EnumType;

        public EnumFieldValue(FieldType type, string defaultValue,string enumType) : base(type, defaultValue)
        {
            EnumType = StringUtils.GetTypeViaClassName(enumType);
        }
    }

    public enum FieldType
    {
        Int,
        Float,
        String,
        Enum,
        Empty,
        Bool,
    }
}