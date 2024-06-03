namespace KarpysDev.Script.Entities
{
    public class PlayerSaveData
    {
        public int CharacterLevel = 0;
        public float LevelExperience = 0;
        public PlayerSaveData(int characterLevel,float levelExperience)
        {
            CharacterLevel = characterLevel;
            LevelExperience = levelExperience;
        }
    }
}