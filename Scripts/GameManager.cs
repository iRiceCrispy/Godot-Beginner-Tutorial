using Godot;

public partial class GameManager : Node
{
    [Signal]
    public delegate void CoinPickedUpEventHandler(int totalCoins);

    public int CoinCounter { get; private set; } = 0;

    public void IncreaseCoin()
    {
        CoinCounter += 1;
        EmitSignal(SignalName.CoinPickedUp, CoinCounter);
    }
}
