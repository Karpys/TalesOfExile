using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities;

namespace KarpysDev.Script.Spell
{
    public abstract class BaseSpellTrigger
    {
        protected float m_SpellAnimDelay = 0;
        protected int m_SpellPriority = 0;

        protected SpellData m_AttachedSpell = null;
        protected float m_SpellEfficiency = 1;
        
        public Action<CastInfo> OnCastSpell = null;
        
        public int SpellPriority => GetSpellPriority();
        public SpellData SpellData => m_AttachedSpell;
        protected virtual int GetSpellPriority()
        {
            return m_SpellPriority;
        }
    
        public virtual void SetAttachedSpell(SpellData spellData,int priority)
        {
            m_SpellPriority = priority;
            m_AttachedSpell = spellData;
        }

        public virtual void CastSpell(TriggerSpellData spellData,SpellTiles spellTiles,float efficiency = 1)
        {
            CastInfo castInfo = GetCastInfo(spellData);
        
            TriggerSpell(spellData,spellTiles,castInfo,efficiency);
            OnCastSpell?.Invoke(castInfo);
        }

        protected virtual CastInfo GetCastInfo(TriggerSpellData spellData)
        {
            if (OnCastSpell != null)
            {
                return new CastInfo(spellData);
            }

            return null;
        }

        public void TriggerSpell(TriggerSpellData spellData, SpellTiles spellTiles, CastInfo castInfo,
            float efficiency = 1)
        {
            m_SpellEfficiency = efficiency;
            Trigger(spellData,spellTiles,castInfo,m_SpellEfficiency);
        }

        protected abstract void Trigger(TriggerSpellData spellData,SpellTiles spellTiles,CastInfo castInfo,float efficiency = 1);

        public abstract void ComputeSpellData(BoardEntity entity);

        public abstract string[] GetDescriptionParts();
    }
    
    public class CastInfo
    {
        private SpellData m_SpellCasted = null;
        public SpellData SpellCasted => m_SpellCasted;

        public CastInfo(SpellData spellCasted)
        {
            m_SpellCasted = spellCasted;
        }
    
        public virtual void AddHitEntity(BoardEntity boardEntity)
        {
        }
    }

    public class DamageCastInfo:CastInfo
    {
        private List<BoardEntity> m_HitEntity = new List<BoardEntity>();
        public List<BoardEntity> HitEntity => m_HitEntity;

        public override void AddHitEntity(BoardEntity boardEntity)
        {
            m_HitEntity.Add(boardEntity);
        }

        public DamageCastInfo(SpellData spellCasted) : base(spellCasted)
        {}
    }
}