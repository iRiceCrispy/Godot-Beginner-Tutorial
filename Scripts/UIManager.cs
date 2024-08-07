using Godot;

public partial class UIManager : Node
{
    [Export]
    private Label _coinCounter;

    [Export]
    private HBoxContainer _heartCounter;

    [Export]
    private Texture2D _heartFull;

    [Export]
    private Texture2D _heartEmpty;

    private GameManager _gameManager;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("%GameManager");

        _gameManager.CoinPickedUp += UpdateCoinCounterText;
        _gameManager.HeartUpdated += UpdateHeartCounterValue;

        CreateHeartCounter();
    }

    public void UpdateCoinCounterText(int totalCoins)
    {
        _coinCounter.Text = totalCoins.ToString("D3");
    }

    public void UpdateHeartCounterValue(int totalHearts)
    {
        for (int i = 0; i < _heartCounter.GetChildCount(); i++)
        {
            TextureRect heart = _heartCounter.GetChild<TextureRect>(i);

            if (totalHearts > i)
            {
                heart.Texture = _heartFull;
            }
            else
            {
                heart.Texture = _heartEmpty;
            }
        }
    }

    private void CreateHeartCounter()
    {
        int maxHearts = _gameManager._maxHearts;

        for (int i = 0; i < maxHearts; i++)
        {
            TextureRect heart = new()
            {
                ExpandMode = TextureRect.ExpandModeEnum.FitWidth,
                StretchMode = TextureRect.StretchModeEnum.KeepAspectCentered
            };

            _heartCounter.AddChild(heart);
        }
    }
}
