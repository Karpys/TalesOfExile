namespace KarpysDev.Script.Entities
{
    public class DefaultExperienceGiver:IExperienceGiver
    {
        public void GiveExperience(BoardEntity receiver, float amount)
        {
            receiver.ReceiveExp(amount);
        }
    }
}