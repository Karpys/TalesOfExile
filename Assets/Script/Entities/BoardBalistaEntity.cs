public class BoardBalistaEntity : FriendlyEntity
{
    protected override void InitalizeEntityBehaviour()
    {
        SetEntityBehaviour(new BalistaIA(this));
    }
}