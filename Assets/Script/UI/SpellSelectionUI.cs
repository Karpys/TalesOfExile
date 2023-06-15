using System.Collections.Generic;
using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class SpellSelectionUI:MonoBehaviour
    {
        [SerializeField] private Canvas_Skills m_CanvasSkill = null;
        [SerializeField] private Transform m_Container = null;
        [SerializeField] private Transform m_LayoutTransform = null;
        [SerializeField] private SpellSelectionUIHolder m_HolderPrefab = null;

        private const float BASE_HEIGHT = 4f;
        private const float BASE_ELEMENT_HEIGHT = 48f;
        private const int ELEMENT_PER_ROW = 3;

        private int m_CurrentSpellId = 0;

        private List<SpellUIHolder> m_Holders = new List<SpellUIHolder>();

        private bool m_IsShown = false;
        private RectTransform m_Rect = null;
        private SpellUIHolder m_CurrentPointer = null;
        private void Awake()
        {
            m_Rect = transform as RectTransform;
        }

        private void Start()
        {
            GameManager.Instance.A_OnControlledEntityChange += OnControlledEntityChange;
        }

        private void OnDestroy()
        {
            if(GameManager.Instance)
                GameManager.Instance.A_OnControlledEntityChange -= OnControlledEntityChange;
        }

        private void OnControlledEntityChange(BoardEntity _, BoardEntity __)
        {
            Hide();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && m_IsShown)
            {
                if (m_CurrentPointer.PointerUp)
                {
                    SetSpell(m_CurrentPointer);
                    Hide();
                }
                else
                {
                    Hide();
                }
            }
        }



        public void Show(int pointerSpellId,Transform pointerOrigin)
        {
            ClearOldSpells();
            m_IsShown = true;
            
            RectTransform rect = (RectTransform) pointerOrigin;
            m_Rect.position = new Vector3(rect.position.x, 0);
            m_Rect.anchoredPosition = new Vector2(m_Rect.anchoredPosition.x, rect.sizeDelta.y);
            
            m_Container.gameObject.SetActive(true);
            m_CurrentSpellId = pointerSpellId;
            DisplaySpells();
            AdaptSize();
        }
        
        private void Hide()
        {
            ClearOldSpells();
            m_Container.gameObject.SetActive(false);
        }

        private void AdaptSize()
        {
            float height = Mathf.Ceil((float)m_Holders.Count / ELEMENT_PER_ROW) * BASE_ELEMENT_HEIGHT + BASE_HEIGHT;
            m_Rect.sizeDelta = new Vector2(m_Rect.sizeDelta.x, height);
        }

        private void ClearOldSpells()
        {
            int count = m_Holders.Count;
            for (int i = 0; i < count; i++)
            {
                Destroy(m_Holders[0].gameObject);
                m_Holders.RemoveAt(0);
            }
        }

        private void DisplaySpells()
        {
            //BoardEntity controlledEntity = GameManager.Instance.ControlledEntity;
            ISpellSet spellSet = GameManager.Instance.ControlledEntity as ISpellSet;

            if (spellSet == null)
            {
                Debug.LogError("Spell set equals nul ?");
                return;
            }

            TriggerSpellData[] spellDatas = spellSet.GetDisplaySpells();
            
            for (int i = 0; i < spellDatas.Length; i++)
            {
                if (spellDatas[i] != null)
                {
                    SpellSelectionUIHolder holder = Instantiate(m_HolderPrefab, m_LayoutTransform);
                    holder.Initialize(spellDatas[i]);
                    holder.SetSelectionController(this);
                    m_Holders.Add(holder);
                }
            }

            SpellSelectionUIHolder nullHolder = Instantiate(m_HolderPrefab, m_LayoutTransform);
            nullHolder.Initialize(null);
            nullHolder.SetSelectionController(this);
            m_Holders.Add(nullHolder);
            
            m_CurrentPointer = m_Holders[0];
        }

        

        public void SetCurrentPointer(SpellUIHolder spellSelectionUIHolder)
        {
            Debug.Log("Set");
            m_CurrentPointer = spellSelectionUIHolder;
        }
        
        private void SetSpell(SpellUIHolder currentPointer)
        {
            ISpellSet spellSet = (ISpellSet) GameManager.Instance.ControlledEntity;

            if (spellSet == null)
            {
                Debug.LogError("Try to set spell with not autorized entity");
                return;
            }
            
            spellSet.InsertSpell(currentPointer.TriggerSpellData,m_CurrentSpellId);
            m_CanvasSkill.RefreshTargetSkills(GameManager.Instance.ControlledEntity);
        }
    }
}
