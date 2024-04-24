using Godot;
using System;
using System.Collections.Generic;

public partial class Player : CharacterBody2D
{
    
    public int Attack { get; set; } = GameStats.attack;
    public int HP { get; set; } = GameStats.hp;
    public int Speed { get; set; } = GameStats.speed;
    [Export] public int Acceleration { get; private set; } = 50;
    [Export] private SwordAbilityController _swordAbilityController;
    [Export] private HammerAbilityController _hammerAbilityController;
    public HealthComponent _healthComponent {get; private set;}

    private ProgressBar _healthBar;
    private Timer _damageIntervalTimer;
    private Node _abilities;
    private AnimationPlayer _animationPlayer;
    private Sprite2D _sprite2D;
    private VelocityComponent _velocityComponent;
    private int _numberCollidingBodies = 0;
    private int _baseDmg = GameConstants.SKILL_SWORD_DMG;
    

    private Area2D _collisionArea;
    public override void _Ready()
    {
        _sprite2D = GetNode<Sprite2D>("Visuals/Sprite2D");
        _sprite2D.Texture = GameStats.texture;
        _collisionArea = GetNode<Area2D>("CollisionArea2D");
        _damageIntervalTimer = GetNode<Timer>("DamageIntervalTimer");
        _healthComponent = GetNode<HealthComponent>("HealthComponent");
        _healthBar = GetNode<ProgressBar>("HealthBar");
        _abilities = GetNode<Node>("Abilities");
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        _velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
        _collisionArea.BodyEntered += OnCollisionAreaEntered;
        _collisionArea.BodyExited += OnCollisionAreaExited;
        _damageIntervalTimer.Timeout += () => CheckDealDamage();
        _healthComponent.HealthChanged += OnHealthChanged;
        GameEvents.AbilityUpgradeAdded += OnAbilityUpgradeAdded;
        UpdateHealthDisplay();
    }

    public override void _ExitTree()
    {
        GameEvents.AbilityUpgradeAdded -= OnAbilityUpgradeAdded;
    }

    private void OnAbilityUpgradeAdded(AbilityUpgrade upgrade, List<AbilityUpgrade> list)
    {
        if(upgrade is not Ability)
            return;
        var ability = upgrade as Ability;
        if(ability.Id.Equals(GameConstants.ABILITY_PLAYER_SPEED))
        {
            Speed += 25;
            return;
        }
        _abilities.AddChild(ability.AbilityControllerScene.Instantiate());
    }

    public override void _PhysicsProcess(double delta)
    {
        var movementVector = GetMovementVector();
        var direction = movementVector.Normalized();

        if(movementVector.X != 0 || movementVector.Y != 0)
            _animationPlayer.Play(GameConstants.ANIM_WALK);
        else
            _animationPlayer.Play("RESET");
        _velocityComponent.AccelerateInDirection(direction);

        _velocityComponent.Move(this);

        if(Input.IsActionJustPressed(GameConstants.INPUT_LEFT_CLICK))
		{
			_swordAbilityController.Attack();
			//_hammerAbilityController.Attack(GlobalPosition);
		}
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
        GameEvents.EmitPlayerDamaged();
        UpdateHealthDisplay();
        GetNode<AudioStreamPlayer2D>("AudioStreamPlayer2D").Play();
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

    private void CheckDealDamage()
    {
        if(_numberCollidingBodies == 0 || !_damageIntervalTimer.IsStopped())
            return;

        _healthComponent.Damage(1);
        _damageIntervalTimer.Start();
    }

    public void SetCharacter(int atk, int speed, int hp, Texture2D texture)
    {
        _sprite2D.Texture = texture;
        Attack = atk;
        Speed = speed;
        HP = hp;
    }
}
