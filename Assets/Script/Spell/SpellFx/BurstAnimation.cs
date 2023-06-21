using System.Collections;

namespace KarpysDev.Script.Spell.SpellFx
{
    public abstract class BurstAnimation : SpellAnimation
    {
        protected virtual void Start()
        {
            Animate();
        }
    }
}