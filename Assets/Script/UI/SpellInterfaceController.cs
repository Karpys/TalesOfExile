using KarpysDev.Script.Entities;
using KarpysDev.Script.Manager;
using KarpysDev.Script.Spell;
using KarpysDev.Script.UI.Pointer;
using KarpysDev.Script.Widget;
using UnityEngine;

namespace KarpysDev.Script.UI
{
    public class SpellInterfaceController : UIPointer
    {
        //UI Part//
        [SerializeField] private Canvas_Skills m_CanvasSkill = null;
        [SerializeField] private SpellIcon m_SpellUI = null;
        [SerializeField] private RectTransform m_SpellLayout = null;
        [SerializeField] private SpellInterpretor m_Interpretor = null;
    
        private SpellIcon[] m_IconsHolder;

        public const int SPELL_DISPLAY_COUNT = 18;
        public SpellInterpretor Interpretor => m_Interpretor;

        private SpellIcon m_CurrentPointer = null;

        public SpellIcon CurrentPointer => m_CurrentPointer;
        //Spell Data Part//
        //List des spells attribue au spell ui icon//
        private void Awake()
        {
            //Initialize tiles depend on screen and tile size//
            m_IconsHolder = new SpellIcon[SPELL_DISPLAY_COUNT];
            for (int i = 0; i < SPELL_DISPLAY_COUNT; i++)
            {
                m_IconsHolder[i] = Instantiate(m_SpellUI, m_SpellLayout.transform);
                m_IconsHolder[i].Initialize(this, i);
                m_IconsHolder[i].SetSpellKey(i,i > (SPELL_DISPLAY_COUNT/2) - 1);
            }

            m_CurrentPointer = m_IconsHolder[0];
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (m_CurrentPointer.PointerUp)
                {
                    m_CurrentPointer.TryUseSpell();
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                if (EntityHelper.CanEditSpell(GameManager.Instance.ControlledEntity) && m_CurrentPointer.PointerUp)
                {
                    m_CanvasSkill.ShowSpellSelection(m_CurrentPointer);
                }
            }
        }

        public void SetSpellIcons(BoardEntity entity)
        {
            TriggerSpellData[] spellsToDisplay = entity.GetDisplaySpells();
        
            for (int i = 0; i < spellsToDisplay.Length; i++)
            {
                m_IconsHolder[i].SetSpell(spellsToDisplay[i]);
            }

            for (int i = spellsToDisplay.Length; i < SPELL_DISPLAY_COUNT; i++)
            {
                m_IconsHolder[i].ClearIcon();
            }
        }

        public void UpdateAllCooldownVisual()
        {
            foreach (SpellIcon spellIcon in m_IconsHolder)
            {
                if (spellIcon.SpellData != null)
                {
                    spellIcon.UpdateCooldownVisual();
                }
            }
        }

        public void SetPointer(SpellIcon pointer)
        {
            m_CurrentPointer = pointer;
        }

        protected override void OnEnter()
        {
            GlobalCanvas.Instance.SetCanvasPointer(this, UICanvasType.SpellIcons);
        }

        protected override void OnExit(){}
    }
}
