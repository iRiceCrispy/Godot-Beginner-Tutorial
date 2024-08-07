using Godot;

public partial class GameManager : Node
{
    [Export]
    private Timer _restartTimer;

    [Signal]
    public delegate void CoinPickedUpEventHandler(int totalCoins);

    public int CoinCounter { get; private set; }

    public void IncreaseCoin()
    {
        CoinCounter += 1;
        EmitSignal(SignalName.CoinPickedUp, CoinCounter);
    }

    public void Restart()
    {
        _restartTimer.Start();
        GetTree().Paused = true;
    }

    private void OnTimerTimeout()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}
