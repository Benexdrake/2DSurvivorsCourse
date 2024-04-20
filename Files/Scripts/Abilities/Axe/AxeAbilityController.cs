using Godot;
using System;

public partial class AxeAbilityController : Node
{
    [Export] private PackedScene AxeAbilityScene;
    private Timer _timer;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += HandleTimeout;
    }

    private void HandleTimeout()
    {
        var player = GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) as Player;
        if (player == null)
            return;

        var foreground = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_FOREGROUND_LAYER) as Node2D;
        if (foreground == null)
            return;

        var axeInstance = AxeAbilityScene.Instantiate() as AxeAbility;
        foreground.AddChild(axeInstance);
        axeInstance.GlobalPosition = player.GlobalPosition;
        axeInstance.HitboxComponent.Damage = GameConstants.SKILL_AXE_DMG;
    }

}
