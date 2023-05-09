using System.Collections.Generic;
using UnityEngine;

public class MineSpellTrigger : DamageSpellTrigger
{
    private bool m_HasTrigger = false;
    private List<SpellAnimationActivator> m_SpellAnimations = new List<SpellAnimationActivator>(); 

    public MineSpellTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {}

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo)
    {
        base.Trigger(spellData, spellTiles,castInfo);

        if (m_HasTrigger)
        {
            MineExplosion();
            spellData.AttachedEntity.ForceDeath();
            return;
        }
        
        m_HasTrigger = true;
    }
    
    protected override void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
    {
        if(m_HasTrigger)
            return;
        base.TileHit(tilePosition, spellData);
    }

    protected override void TriggerTileHitFx(Vector3 tilePosition, Transform transform, params object[] args)
    {
        m_SpellAnimations.Add(TileHitAnimation.TriggerFx(tilePosition,m_AttachedSpell.AttachedEntity.transform) as SpellAnimationActivator);
    }
    
    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup,
        Vector2Int origin, CastInfo castInfo)
    {
        if(m_HasTrigger)
            base.EntityHit(entity, spellData, targetGroup, origin,castInfo);
    }
    
    private void MineExplosion()
    {
        foreach (SpellAnimationActivator activator in m_SpellAnimations)
        {
            activator.Activate();
        }
    }
}