using System;

public class TurnSystem : SingletonMonoBehaviour<TurnSystem>
{
    public event EventHandler<int> OnTurnChanged;
    private int turnNumber = 1;
    private bool isPlayerTurn = true;

    public void NextTurn()
    {
        turnNumber++;
        isPlayerTurn = !isPlayerTurn;
        OnTurnChanged.Invoke(this, turnNumber);
    }

    public bool IsPlayerTurn()
    {
        return isPlayerTurn;
    }
}