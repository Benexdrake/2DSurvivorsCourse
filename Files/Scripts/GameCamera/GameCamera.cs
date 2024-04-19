using Godot;
using System;
using System.Linq;

public partial class GameCamera : Camera2D
{
	public Vector2 TargetPosition { get; private set; } = Vector2.Zero;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		MakeCurrent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		AquireTarget();
		GlobalPosition = GlobalPosition.Lerp(TargetPosition, 1.0f - (float)Mathf.Exp(-delta * 20));
	}

	private void AquireTarget()
	{
		var playerNode = GetTree().GetNodesInGroup("player").FirstOrDefault() as Node2D;
		if(playerNode == null)
			return;
		Offset = playerNode.GlobalPosition;
	}
}
