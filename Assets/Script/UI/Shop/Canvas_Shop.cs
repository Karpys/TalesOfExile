using System;
using System.Collections.Generic;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.Pointer;
using KarpysDev.Script.Widget;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace KarpysDev.Script.UI
{
    public class Canvas_Shop : MonoBehaviour,IUIPointerController
    {
        [SerializeField] private Transform m_GridLayoutTransform = null;
        [SerializeField] private Object[] m_IBuyableReference = null;
        [SerializeField] private UIBuyableHolder m_BuyableHolderPrefab = null;
        [SerializeField] private TMP_Text m_GoldCountText = null;

        [Header("Buyable Selection")] 
        [SerializeField] private Image m_BuyableVisual = null; 
        [SerializeField] private TMP_Text m_BuyablePrice = null;

        [Header("Button Buy")] 
        [SerializeField] private ShopBuyUIButtonPointer m_ShopButton = null;

        private bool m_HasInit = false;
        private UIBuyableHolder m_CurrentPointer = null;
        private Clock m_BuyableDisplay = null;
        
        private UIBuyableHolder m_SelectedBuyableHolder = null;

        private List<UIBuyableHolder> m_BuyableHolders = new List<UIBuyableHolder>();
        private void Update()
        {
            m_BuyableDisplay?.UpdateClock();

            if (Input.GetMouseButtonDown(0))
            {
                TrySelectBuyable();
            }
        }

        private void Start()
        {
            GoldManager.Instance.OnGoldUpdated += CheckButtonState;
            CheckButtonState();
        }

        private void OnDestroy()
        {
            if(GoldManager.Instance)
                GoldManager.Instance.OnGoldUpdated -= CheckButtonState;
        }

        public void Open()
        {
            if (!m_HasInit)
                Init();
            gameObject.SetActive(true);
            GlobalCanvas.Instance.GoldUIUpdater.AddGoldDisplayer(m_GoldCountText);
        }

        private void Init()
        {
            m_HasInit = true;

            for (int i = 0; i < m_IBuyableReference.Length; i++)
            {
                //Todo: Already buy check//
                if (m_IBuyableReference[i] is IBuyableData buyableData)
                {
                    UIBuyableHolder buyableHolder = Instantiate(m_BuyableHolderPrefab, m_GridLayoutTransform);
                    buyableHolder.InitializeBuyableHolder(buyableData.ToUIBuyable(buyableHolder.transform));
                    buyableHolder.AssignController(this);
                    m_BuyableHolders.Add(buyableHolder);
                }
                else
                {
                    Debug.Log("IBuyableData cast failed");
                }
            }
        }

        private void RemoveBuyable(UIBuyableHolder buyableHolder)
        {
            for (int i = 0; i < m_BuyableHolders.Count; i++)
            {
                if (m_BuyableHolders[i] == buyableHolder)
                {
                    m_BuyableHolders.Remove(buyableHolder);
                    Destroy(buyableHolder.gameObject);
                }
            }
        }
        
        public void Close()
        {
            GlobalCanvas.Instance.GetSpellUIDisplayer().HideSpell();
            gameObject.SetActive(false);
            GlobalCanvas.Instance.GoldUIUpdater.RemoveGoldDisplayer(m_GoldCountText);
            ClearAll();
        }

        public void SetCurrentPointer(UIPointerController pointerController)
        {
            m_CurrentPointer = pointerController as UIBuyableHolder;

            m_BuyableDisplay = new Clock(0.05f, TryDisplayBuyable);
        }

        public void OnPointerExit(UIPointerController pointerController)
        {
            ((UIBuyableHolder)pointerController).Buyable.HideBuyable();
        }

        private void TryDisplayBuyable()
        {
            if (m_CurrentPointer.PointerUp)
            {
                m_CurrentPointer.Buyable.DisplayBuyable();
            }
        }
        
        private void TrySelectBuyable()
        {
            if (m_CurrentPointer != null && m_CurrentPointer.PointerUp)
            {
                SetCurrentBuyable(m_CurrentPointer);
            }
        }

        private void SetCurrentBuyable(UIBuyableHolder buyableHolder)
        {
            if (m_SelectedBuyableHolder != null)
            {
                m_SelectedBuyableHolder.Clear();
            }
            m_SelectedBuyableHolder = buyableHolder;
            
            m_SelectedBuyableHolder.OnSelect();
            m_BuyablePrice.text = buyableHolder.Buyable.Price.ToString("0") + GoldManager.GOLD_ICON;
            m_BuyableVisual.sprite = buyableHolder.Buyable.GetIcon();
            m_BuyableVisual.color = Color.white;
            CheckButtonState();
        }

        private void CheckButtonState()
        {
            if (m_SelectedBuyableHolder == null)
            {
                m_ShopButton.SetState(false);   
                return;
            }
            m_ShopButton.SetState(GoldManager.Instance.CanBuy(m_SelectedBuyableHolder.Buyable.Price));
        }

        public void Buy()
        {
            //Buy current buyable//
            m_SelectedBuyableHolder.Buyable.OnBuy();
            RemoveBuyable(m_SelectedBuyableHolder);
            ClearAll();
        }

        private void ClearAll()
        {
            if(m_SelectedBuyableHolder)
                m_SelectedBuyableHolder.Clear();
            m_SelectedBuyableHolder = null;
            m_BuyableVisual.sprite = null;
            m_BuyablePrice.text = String.Empty;
            m_BuyableVisual.color = Color.white.setAlpha(0);
            CheckButtonState();
        }
    }
}