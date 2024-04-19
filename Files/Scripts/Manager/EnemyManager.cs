using Godot;
using System;

public partial class EnemyManager : Node
{
	[Export] public PackedScene Enemy { get; private set; }
	[Export] public Timer Timer { get; private set; }
	[Export] public int SpawnRadius { get; private set; } = 350;

	private int timerCount = 0;
	private int spawnUp = 10;
	private int maxSpawn = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Timer.Timeout += HandleTimer;
	}

    public override void _Process(double delta)
    {
        
    }

    private void HandleTimer()
    {
		// GD.Print("Timer Count:" + timerCount);
		// GD.Print("Max Spawn:" + maxSpawn);

		timerCount++;

		if (timerCount == spawnUp)
		{
			timerCount = 0;
			maxSpawn++;
		}

		for (int i = 0; i < maxSpawn; i++)
		{
			var player = GetTree().GetFirstNodeInGroup("player") as Node2D;

			if (player == null)
				return;

			var randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));
			var spawnPosition = player.GlobalPosition + (randomDirection * SpawnRadius);

			var enemy = Enemy.Instantiate() as Node2D;

			GetParent().GetParent().AddChild(enemy);
			enemy.GlobalPosition = spawnPosition;
		}
    }
}
