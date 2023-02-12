using System;
using UnityEngine;
using UnityEngine.UI;

namespace Script.UI.Jauge
{
    public abstract class BaseJauge : MonoBehaviour
    {
        [SerializeField] private Image m_FillJauge = null;

        protected virtual void UpdateJaugeFillValue(float currentValue, float maxValue)
        {
            m_FillJauge.fillAmount = currentValue / maxValue;
        }
    }
}