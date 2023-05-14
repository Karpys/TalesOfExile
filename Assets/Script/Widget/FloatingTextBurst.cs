using System.Collections;
using System.Collections.Generic;
using TMPro;
using TweenCustom;
using UnityEngine;

[System.Serializable]
public class TweenParam
{
    public Transform Target = null;
    public AnimationCurve Curve = null;
    public float Duration = 1f;
}
public class FloatingTextBurst : MonoBehaviour
{
    [SerializeField] private TMP_Text m_Text = null;
    [SerializeField] private float m_RangeRandom = 0.5f;
    [SerializeField] private TweenParam m_XAlignement = null;
    [SerializeField] private TweenParam m_YEndAlignement = null;

    private bool fading = false;
    public void LaunchFloat(float damageValue,Color? color = null,float triggerDelay = 0f)
    {
        IEnumerator ILaunchFloat(float delay)
        {
            yield return new WaitForSeconds(delay + triggerDelay);
            
            Color targetColor = color ?? Color.white;
            m_Text.color = targetColor;
            m_XAlignement.Target.localPosition = new Vector3(Random.Range(-m_RangeRandom,m_RangeRandom), 0, 0);
            m_Text.text = (int)damageValue + "";
            transform.DoMoveX(m_XAlignement.Target.position.x, m_XAlignement.Duration).SetCurve(m_XAlignement.Curve);
            transform.DoMoveY(m_YEndAlignement.Target.position.y, m_YEndAlignement.Duration).SetCurve(m_YEndAlignement.Curve);
            Invoke("LaunchFade",m_YEndAlignement.Duration * 0.8f);
        }

        StartCoroutine(ILaunchFloat(Random.Range(0, 0.15f)));
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
