using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EntityStats
{
    //All the EntitiesStats//
    //From Damage to move speed to base life ?//
    //MainTypeModifier//
    public float MeleeModifier = 0f;
    public float ProjectileModifier = 0f;
    public float SpellModifier = 0f;
    //SubTypeModifier//
    public float ColdDamageModifier = 0f;
    public float HolyDamageModifier = 0f;
    public float FireDamageModifier = 0f;
    //Physical
    //Ect ect


    public float GetDamageModifier(SubDamageType subDamageType)
    {
        switch (subDamageType)
        {
            case SubDamageType.Cold:
                return ColdDamageModifier;
            case SubDamageType.Holy:
                return HolyDamageModifier;
            case SubDamageType.Fire:
                return FireDamageModifier;
            default:
                return 0;
        }
    }
    
    public float GetMainTypeModifier(MainDamageType mainDamageType)
    {
        switch (mainDamageType)
        {
            case MainDamageType.Melee:
                return MeleeModifier;
            case MainDamageType.Projectile:
                return ProjectileModifier;
            case MainDamageType.Spell:
                return SpellModifier;
            default:
                return 0;
        }
    }
}
public class BoardEntity : MonoBehaviour
{
    [Header("Base")]
    [SerializeField] protected int m_XPosition = 0;
    [SerializeField] protected int m_YPosition = 0;
    [SerializeField] protected float m_Life = 100;
    [SerializeField] protected EntityStats m_Stats = null;
    [SerializeField] protected List<SpellData> m_Spells = new List<SpellData>();
    [SerializeField] protected AddDamageModifier m_TestModifier = null;

    protected MapData m_TargetMap = null;

    public Vector2Int EntityPosition => new Vector2Int(m_XPosition, m_YPosition);
    public List<SpellData> Spells => m_Spells;
    public EntityStats EntityStats => m_Stats;

    protected virtual void Start()
    {
        Debug.Log("New entity created: " + gameObject.name + "at :" + EntityPosition);
        
        //Initi Serialize Spells//
        RegisterStartSpells();
    }
    
    //Board Related
    public void Place(int x, int y, MapData targetMap)
    {
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap = targetMap;
        transform.position = targetMap.GetTilePosition(x, y);
        m_TargetMap.Map.Tiles[x, y].Walkable = false;
    }

    public virtual void MoveTo(int x,int y)
    {
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = true;
        m_XPosition = x;
        m_YPosition = y;
        m_TargetMap.Map.Tiles[m_XPosition, m_YPosition].Walkable = false;
        //OnMove ?//
        Movement();
    }
    
    public void MoveTo(Vector2Int pos)
    {
        MoveTo(pos.x,pos.y);
    }

    protected virtual void Movement()
    {
        transform.position = m_TargetMap.GetTilePosition(m_XPosition, m_YPosition);
    }
    
    //Entity Behaviour Related//
    private void RegisterStartSpells()
    {
        for (int i = 0; i < m_Spells.Count; i++)
        {
            RegisterSpell(m_Spells[i]);
        }
    }

    protected void RegisterSpell(SpellData spell)
    {
        spell.AttachedEntity = this;
        spell.InitializeTrigger();
    }
    
    //Stats Related//
    
    //Damage Related//
    public void ChangeLifeValue(float value)
    {
        m_Life += value;
    }

    public DamageSource[] GetAdditionalSources(DamageType damageType)
    {
        //Fetch All Additional Sources / From equipement / skill tree
        List<DamageSource> additionalSources = new List<DamageSource>();
        
        //Test is in local entity
        DamageSource newSource = m_TestModifier.GetAdditionalDamage(damageType);
        
        if(newSource != null)
            additionalSources.Add(newSource);
        return additionalSources.ToArray();
    }
}