using UnityEngine;

namespace KarpysDev.Script.Spell
{
    [CreateAssetMenu(menuName = "Spell Scaler/ Cooldown Reduction", fileName = "CooldownSpellLevelScaler", order = 0)]
    public class CooldownSpellLevelScalerScriptable : BaseSpellLevelScalerScriptable
    {
        [SerializeField] private AnimationCurve m_ScaleCurve = null;
        [SerializeField] private Vector2Int m_CooldownReductionMinMax = Vector2Int.zero;


        public override ILevelScaler GetBaseSpellLevelScaler()
        {
            return new CooldownSpellLevelScaler(this);
        }

        public int Evaluate(float ratio)
        {
            return (int)Mathf.LerpUnclamped(m_CooldownReductionMinMax.x,m_CooldownReductionMinMax.y,m_ScaleCurve.Evaluate(ratio));
        }
    }
}