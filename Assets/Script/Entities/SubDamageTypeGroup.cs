using KarpysDev.KarpysUtils;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.DamgeType
{
    [System.Serializable]
    public class SubDamageTypeGroup
    {
        [SerializeField] private float m_Physical;
        [SerializeField] private float m_Cold;
        [SerializeField] private float m_Lightning;
        [SerializeField] private float m_Fire;
        [SerializeField] private float m_Nature;

        public float Physical => m_Physical;
        public float Cold => m_Cold;
        public float Lightning => m_Lightning;
        public float Nature => m_Nature;
        public float Fire => m_Fire;

        public SubDamageTypeGroup(SubDamageTypeGroup typeGroup)
        {
            m_Physical = typeGroup.Physical;
            m_Cold = typeGroup.Cold;
            m_Fire = typeGroup.Fire;
            m_Nature = typeGroup.Nature;
            m_Lightning = typeGroup.Lightning;
        }

        public float GetTypeValue(SubDamageType subDamageType)
        {
            switch (subDamageType)
            {
                case SubDamageType.Physical:
                    return m_Physical;
                case SubDamageType.Fire:
                    return m_Fire;
                case SubDamageType.Cold:
                    return m_Cold;
                case SubDamageType.Lightning:
                    return m_Lightning;
                case SubDamageType.Nature:
                    return m_Nature;
                default:
                    return 0;
            }
        }

        public void ChangeColdValue(float value)
        {
            m_Cold += value;
        }
        
        public void ChangePhysicalValue(float value)
        {
            m_Physical += value;
        }

        public void ChangeLightningValue(float value)
        {
            m_Lightning += value;
        }

        public void ChangeFireValue(float value)
        {
            m_Fire += value;
        }

        public void ChangeNatureValue(float value)
        {
            m_Nature += value;
        }
    }
}
