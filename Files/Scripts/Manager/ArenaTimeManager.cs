using Godot;
using System;

public partial class ArenaTimeManager : Node
{
	private Timer _timer;
	[Export] public PackedScene EndScreenScene { get; private set; }
	[Signal] public delegate void ArenaDifficultyIncreasedEventHandler(int arenaDifficulty);

	private int _arenaDifficulty = 0;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
		_timer.Timeout += HandleTimeout;
    }

    public override void _Process(double delta)
    {
        var nextTimeTarget = _timer.WaitTime - ((_arenaDifficulty + 1) * GameConstants.DIFFICULTY_INTERVAL);
		if(_timer.TimeLeft <= nextTimeTarget)
		{
			_arenaDifficulty++;
			EmitSignal(SignalName.ArenaDifficultyIncreased, _arenaDifficulty);
		}
    }

    private void HandleTimeout()
    {
        var victoryScreenInstance = EndScreenScene.Instantiate() as EndScreen;
		AddChild(victoryScreenInstance);
		victoryScreenInstance.SetWin();
		victoryScreenInstance.PlayJingle(false);
    }


    public double GetTimeElapsed()
	{
		return _timer.WaitTime - _timer.TimeLeft;
	}
}
