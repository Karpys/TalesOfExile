using System;
using KarpysDev.Script.Utils;

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