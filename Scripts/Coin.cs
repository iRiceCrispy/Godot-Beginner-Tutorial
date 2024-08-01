using Godot;

public partial class Coin : Area2D
{
    private GameManager _gameManager;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("%GameManager");
    }

    private void OnBodyEntered(Node2D body)
    {
        _gameManager.IncreaseCoin();
        QueueFree();
    }
}
