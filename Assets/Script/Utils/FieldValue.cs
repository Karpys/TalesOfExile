using System;
using UnityEngine;

[System.Serializable]
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
                return new FieldValue(FieldType.Float, "0.5");
            case "System.String":
                return new FieldValue(FieldType.String, "Empty");
            default:
                //Try get enum//
                System.Type enumType = System.Type.GetType(type);
                if (enumType != null)
                {
                    return new EnumFieldValue(FieldType.Enum, "0", type);
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
                return int.Parse(Value);
            case FieldType.Float:
                return float.Parse(Value);
            case FieldType.Enum:
                EnumFieldValue enumFieldValue = this as EnumFieldValue;
                return Enum.Parse(enumFieldValue.EnumType, Value);
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
        EnumType = System.Type.GetType(enumType);
    }
}

public enum FieldType
{
    Int,
    Float,
    String,
    Enum,
    Empty,
}