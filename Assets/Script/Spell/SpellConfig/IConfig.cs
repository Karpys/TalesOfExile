namespace KarpysDev.Script.Spell.SpellConfig
{
    public interface IConfig
    {
        bool CanDisplayConfig { get; }
        void DisplayConfig(ConfigMonitor monitor);
    }
}