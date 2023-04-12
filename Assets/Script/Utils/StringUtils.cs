using System;
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
}
