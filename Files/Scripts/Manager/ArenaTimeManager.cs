using Godot;
using System;

public partial class ArenaTimeManager : Node
{
	private Timer _timer;
	[Export] public PackedScene VictoryScreenScene { get; private set; }

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");

		_timer.Timeout += HandleTimeout;
    }

    private void HandleTimeout()
    {
        var victoryScreenInstance = VictoryScreenScene.Instantiate();
		AddChild(victoryScreenInstance);
    }


    public double GetTimeElapsed()
	{
		return _timer.WaitTime - _timer.TimeLeft;
	}
}
