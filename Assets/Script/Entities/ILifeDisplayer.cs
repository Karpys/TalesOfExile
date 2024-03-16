namespace KarpysDev.Script.Entities
{
    public interface ILifeDisplayer
    {
        public BoardEntityLife EntityLife { get; }
        public void EnableDisplay();
        public void DisableDisplay();
        public void UpdateLifeDisplay();
        public void UpdateShieldDisplay();
        public void EnableShieldDisplay();
        public void HideShieldDisplay();
    }
}