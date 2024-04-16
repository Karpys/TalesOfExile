namespace KarpysDev.Script.Spell
{
    using UnityEngine;

    [CreateAssetMenu(menuName = "Spell Scaler/ Spell Activator", fileName = "SpellActivator", order = 0)]
    public class SpellActivatorLevelScalerScriptable : BaseSpellLevelScalerScriptable
    {
        [SerializeField] private float _levelRatioTreshold = 0f;
        public override ILevelScaler GetBaseSpellLevelScaler()
        {
            return new SpellActivatorLevelScaler(this);
        }

        public bool IsActive(float levelRatio)
        {
            return levelRatio >= _levelRatioTreshold;
        }
    }
}