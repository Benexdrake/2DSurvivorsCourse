using Godot;
using System;

public partial class ArenaTimeUI : CanvasLayer
{
	[Export] public ArenaTimeManager ArenaTimeManager { get; private set; }
	[Export] public Label Label { get; private set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(ArenaTimeManager == null)
			return;
		var timeElapsed = ArenaTimeManager.GetTimeElapsed();
		Label.Text = FormatSecondsToString(timeElapsed);
	}

	private string FormatSecondsToString(double seconds)
	{
		var minutes = Mathf.Floor(seconds / 60);

		var remainSeconds = seconds - (minutes * 60);

		if(remainSeconds < 10)
			return $"{minutes}:0{Mathf.Floor(remainSeconds)}";
		
			return $"{minutes}:{Mathf.Floor(remainSeconds)}";
	}
}
