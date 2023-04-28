public class BoardBalistaEntity : FriendlyEntity
{
    protected override void InitializeEntityBehaviour()
    {
        SetEntityBehaviour(new BalistaIA(this));
    }
}