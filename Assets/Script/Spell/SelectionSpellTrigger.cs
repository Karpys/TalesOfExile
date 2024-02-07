using System;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Map_Related;
using KarpysDev.Script.Spell.SpellFx;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.Spell
{
    public abstract class SelectionSpellTrigger:BaseSpellTrigger
    {
        protected SpellAnimation OnHitAnimation = null;
        protected SpellAnimation TileHitAnimation = null;
        protected SpellAnimation CenterOriginAnimation = null;
        
        protected bool ShouldTriggerOriginAnimation = false;
        protected bool ShouldTriggerOnHitAnimation = false;
        protected bool ShouldTriggerTileHitAnimation = false;

        public SelectionSpellTrigger(BaseSpellTriggerScriptable baseScriptable)
        {
            OnHitAnimation = baseScriptable.OnHitAnimation;
            TileHitAnimation = baseScriptable.OnTileHitAnimation;
            CenterOriginAnimation = baseScriptable.CenterOriginAnimation;

            if (CenterOriginAnimation != null)
                ShouldTriggerOriginAnimation = true;
            if (OnHitAnimation != null)
                ShouldTriggerOnHitAnimation = true;
            if (TileHitAnimation != null)
                ShouldTriggerTileHitAnimation = true;
        }

        protected virtual void TileHit(Vector2Int tilePosition, TriggerSpellData spellData)
        {
            if (ShouldTriggerTileHitAnimation)
            {
                if(TileHitAnimation.BaseSpellDelay > m_SpellAnimDelay)
                    m_SpellAnimDelay = TileHitAnimation.BaseSpellDelay;

                CreateTileHitFx(MapData.Instance.GetTilePosition(tilePosition),null);
            }
        }

        protected virtual SpellAnimation CreateTileHitFx(Vector3 tilePosition,Transform transform)
        {
            return TileHitAnimation.TriggerFx(tilePosition,transform);
        }
    
        protected virtual void EntityHit(BoardEntity entity, TriggerSpellData spellData,
            Vector2Int spellOrigin, CastInfo castInfo)
        {
            if (ShouldTriggerOnHitAnimation)
            {
                if(OnHitAnimation.BaseSpellDelay > m_SpellAnimDelay)
                    m_SpellAnimDelay = OnHitAnimation.BaseSpellDelay;

                CreateOnHitFx(entity.WorldPosition,null);
            }
        }

        protected virtual SpellAnimation CreateOnHitFx(Vector3 entityPosition,Transform transform)
        {
            return OnHitAnimation.TriggerFx(entityPosition,transform);
        }
        
        protected virtual void CenterOriginFx(Vector2Int originPosition)
        {
            if (ShouldTriggerOriginAnimation)
            {
                if(CenterOriginAnimation.BaseSpellDelay > m_SpellAnimDelay)
                    m_SpellAnimDelay = CenterOriginAnimation.BaseSpellDelay;

                CreateOriginFx(MapData.Instance.GetTilePosition(originPosition),null);
            }
        }
        
        protected virtual SpellAnimation CreateOriginFx(Vector3 originPosition,Transform transform)
        {
            return CenterOriginAnimation.TriggerFx(originPosition,transform);
        }

        protected override void Trigger(TriggerSpellData spellData, SpellTiles spellTiles,CastInfo castInfo,float efficiency = 1)
        {
            m_SpellAnimDelay = 0;
            EntityGroup targetGroup = GetEntityGroup(spellData);

            for (int i = 0; i < spellTiles.ActionTiles.Count; i++)
            {
                for (int j = 0; j < spellTiles.ActionTiles[i].Count; j++)
                {
                    TileHit(spellTiles.ActionTiles[i][j],spellData);
                
                    BoardEntity entityHit = MapData.Instance.GetEntityAt(spellTiles.ActionTiles[i][j],targetGroup);
                
                    if(!entityHit)
                        continue;
                
                    EntityHit(entityHit,spellData,spellTiles.OriginTiles[i],castInfo);
                    //Foreach Damage Sources//
                }
            }

            if (ShouldTriggerOriginAnimation)
            {
                foreach (Vector2Int originTile in spellTiles.OriginTiles)
                {
                    CenterOriginFx(originTile);
                }
            }
        
            if (spellData.AttachedEntity.EntityData.m_EntityGroup == EntityGroup.Friendly)
            {
                GameManager.Instance.FriendlyWaitTime = m_SpellAnimDelay;
            }
            else
            {
                GameManager.Instance.EnnemiesWaitTime = m_SpellAnimDelay;
            }
        }

        protected virtual EntityGroup GetEntityGroup(TriggerSpellData spellData)
        {
            return EntityHelper.GetInverseEntityGroup(spellData.AttachedEntity.EntityGroup);
        }

        public override void ComputeSpellData(BoardEntity entity)
        {
            OnCastSpell = null;
            entity.EntityEvent.OnRequestCastEvent?.Invoke(this);
        }

        public override string[] GetDescriptionParts()
        {
            return Array.Empty<string>();
        }
    }
}