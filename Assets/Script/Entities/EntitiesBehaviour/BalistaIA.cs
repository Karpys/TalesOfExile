using UnityEngine;

public class BalistaIA : BaseEntityIA
{
    public BalistaIA(BoardEntity entity) : base(entity)
    {
    }

    public override void Behave()
    {
        Debug.Log("BALISTA BEHAVE");
        base.Behave();
    }

    protected override bool MovementAction()
    {
        return false;
    }
}