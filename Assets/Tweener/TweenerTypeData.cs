using System;
using System.Collections;
using UnityEngine;


namespace TweenCustom
{

    [Serializable]
    public enum Ease
    {
        LINEAR,
        EASE_IN_SIN,
        EASE_OUT_SIN,
        EASE_OUT_ELASTIC,
    }

    public enum TweenMode
    {
        NORMAL,
        ADDITIVE,
    }

}