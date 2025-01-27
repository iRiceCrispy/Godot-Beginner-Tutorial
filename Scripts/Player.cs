using Godot;

public partial class Player : CharacterBody2D
{
    [Export]
    private float _speed;

    [Export]
    private float _jumpVelocity;

    [Export]
    private Sprite2D _sprite;

    [Export]
    private Timer _iFrameTimer;

    [Export]
    private AnimationPlayer _animationPlayer;

    private GameManager _gameManager;
    private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

    public bool _isInvincible;

    public override void _Ready()
    {
        _gameManager = GetNode<GameManager>("%GameManager");
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity.Y += _gravity * (float)delta;
        }

        if (Input.IsActionPressed("jump") && IsOnFloor())
        {
            velocity.Y = -_jumpVelocity;
        }

        float direction = Input.GetAxis("move_left", "move_right");
        if (direction != 0)
        {
            if (direction < 0)
            {
                _sprite.FlipH = true;
            }
            else if (direction > 0)
            {
                _sprite.FlipH = false;
            }

            velocity.X = direction * _speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, _speed);
        }

        Velocity = velocity;

        MoveAndSlide();
    }

    private void OnBottomBoundBodyEntered(Node2D body)
    {
        _gameManager.KillPlayer();
    }

    public void ActivateInvincibilityFrame()
    {
        _isInvincible = true;
        _animationPlayer.Play("blink");
        _iFrameTimer.Start();
    }

    private void OnIFrameTimerTimeout()
    {
        _isInvincible = false;
        _animationPlayer.Stop();
    }
}
