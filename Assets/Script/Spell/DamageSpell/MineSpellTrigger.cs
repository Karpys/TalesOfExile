using System.Collections.Generic;
using UnityEngine;

public class MineSpellTrigger : DamageSpellTrigger
{
    private bool m_HasTrigger = false;
    private List<SpellAnimationActivator> m_SpellAnimations = new List<SpellAnimationActivator>(); 

    public MineSpellTrigger(DamageSpellScriptable damageSpellData) : base(damageSpellData)
    {}

    public override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles)
    {
        base.Trigger(spellData, spellTiles);

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
    
    protected override void TriggerTileHitFx(SpellAnimation tileHitAnim,Vector2Int tilePosition)
    {
        m_SpellAnimations.Add(tileHitAnim.TriggerFx(MapData.Instance.GetTilePosition(tilePosition),m_AttachedSpell.AttachedEntity.transform) as SpellAnimationActivator);
    }

    protected override void EntityHit(BoardEntity entity, TriggerSpellData spellData, EntityGroup targetGroup, Vector2Int origin)
    {
        if(m_HasTrigger)
            base.EntityHit(entity, spellData, targetGroup, origin);
    }
    
    private void MineExplosion()
    {
        foreach (SpellAnimationActivator activator in m_SpellAnimations)
        {
            activator.Activate();
        }
    }
}