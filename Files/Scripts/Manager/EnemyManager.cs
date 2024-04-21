using Godot;

public partial class EnemyManager : Node
{
	[Export] public PackedScene Enemy { get; private set; }
	[Export] public Timer Timer { get; private set; }
	[Export] public int SpawnRadius { get; private set; } = 350;
	[Export] public ArenaTimeManager ArenaTimeManager { get; private set; }

	private double _baseSpawnTime = 0;

	private int timerCount = 0;
	private int spawnUp = 10;
	private int maxSpawn = 1;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_baseSpawnTime = Timer.WaitTime;
		Timer.Timeout += HandleTimer;
		ArenaTimeManager.ArenaDifficultyIncreased += HandleArenaDifficultyIncreased;
	}

    private void HandleArenaDifficultyIncreased(int arenaDifficulty)
    {
        var timeOff = .1 / 12 * arenaDifficulty;
		timeOff = Mathf.Min(timeOff, .7);
		Timer.WaitTime = _baseSpawnTime - timeOff;
    }

	private Vector2 GetSpawnPosition()
	{
		if (GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) is not Node2D player)
            return Vector2.Zero;
		
		var spawnPosition = Vector2.Zero;
		var randomDirection = Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau));

		for (int i = 0; i < 4; i++)
		{
			spawnPosition = player.GlobalPosition + (randomDirection * SpawnRadius);
			var additionalCheckOffset = randomDirection * 20;

			var queryParameters = PhysicsRayQueryParameters2D.Create(player.GlobalPosition, spawnPosition + additionalCheckOffset, 1<<0);
			var result = GetTree().Root.World2D.DirectSpaceState.IntersectRay(queryParameters);
			
			if(result.Count <= 0)
				break;

			randomDirection = randomDirection.Rotated(Mathf.DegToRad(90));
		}
		return spawnPosition;
	}

    private void HandleTimer()
    {
		Timer.Start();

		timerCount++;

		if (timerCount == spawnUp)
		{
			timerCount = 0;
			maxSpawn++;
		}

		var enemy = Enemy.Instantiate() as Node2D;
		var entitiesLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_ENTITIES_LAYER);
		entitiesLayer.AddChild(enemy);
		enemy.GlobalPosition = GetSpawnPosition();
    }
}
