using System;
using UnityEngine;

namespace KarpysDev.Script.Utils
{
    [Serializable]
    public class FloatPercentageSlider
    {
        [Range(0, 100)] public float Value = 50f;
    }
}
