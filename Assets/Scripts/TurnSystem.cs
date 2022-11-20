using System;

public class TurnSystem : SingletonMonoBehaviour<TurnSystem>
{
    public event EventHandler<int> OnTurnChanged;
    private int turnNumber = 1;

    public void NextTurn()
    {
        turnNumber++;
        OnTurnChanged.Invoke(this, turnNumber);
    }
}