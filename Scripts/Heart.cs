using Godot;

public partial class Heart : Area2D
{
    [Export]
    private int _healing;

    private GameManager _gameManager;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("%GameManager");
    }

    private void OnBodyEntered(Node2D body)
    {
        _gameManager.HealPlayer();
        QueueFree();
    }
}
