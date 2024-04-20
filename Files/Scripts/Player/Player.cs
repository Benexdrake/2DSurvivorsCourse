using Godot;
using System;

public partial class Player : CharacterBody2D
{
    [Export(PropertyHint.Range,"0,200,0.5")] public int Speed { get; private set; } = 125;
    [Export(PropertyHint.Range,"0,200,0.5")] public int AccelerationSmoothing { get; private set; } = 50;
    private HealthComponent _healthComponent;

    private ProgressBar _healthBar;
    private Timer _damageIntervalTimer;
    private int _numberCollidingBodies = 0;

    private Area2D _collisionArea;
    public override void _Ready()
    {
        _collisionArea = GetNode<Area2D>("CollisionArea2D");
        _damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");
        _healthComponent = GetNode<HealthComponent>("HealthComponent");
        _healthBar = GetNode<ProgressBar>("HealthBar");

        _collisionArea.BodyEntered += OnCollisionAreaEntered;
        _collisionArea.BodyExited += OnCollisionAreaExited;
        _damageIntervalTimer.Timeout += () => CheckDealDamage();
        _healthComponent.HealthChanged += OnHealthChanged;

        UpdateHealthDisplay();
    }

    public override void _PhysicsProcess(double delta)
    {
        var movementVector = GetMovementVector();
        var direction = movementVector.Normalized();
        var targetVelocity = direction * Speed;

        Velocity = Velocity.Lerp(targetVelocity, 1 -  (float)Mathf.Exp(-delta * AccelerationSmoothing));
        
        MoveAndSlide();
    }

    private void OnCollisionAreaEntered(Node2D body)
    {
        _numberCollidingBodies++;
        CheckDealDamage();
        GD.Print(_numberCollidingBodies);
    }

    private void OnCollisionAreaExited(Node2D body)
    {
        _numberCollidingBodies--;
    }

    private void CheckDealDamage()
    {
        if(_numberCollidingBodies == 0 || !_damageIntervalTimer.IsStopped())
            return;

        _healthComponent.Damage(1);
        _damageIntervalTimer.Start();
        GD.Print(_healthComponent.CurrentHealth);
    }

    private void OnHealthChanged()
    {
        UpdateHealthDisplay();
        GD.Print(_healthBar.Value);
    }

    private void UpdateHealthDisplay()
    {
        _healthBar.MaxValue = _healthComponent.MaxHealth;
        _healthBar.Value = _healthComponent.CurrentHealth;
    }

    private Vector2 GetMovementVector()
    {
        var xMovement = Input.GetActionStrength(GameConstants.INPUT_MOVE_RIGHT) - Input.GetActionStrength(GameConstants.INPUT_MOVE_LEFT);
        var yMovement = Input.GetActionStrength(GameConstants.INPUT_MOVE_DOWN) - Input.GetActionStrength(GameConstants.INPUT_MOVE_UP);

        return new Vector2(xMovement, yMovement);
    }
}
