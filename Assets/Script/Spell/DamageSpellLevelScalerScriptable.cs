namespace KarpysDev.Script.Spell
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Spell Scaler/ Damage Amplify", fileName = "DamageAmplifier", order = 0)]

    public class DamageSpellLevelScalerScriptable : BaseSpellLevelScalerScriptable
    {
        [SerializeField] private AnimationCurve m_ScaleCurve = null;
        [SerializeField] private Vector2 m_DamageAmplifier = Vector2Int.zero;

        public override ILevelScaler GetBaseSpellLevelScaler()
        {
            return new DamageSpellLevelScaler(this);
        }

        public float Evaluate(float ratio)
        {
            return Mathf.LerpUnclamped(m_DamageAmplifier.x,m_DamageAmplifier.y,m_ScaleCurve.Evaluate(ratio));
        }
    }
}