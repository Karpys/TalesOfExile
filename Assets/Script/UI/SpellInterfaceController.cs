using UnityEngine;

namespace Script.UI
{
    public class SpellInterfaceController : MonoBehaviour
    {
        //UI Part//
        [SerializeField] private SpellIcon m_SpellUI = null;
        [SerializeField] private int m_SpellCount = 0;
        [SerializeField] private RectTransform m_SpellLayout = null;
        private SpellIcon[] m_IconsHolder;

        //Spell Data Part//
        //List des spells attribue au spell ui icon//
        private void Awake()
        {
            //Initalize tiles depend on screen and tile size//
            m_IconsHolder = new SpellIcon[m_SpellCount];
            for (int i = 0; i < m_SpellCount; i++)
            {
                m_IconsHolder[i] = Instantiate(m_SpellUI, m_SpellLayout.transform);
                m_IconsHolder[i].SetSpellKey(i);
            }
        }

        public void SetSpellIcons(BoardEntity entity)
        {
            for (int i = 0; i < entity.Spells.Count; i++)
            {
                TriggerSpellData triggerSpell = entity.Spells[i] as TriggerSpellData;
                
                if(triggerSpell == null)
                    return;
                
                m_IconsHolder[i].SetSpell(triggerSpell);
            }

            for (int i = entity.Spells.Count; i < m_SpellCount; i++)
            {
                m_IconsHolder[i].ClearIcon();
            }
        }
    }
}