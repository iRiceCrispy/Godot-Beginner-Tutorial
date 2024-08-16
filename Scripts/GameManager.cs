using Godot;

public partial class GameManager : Node
{
    [Export]
    public int _maxHearts;

    [Export]
    private int _initialHearts;

    [Export]
    private Timer _restartTimer;

    [Export]
    private Player _player;

    [Signal]
    public delegate void CoinPickedUpEventHandler(int totalCoins);

    [Signal]
    public delegate void HeartUpdatedEventHandler(int totalHearts);

    public int CoinCounter { get; private set; }
    public int HeartCounter { get; private set; }

    public override void _Ready()
    {
        HeartCounter = _initialHearts;
    }

    public void IncreaseCoin()
    {
        CoinCounter += 1;
        EmitSignal(SignalName.CoinPickedUp, CoinCounter);
    }

    public void HealPlayer(int amount = 1)
    {
        UpdateHeartCounter(amount);
    }

    public void FullHealPlayer()
    {
        UpdateHeartCounter(_maxHearts);
    }

    public void DamagePlayer(int amount = 1)
    {
        if (!_player._isInvincible)
        {
            UpdateHeartCounter(-amount);
            _player.ActivateInvincibilityFrame();
        }
    }

    public void KillPlayer()
    {
        UpdateHeartCounter(-_maxHearts);
    }

    private void UpdateHeartCounter(int amount)
    {
        HeartCounter += amount;

        if (HeartCounter > _maxHearts)
        {
            HeartCounter = _maxHearts;
        }
        else if (HeartCounter < 0)
        {
            HeartCounter = 0;
        }

        GD.Print(HeartCounter);
        UpdateHeartUI();

        if (HeartCounter == 0)
        {
            Restart();
        }
    }

    private void Restart()
    {
        _restartTimer.Start();
        GetTree().Paused = true;
    }

    private void UpdateHeartUI()
    {
        EmitSignal(SignalName.HeartUpdated, HeartCounter);
    }

    private void OnUIManagerReady()
    {
        UpdateHeartUI();
    }

    private void OnTimerTimeout()
    {
        GetTree().Paused = false;
        GetTree().ReloadCurrentScene();
    }
}
