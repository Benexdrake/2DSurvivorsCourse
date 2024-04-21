using Godot;
using System;

public abstract partial class Enemy : CharacterBody2D
{
  [Export] public int MaxHealth { get; private set; }
  [Export(PropertyHint.Range,"1,200,1")]
  public int MaxSpeed { get; private set; } = 40;
    
  [Export(PropertyHint.Range,"1,200,1")]
  public float Acceleration { get; private set; } = 5;
  [Export] private Texture2D texture2D;
  
	private VelocityComponent _velocityComponent;
	private HealthComponent _healthComponent;
  private Sprite2D _sprite2D;

  public override void _Ready()
  {
    _healthComponent = GetNode<HealthComponent>("HealthComponent");
    _healthComponent.MaxHealth = MaxHealth;
    _velocityComponent = GetNode<VelocityComponent>("VelocityComponent");
    _sprite2D = GetNode<Sprite2D>("Visuals/Sprite2D");
    _sprite2D.Texture = texture2D;
    _velocityComponent.Ready();
  }

  public override void _PhysicsProcess(double delta)
  {
	  _velocityComponent.AccelerateToPlayer();
	  _velocityComponent.Move(this);
  }
}
