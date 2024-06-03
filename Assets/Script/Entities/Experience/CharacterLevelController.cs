using KarpysDev.KarpysUtils;
using UnityEngine;

namespace KarpysDev.Script.Entities
{
    public class CharacterLevelController
    {
        private int m_Level = 0;
        private float m_Experience = 0;
        private float m_NextLevelExperienceNeeded = 0;

        public int Level => m_Level;
        public float Experience => m_Experience;
        public float ExperienceRatio => Mathf.Min(1,m_Experience / m_NextLevelExperienceNeeded);

        public CharacterLevelController(int level, float experience)
        {
            m_Level = level;
            if (m_Level == 0)
                m_Level = 1;
            m_Experience = experience;
            m_NextLevelExperienceNeeded = GetExperienceNeeded(m_Level + 1);
        }

        public void GainExperience(float experience)
        {
            m_Experience += experience;
            LevelUpCheck();
        }

        private void LevelUp()
        {
            m_Level.Log("On Level Up");
            m_Level++;
            m_Experience -= m_NextLevelExperienceNeeded;
            m_NextLevelExperienceNeeded = GetExperienceNeeded(m_Level + 1);
            LevelUpCheck();
        }

        private void LevelUpCheck()
        {
            if (m_Experience >= m_NextLevelExperienceNeeded)
            {
                LevelUp();
            }
        }

        //https://howtomakeanrpg.com/a/how-to-make-an-rpg-levels.html
        //DND methods
        private float GetExperienceNeeded(int level)
        {
            return 500 * level * level - 500 * level;
        }
    }
}