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
}
