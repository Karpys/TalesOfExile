public class SilenceBuff : Buff
{
    protected override void Apply()
    {
        m_Receiver.EntityEvent.OnRequestBlockSpell += AddBlockSpell;
    }

    private void AddBlockSpell(IntSocket blockSpell)
    {
        blockSpell.Value += 1;
    }

    protected override void UnApply()
    {
        m_Receiver.EntityEvent.OnRequestBlockSpell -= AddBlockSpell;
    }
}