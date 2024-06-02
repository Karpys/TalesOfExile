namespace KarpysDev.Script.Entities
{
    public interface IExperienceGiver
    {
        public void GiveExperience(BoardEntity receiver, float amount);
    }
}