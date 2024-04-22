using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Node
{
    private Player _player;
    [Export] private PackedScene _endScreenScene;
    public override void _Ready()
    {
        _player = GetNode<Player>($"%{GameConstants.PLAYER}");
        _player._healthComponent.Died += HandlePlayerDied;
    }

    private void HandlePlayerDied()
    {
        var endScreenInstance = _endScreenScene.Instantiate() as EndScreen;
        AddChild(endScreenInstance);
        endScreenInstance.SetDefeat();
    }
}
