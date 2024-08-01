using Godot;

public partial class UIManager : Node
{
    [Export]
    private Label _coinCounter;

    private GameManager _gameManager;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("%GameManager");

        _gameManager.CoinPickedUp += UpdateCoinCounterText;
    }

    public void UpdateCoinCounterText(int totalCoins)
    {
        _coinCounter.Text = totalCoins.ToString("D3");
    }
}
