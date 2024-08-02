using Godot;

public partial class Enemy : Area2D
{
    [Export]
    private RayCast2D _rayCastFront;

    [Export]
    private RayCast2D _rayCastGround;

    [Export]
    private float _speed;

    private GameManager _gameManager;
    private float _direction = 1f;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("%GameManager");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 position = Position;
        Vector2 scale = Scale;

        if (_rayCastFront.IsColliding() || !_rayCastGround.IsColliding())
        {
            _direction = -_direction;
        }

        position.X += _direction * _speed * (float)delta;
        scale.X = _direction * Mathf.Abs(scale.X);

        Position = position;
        Scale = scale;
    }

    private void OnBodyEntered(Node2D body)
    {
        _gameManager.Restart();
    }
}
