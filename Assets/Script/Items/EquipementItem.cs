using System;
using System.Collections.Generic;

public class EquipementItem:Item
{
    private EquipementType m_Type = EquipementType.Null;
    private Modifier[] m_ItemBaseModifiers = null;
    private Modifier[] m_AdditionalModifiers = Array.Empty<Modifier>();

    private bool m_IsEquiped = false;
    private Rarity m_EquipementRarity = Rarity.Null;
    public EquipementItemData BaseEquipementData => Data as EquipementItemData;
    public Modifier[] ItemModifiers => GetAllModifiers();
    
    public EquipementType Type => m_Type;
    public bool IsEquiped => m_IsEquiped;

    public EquipementItem(EquipementItemData data) : base(data)
    {
        m_Type = data.EquipementType;
        InitializeBaseModifier();
    }

    public override List<ItemButtonUIParameters> ButtonRequestOptionButton(ItemUIHolder inventoryUI)
    { 
        List<ItemButtonUIParameters> itemButtonParameters = base.ButtonRequestOptionButton(inventoryUI);
        itemButtonParameters.Add(new ItemButtonUIParameters(TryEquip,"Equip"));
        return itemButtonParameters;
    }
    
    #region Rarity
    
    public void SetRarity(Rarity rarity)
    {
        m_EquipementRarity = rarity;
    }

    public void InitializeRarity(Rarity rarity,RarityParameter parameter)
    {
        SetRarity(rarity);
        ModifierUtils.GiveModifierBasedOnRarityAndType(this, parameter.ModifierDrawCount, BaseEquipementData.EquipementTier);
    }

    protected override Rarity GetRarity()
    {
        Rarity itemRarity = m_EquipementRarity == Rarity.Null ? Data.Rarity : m_EquipementRarity;
        return itemRarity;
    }
    #endregion

    #region Equip
    private void TryEquip()
    {
        EquipementUtils.Equip(this,GameManager.Instance.PlayerEntity);
        m_IsEquiped = true;
        
        if(m_UIHolder)
            m_UIHolder.RefreshEquipedState();
    }

    public void UnEquip()
    {
        m_IsEquiped = false;
        
        if(m_UIHolder)
            m_UIHolder.RefreshEquipedState();
    }
    #endregion

    #region Modifiers

    private void InitializeBaseModifier()
    {
        m_ItemBaseModifiers = new Modifier[BaseEquipementData.EquipementBaseModifiers.Count];

        for (int i = 0; i < BaseEquipementData.EquipementBaseModifiers.Count; i++)
        {
            Modifier modifier = BaseEquipementData.EquipementBaseModifiers[i];
            m_ItemBaseModifiers[i] = new Modifier(modifier.Type, modifier.Value);
        }
    }

    public void SetAdditionalModifiers(Modifier[] modifiers)
    {
        m_AdditionalModifiers = modifiers;
    }
    private Modifier[] GetAllModifiers()
    {
        Modifier[] modifiers = new Modifier[m_ItemBaseModifiers.Length + m_AdditionalModifiers.Length];

        for (int i = 0; i < m_ItemBaseModifiers.Length; i++)
        {
            modifiers[i] = m_ItemBaseModifiers[i];
        }

        for (int i = 0; i < m_AdditionalModifiers.Length; i++)
        {
            modifiers[i + m_ItemBaseModifiers.Length] = m_AdditionalModifiers[i];
        }

        return modifiers;
    }
    #endregion

    //Save Part
    
    //Save Load Constructor
    public EquipementItem(string[] saveArgs) : base(saveArgs)
    {
        m_EquipementRarity = (Rarity)saveArgs[2].ToInt();
        m_IsEquiped = bool.Parse(saveArgs[3]);

        string additionalParameterSave = saveArgs[4];

        if (additionalParameterSave != "null")
        {
            string[] modifierSave = additionalParameterSave.Split('-');
            Modifier[] additionalModifier = new Modifier[modifierSave.Length];

            for (int i = 0; i < modifierSave.Length; i++)
            {
                additionalModifier[i] = new Modifier(modifierSave[i]);
            }

            SetAdditionalModifiers(additionalModifier);
        }
        
        //Base Constructor//
        m_Type = BaseEquipementData.EquipementType;
        InitializeBaseModifier();
        
        if(m_IsEquiped)
            TryEquip();
    }
    public override string GetSaveData()
    {
        string baseSaveData = base.GetSaveData();
        baseSaveData += m_IsEquiped + " ";
        
        string additionalModifierSave = String.Empty;

        if (m_AdditionalModifiers.Length == 0)
        {
            additionalModifierSave = "null";
        }
        else
        {
            for (int i = 0; i < m_AdditionalModifiers.Length; i++)
            {
                Modifier additionalModifier = m_AdditionalModifiers[i];
                additionalModifierSave += additionalModifier.ToSave();

                if (i != m_AdditionalModifiers.Length - 1)
                    additionalModifierSave += "-";
            }
        }

        baseSaveData += additionalModifierSave;

        return baseSaveData;
    }
}