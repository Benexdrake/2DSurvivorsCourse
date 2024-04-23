using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Main : Node
{
    private Player _player;
    [Export] private PackedScene _endScreenScene;
    [Export] private PackedScene _pauseMenuScene;
    private CanvasLayer _characterScene;
    public override void _Ready()
    {

        _player = GetNode<Player>($"%{GameConstants.PLAYER}");
        _player._healthComponent.Died += HandlePlayerDied;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if(@event.IsActionPressed("pause"))
        {
            AddChild(_pauseMenuScene.Instantiate());
            GetTree().Root.SetInputAsHandled();
        }            
    }

    private void HandlePlayerDied()
    {
        var endScreenInstance = _endScreenScene.Instantiate() as EndScreen;
        AddChild(endScreenInstance);
        endScreenInstance.SetDefeat();
    }
}
