﻿using System;
using System.Collections.Generic;
using KarpysDev.Script.Entities.EquipementRelated;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Manager.Library;
using KarpysDev.Script.UI;
using KarpysDev.Script.UI.ItemContainer;
using KarpysDev.Script.UI.ItemContainer.V2;
using KarpysDev.Script.Utils;

namespace KarpysDev.Script.Items
{
    using Entities;

    public class EquipementItem:Item
    {
        private EquipementType m_Type = EquipementType.Null;
        private Modifier[] m_ItemBaseModifiers = null;
        private Modifier[] m_AdditionalModifiers = Array.Empty<Modifier>();

        private bool m_IsEquiped = false;
        private Rarity m_EquipementRarity = Rarity.Null;
        public EquipementItemData EquipementData => Data as EquipementItemData;
        public Modifier[] ItemModifiers => GetAllModifiers();
    
        public EquipementType Type => m_Type;
        public bool IsEquiped => m_IsEquiped;

        public bool IsTwoHandedWeapon => Data is WeaponEquipementItemdata {TwoHanded: true};

        public EquipementItem(EquipementItemData data) : base(data)
        {
            m_Type = data.EquipementType;
            InitializeBaseModifier();
        }

        public override List<ItemButtonUIParameters> ButtonRequestOptionButton(ItemUIHolder inventoryUI)
        { 
            List<ItemButtonUIParameters> itemButtonParameters = base.ButtonRequestOptionButton(inventoryUI);
            // itemButtonParameters.Add(new ItemButtonUIParameters(Equip,"Equip"));
            return itemButtonParameters;
        }
    
        #region Rarity
    
        private void SetRarity(Rarity rarity)
        {
            m_EquipementRarity = rarity;
        }

        public void InitializeRarity(Rarity rarity,RarityParameter parameter)
        {
            SetRarity(rarity);
            ModifierUtils.GiveModifierBasedOnRarityAndType(this, parameter.ModifierDrawCount);
        }

        protected override Rarity GetRarity()
        {
            Rarity itemRarity = m_EquipementRarity == Rarity.Null ? Data.Rarity : m_EquipementRarity;
            return itemRarity;
        }
        #endregion

        #region Equip
        public void Equip(BoardEntity entity)
        {
            EquipementUtils.Equip(this,entity);
            m_IsEquiped = true;
        }

        public void UnEquip(BoardEntity entity)
        {
            EquipementUtils.UnEquip(this,entity);
            m_IsEquiped = false;
        }
        #endregion

        #region Modifiers

        private void InitializeBaseModifier()
        {
            m_ItemBaseModifiers = new Modifier[EquipementData.EquipementBaseModifiers.Count];

            for (int i = 0; i < EquipementData.EquipementBaseModifiers.Count; i++)
            {
                Modifier modifier = EquipementData.EquipementBaseModifiers[i];
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
            m_Type = EquipementData.EquipementType;
            InitializeBaseModifier();
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
}