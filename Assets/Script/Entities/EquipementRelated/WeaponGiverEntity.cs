namespace KarpysDev.Script.Entities.EquipementRelated
{
    using Items;
    using Spell.DamageSpell;
    using UnityEngine;

    public class WeaponGiverEntity : MonoBehaviour
    {
        [SerializeField] private DamageSource m_DamageSource = null;
        [SerializeField] private WeaponTarget m_WeaponTarget = WeaponTarget.AllWeapons;
        [SerializeField] private WeaponType m_WeaponType = WeaponType.Sword;
        [SerializeField] private BoardEntity m_BoardEntity = null;

        private void Awake()
        {
            m_BoardEntity.A_OnEntityInitialization += EquipWeapon;
        }

        private void OnValidate()
        {
            m_BoardEntity = GetComponent<BoardEntity>();
        }

        private void EquipWeapon()
        {
            m_BoardEntity.EntityStats.AddWeapon(new BaseWeapon(m_DamageSource,m_WeaponType), m_WeaponTarget);
        }
    }
}