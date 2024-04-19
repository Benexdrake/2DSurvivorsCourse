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
        GameEvents.EmitExperienceVialCollected(1);
		QueueFree();
    }

    private void HandleExperienceVialCollected(int obj)
    {
        throw new NotImplementedException();
    }

}
