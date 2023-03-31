using UnityEngine;

public class BalistaIA : BaseEntityIA
{
    public BalistaIA(BoardEntity entity) : base(entity){}

    protected override bool MovementAction()
    {
        return false;
    }
}