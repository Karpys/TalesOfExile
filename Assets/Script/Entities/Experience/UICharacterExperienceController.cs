using KarpysDev.Script.Manager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KarpysDev.Script.Entities
{
    public class UICharacterExperienceController : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_LevelUI = null;
        [SerializeField] private TMP_Text m_LevelFillRatio = null;
        [SerializeField] private Image m_LevelFill = null;
        [SerializeField] private Transform m_ExperienceContainer = null;

        private const string BASE_LVL = "LVL ";
        private IExperience m_Experience = null;

        private void Awake()
        {
            GameManager.Instance.A_OnControlledEntityChange += UpdateUIOnChange;
            GameManager.Instance.A_OnEndTurn += UpdateUI;
        }

        private void UpdateUIOnChange(BoardEntity old, BoardEntity newEntity)
        {
            if (newEntity is IExperience experience)
            {
                m_Experience = experience;
                m_ExperienceContainer.gameObject.SetActive(true);
                UpdateUI();
            }
            else
            {
                m_Experience = null;
                DisplayDefaultUI();
            }
        }

        private void DisplayDefaultUI()
        {
            m_ExperienceContainer.gameObject.SetActive(false);
            m_LevelUI.text = "-";
        }

        private void UpdateUI()
        {
            if(m_Experience == null)
                return;

            float levelRatio = m_Experience.LevelExperienceRatio;
            m_LevelUI.text = BASE_LVL + m_Experience.Level;
            m_LevelFill.fillAmount =levelRatio;
            m_LevelFillRatio.text = (levelRatio * 100).ToString("0") + " %";
        }
    }
}