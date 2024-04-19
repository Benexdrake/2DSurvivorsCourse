using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export(PropertyHint.Range,"1,200,0.5")] public int Speed { get; private set; } = 50;
	[Export] public Area2D HitboxNode { get; private set; }

	[Export(PropertyHint.Range,"1,100,1")] public int LifePoints { get; set; } = 1;
	[Export] public Sprite2D sprite { get; private set; }
	[Export] public Texture2D texture { get; private set; }


    public override void _Ready()
    {
		HitboxNode.AreaEntered += HandleHitboxEntered;
		sprite.Texture = texture;
    }



    public override void _Process(double delta)
	{
		Velocity = GetDirectionToPlayer() * Speed;
		MoveAndSlide();
	}
    private void HandleHitboxEntered(Area2D area)
    {
		LifePoints--;

		if(LifePoints <= 0)
			QueueFree();
    }

	private Vector2 GetDirectionToPlayer()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (player == null)
			return Vector2.Zero;

		return (player.GlobalPosition - GlobalPosition).Normalized();
	}
}
