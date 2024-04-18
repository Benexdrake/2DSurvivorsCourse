using Godot;
using System;

public partial class Enemy : CharacterBody2D
{
	[Export(PropertyHint.Range,"1,200,0.5")] public int Speed { get; private set; } = 75;
    public override void _Ready()
    {

    }

    public override void _Process(double delta)
	{
		Velocity = GetDirectionToPlayer() * Speed;
		MoveAndSlide();
	}

	private Vector2 GetDirectionToPlayer()
	{
		var player = GetTree().GetFirstNodeInGroup("player") as Node2D;

		if (player == null)
			return Vector2.Zero;

		return (player.GlobalPosition - GlobalPosition).Normalized();
	}
}
