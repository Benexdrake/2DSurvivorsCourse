using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export(PropertyHint.Range,"1,200,0.5")] public int Speed { get; private set; } = 50;
	[Export] public Area2D HitboxNode { get; private set; }

	[Export(PropertyHint.Range,"1,100,1")] public int LifePoints { get; set; } = 1;
	[Export] public Sprite2D sprite { get; private set; }
	[Export] public Texture2D texture { get; private set; }

	[Export] public HealthComponent HealthComponent { get; set; }

	private Sprite2D _sprite2D;

    public override void _Ready()
    {
		_sprite2D = GetNode<Sprite2D>("Sprite2D");
    }

    public override void _PhysicsProcess(double delta)
    {
        Velocity = GetDirectionToPlayer() * Speed;

		if(Velocity.X > 0)
			_sprite2D.FlipH = true;
		else
			_sprite2D.FlipH = false;

		if(Velocity.Y > 0)
			_sprite2D.FlipV = false;
		else
			_sprite2D.FlipV = true;

		MoveAndSlide();
    }


    private Vector2 GetDirectionToPlayer()
	{
		var player = GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) as Node2D;

		if (player == null)
			return Vector2.Zero;

		return (player.GlobalPosition - GlobalPosition).Normalized();
	}
}
