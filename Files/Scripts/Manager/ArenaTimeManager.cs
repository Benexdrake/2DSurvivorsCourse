using Godot;
using System;

public partial class ArenaTimeManager : Node
{
	[Export] public Timer Timer { get; private set; }
	public double GetTimeElapsed()
	{
		return Timer.WaitTime - Timer.TimeLeft;
	}
}
