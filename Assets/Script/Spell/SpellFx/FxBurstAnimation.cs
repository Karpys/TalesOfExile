namespace KarpysDev.Script.Spell.SpellFx
{
    public abstract class FxBurstAnimation : SpellAnimation
    {
        protected virtual void Start()
        {
            Animate();
        }
    }
}