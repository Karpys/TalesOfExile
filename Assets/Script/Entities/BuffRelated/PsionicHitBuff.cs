using System.Collections.Generic;
using System.Linq;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.Utils;
using UnityEngine;

namespace KarpysDev.Script.Entities.BuffRelated
{
    public class PsionicHitBuff : Buff
    {
        private int m_HitCount = 0;
        private Zone m_Zone = null;
        private SpellInfo m_SpellInfo = null;
        private TriggerSpellData m_TriggerSpellData = null;
        private List<Vector2Int> m_HitReceivers = new List<Vector2Int>();
        
        public PsionicHitBuff(BoardEntity caster, BoardEntity receiver, BuffType buffType, BuffGroup buffGroup, int cooldown, float buffValue, BuffCategory[] categories, bool ignoreFirstBehaveTurn
            ,SpellInfo spellInfo,int hitCount,ZoneType zoneType,int range) : base(caster, receiver, buffType, buffGroup, cooldown, buffValue, categories,ignoreFirstBehaveTurn)
        {
            m_SpellInfo = spellInfo;
            m_Zone = new Zone(zoneType, range);
            m_HitCount = hitCount;
        }

        public override void Apply()
        {
            m_TriggerSpellData = m_Receiver.RegisterSpell(m_SpellInfo);
            m_TriggerSpellData.SpellTrigger.ComputeSpellData(m_Receiver);
            
            m_Receiver.EntityEvent.OnRequestCastEvent += AddPsionicHit;
            m_Receiver.ComputeAllSpells();
            m_Receiver.EntityEvent.OnSpellRecompute += Recompute;
        }

        private void AddPsionicHit(BaseSpellTrigger spellTrigger)
        {
            if (spellTrigger.SpellData.Data.SpellGroups.Contains(SpellGroup.UnarmedHit))
            {
                spellTrigger.OnCastSpell += PsionicHitCallBack;
            }
        }

        private void PsionicHitCallBack(CastInfo castInfo)
        {
            if (!(castInfo is DamageCastInfo damageCastInfo)) return;

            if (GameManager.Instance.AddCallBackAction(PsionicHit))
            {
                if (damageCastInfo.HitEntity.Count > 0)
                {
                    List<BoardEntity> allEntity = new List<BoardEntity>(GameManager.Instance.GetEntityViaGroup(m_Receiver.TargetEntityGroup));
                    m_HitReceivers.AddRange(DistanceUtils.GetContactAround(m_Zone, allEntity, damageCastInfo.HitEntity[0].EntityPosition, m_HitCount,true));
                }
            }
        }

        private void PsionicHit()
        {
            foreach (Vector2Int hitReceiver in m_HitReceivers)
            {
                SpellCastUtils.TriggerSpellAt(m_TriggerSpellData, hitReceiver,m_Receiver.EntityPosition);
            }
            m_HitReceivers.Clear();
        }

        protected override void UnApply()
        {
            m_Receiver.EntityEvent.OnRequestCastEvent -= AddPsionicHit;
            m_Receiver.ComputeAllSpells();
            m_Receiver.EntityEvent.OnSpellRecompute -= Recompute;
        }
        
        private void Recompute()
        {
            m_TriggerSpellData.SpellTrigger.ComputeSpellData(m_Receiver);
        }
    }
}