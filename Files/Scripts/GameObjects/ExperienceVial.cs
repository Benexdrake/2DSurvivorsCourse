using Godot;
using System;

public partial class ExperienceVial : Node2D
{
	[Export] public Area2D Area { get; private set; }
	public override void _Ready()
	{
		Area.AreaEntered += HandleAreaEntered;
	}

    private void HandleAreaEntered(Area2D area)
    {
        var player = area.GetParent() as Player;
		if(player == null)
			return;

		// Bewegung Richtung Player

		//Wenn this == player position dann Delete
		QueueFree();
    }
}
