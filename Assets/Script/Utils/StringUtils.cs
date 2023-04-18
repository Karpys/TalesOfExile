using System;
using System.Reflection;
using UnityEngine;

public static class StringUtils
{
    //Change to string : Obsolete *:(//
    public static string[] ExtractParameterLessVariable(string name)
    {
        string[] parameters = name.Split(':');
        string[] variables = new string[parameters.Length-1];

        for (int i = 1; i < parameters.Length; i++)
        {
            variables[i - 1] = parameters[i];
        }

        return variables;
    }
    
    public static string[] Parameters(string parameter)
    {
        string[] parameters = parameter.Split(':');
        
        return parameters;
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
        Type classType = Type.GetType(className);
        if(classType == null)
            Debug.LogError("Class Name Dont exist " + className);
        return classType;
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
}
