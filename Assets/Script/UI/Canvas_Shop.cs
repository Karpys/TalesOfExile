using System;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.Pointer;
using KarpysDev.Script.Widget;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace KarpysDev.Script.UI
{
    public class Canvas_Shop : MonoBehaviour,IUIPointerController
    {
        [SerializeField] private Transform m_GridLayoutTransform = null;
        [SerializeField] private Object[] m_IBuyableReference = null;
        [SerializeField] private UIBuyableHolder m_BuyableHolderPrefab = null;

        [SerializeField] private TMP_Text m_GoldCountText = null;

        private bool m_HasInit = false;
        private UIBuyableHolder m_CurrentPointer = null;
        private Clock m_BuyableDisplay = null;

        private void Update()
        {
            m_BuyableDisplay?.UpdateClock();
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
                }
                else
                {
                    Debug.Log("IBuyableData cast failed");
                }
            }
        }
        
        public void Close()
        {
            GlobalCanvas.Instance.GetSpellUIDisplayer().HideSpell();
            gameObject.SetActive(false);
            GlobalCanvas.Instance.GoldUIUpdater.RemoveGoldDisplayer(m_GoldCountText);
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
    }
}