using UnityEngine;

namespace KarpysDev.Script.Spell
{
    [CreateAssetMenu(menuName = "Spell Scaler/ Cooldown Reduction", fileName = "CooldownSpellLevelScaler", order = 0)]
    public class CooldownSpellLevelScalerScriptable : BaseSpellLevelScalerScriptable
    {
        [SerializeField] private AnimationCurve m_ScaleCurve = null;
        [SerializeField] private Vector2Int m_CooldownReductionMinMax = Vector2Int.zero;

        public AnimationCurve ScaleCurve => m_ScaleCurve;
        public Vector2Int CooldownReductionMinMax => m_CooldownReductionMinMax;

        public override ILevelScaler GetBaseSpellLevelScaler()
        {
            return new CooldownSpellLevelScaler(this);
        }
    }
}