using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    [Export(PropertyHint.Range,"0,200,0.5")] public int Speed { get; private set; } = 125;
    [Export(PropertyHint.Range,"0,200,0.5")] public int AccelerationSmoothing { get; private set; } = 50;
    [Export] private PackedScene _swordAbility;
    [Export] private SwordAbilityController _swordAbilityController;
    public HealthComponent _healthComponent {get; private set;}

    private ProgressBar _healthBar;
    private Timer _damageIntervalTimer;
    private Timer _swordAbilityControllerTimer;
    private Node _abilities;
    private AnimationPlayer _animationPlayer;
    private Sprite2D _sprite2D;
    private int _numberCollidingBodies = 0;

    private Area2D _collisionArea;
    public override void _Ready()
    {
        _collisionArea = GetNode<Area2D>("CollisionArea2D");
        _damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");
        _healthComponent = GetNode<HealthComponent>("HealthComponent");
        _healthBar = GetNode<ProgressBar>("HealthBar");
        _abilities = GetNode<Node>("Abilities");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _sprite2D = GetNode<Sprite2D>("Sprite2D");

        _swordAbilityControllerTimer = _swordAbilityController.Timer;

        _collisionArea.BodyEntered += OnCollisionAreaEntered;
        _collisionArea.BodyExited += OnCollisionAreaExited;
        _damageIntervalTimer.Timeout += () => CheckDealDamage();
        _healthComponent.HealthChanged += OnHealthChanged;
        GameEvents.AbilityUpgradeAdded += OnAbilityUpgradeAdded;

        UpdateHealthDisplay();
    }

    public override void _PhysicsProcess(double delta)
    {
        var movementVector = GetMovementVector();
        var direction = movementVector.Normalized();
        var targetVelocity = direction * Speed;

        Velocity = Velocity.Lerp(targetVelocity, 1 -  (float)Mathf.Exp(-delta * AccelerationSmoothing));

        if(Input.IsActionJustPressed(GameConstants.INPUT_LEFT_CLICK))
		{
			if(_swordAbilityControllerTimer.IsStopped())
			{
				Attack();
				_swordAbilityControllerTimer.Start();
			}
		}

        if(movementVector.X != 0 || movementVector.Y != 0)
        {
            if(movementVector.X < 0)
                _sprite2D.FlipH = true;
            else if(movementVector.X > 0)
                _sprite2D.FlipH = false;

            _animationPlayer.Play(GameConstants.ANIM_MOVE);
        }
        else
            _animationPlayer.Play("RESET");

        
        MoveAndSlide();

        // if(movementVector.X != 0 || movementVector.Y != 0)
        // {
        //     _animationPlayer.Play(GameConstants.ANIM_MOVE);

        //     if(movementVector.X > 0)
        
        //         _sprite2D.FlipH=false;
        //     else
        //         _sprite2D.FlipH = true;
        // }
        // else
        //     _animationPlayer.Play("Reset");

    }

    private void OnAbilityUpgradeAdded(AbilityUpgrade upgrade, List<AbilityUpgrade> list)
    {
        if(upgrade is not Ability)
            return;
        var ability = upgrade as Ability;
        _abilities.AddChild(ability.AbilityControllerScene.Instantiate());

    }

    private void OnCollisionAreaEntered(Node2D body)
    {
        _numberCollidingBodies++;
        CheckDealDamage();
    }

    private void OnCollisionAreaExited(Node2D body)
    {
        _numberCollidingBodies--;
    }

    private void OnHealthChanged()
    {
        UpdateHealthDisplay();
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

        private void Attack()
	{
		var mousePosition = GetViewport().GetCamera2D().GetLocalMousePosition();

		var swordAbilityInstance = _swordAbility.Instantiate() as SwordAbility;

		var foregroundLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_FOREGROUND_LAYER);

		foregroundLayer.AddChild(swordAbilityInstance);

		swordAbilityInstance.HitboxComponent.Damage = GameConstants.SKILL_SWORD_DMG;

		swordAbilityInstance.GlobalPosition = mousePosition;
		swordAbilityInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;
	}

    private void CheckDealDamage()
    {
        if(_numberCollidingBodies == 0 || !_damageIntervalTimer.IsStopped())
            return;

        _healthComponent.Damage(1);
        _damageIntervalTimer.Start();
    }
}
