namespace KarpysDev.Script.Map_Related
{
    public interface ITurn
    {
        int TurnLeft { get; }
        void SetTurn(int turn);
        void ReduceTurn();
        void OnEndLife();
        void SubToTurnManager();
    }
}