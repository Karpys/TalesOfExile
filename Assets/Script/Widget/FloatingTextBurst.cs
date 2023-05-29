using System;
using System.Collections;
using TMPro;
using TweenCustom;
using UnityEngine;
using Random = UnityEngine.Random;

namespace KarpysDev.Script.Widget
{
    [Serializable]
    public struct TweenParam
    {
        public Transform Target;
        public AnimationCurve Curve;
        public float Duration;
    }
    public class FloatingTextBurst : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_Text = null;
        [SerializeField] private float m_RangeRandom = 0.5f;
        [SerializeField] private TweenParam m_XAlignement;
        [SerializeField] private TweenParam m_YEndAlignement;

        private bool fading = false;
        public void LaunchFloat(float damageValue,Color? color = null,float triggerDelay = 0f)
        {
            float delay = triggerDelay + Random.Range(0f, 0.15f);
            Color targetColor = color ?? Color.white;
            m_Text.color = targetColor;
            m_XAlignement.Target.localPosition = new Vector3(Random.Range(-m_RangeRandom,m_RangeRandom), 0, 0);
            transform.DoMoveX(m_XAlignement.Target.position.x, m_XAlignement.Duration).SetCurve(m_XAlignement.Curve).SetDelay(delay);
            transform.DoMoveY(m_YEndAlignement.Target.position.y, m_YEndAlignement.Duration).SetCurve(m_YEndAlignement.Curve).SetDelay(delay).OnStart(
                () =>
                {
                    m_Text.text = (int)damageValue + "";
                    Invoke(nameof(LaunchFade),m_YEndAlignement.Duration * 0.8f);
                });
        }

        void Update()
        {
            if (fading)
            {
                Color textColor = m_Text.color;
                textColor.a -= Time.deltaTime;
                m_Text.color = textColor;
            
                if(m_Text.color.a <= 0)
                    Destroy(gameObject);
            }
        }

        private void LaunchFade()
        {
            fading = true;
        }
    }
}