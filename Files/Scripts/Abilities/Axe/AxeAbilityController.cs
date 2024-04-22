using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class AxeAbilityController : Node
{
    [Export] private PackedScene AxeAbilityScene;
    private Timer _timer;

    private int _baseDmg = GameConstants.SKILL_AXE_DMG;

    public override void _Ready()
    {
        _timer = GetNode<Timer>("Timer");
        _timer.Timeout += HandleTimeout;
        GameEvents.AbilityUpgradeAdded += HandeAbilityUpgradeAdded;
    }

    public override void _ExitTree()
    {
        GameEvents.AbilityUpgradeAdded -= HandeAbilityUpgradeAdded;
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
        axeInstance.HitboxComponent.Damage = _baseDmg;
    }

    private void HandeAbilityUpgradeAdded(AbilityUpgrade upgrade, List<AbilityUpgrade> upgrades)
    {
        GD.Print("HI");
		if (upgrade.Id.Equals(GameConstants.ABILITY_AXE_DMG))
			_baseDmg++;
    }

}
