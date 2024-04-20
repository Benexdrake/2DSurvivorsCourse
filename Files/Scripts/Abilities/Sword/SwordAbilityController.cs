using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class SwordAbilityController : Node
{
	[Export] public PackedScene SwordAbility { get; private set; }
	[Export] public int Range { get; private set; } = 150;

	[Export] public Timer Timer { get; set; }

	
	public override void _Ready()
	{
		GameEvents.AbilityUpgradeAdded += HandeAbilityUpgradeAdded;
	}

    private void HandeAbilityUpgradeAdded(AbilityUpgrade upgrade, List<AbilityUpgrade> upgrades)
    {
		if (!upgrade.Id.Equals(GameConstants.ABILITY_SWORD_RATE))
			return;

		var swordRateAbility = upgrades.FirstOrDefault(x => x.Id.Equals(GameConstants.ABILITY_SWORD_RATE));

		Timer.WaitTime *= 1- (swordRateAbility.Quantity * .1);
    }


    // public override void _PhysicsProcess(double delta)
    // {
    //     if(Input.IsActionJustPressed(GameConstants.INPUT_LEFT_CLICK))
	// 	{
	// 		if(Timer.IsStopped())
	// 		{
	// 			Attack();
	// 			Timer.Start();
	// 		}
	// 	}
    // }

    // private void Attack()
	// {
	// 	var mousePosition = GetViewport().GetCamera2D().GetLocalMousePosition();

	// 	var player = GetTree().GetFirstNodeInGroup(GameConstants.PLAYER) as Node2D;

	// 	if (player == null)
	// 		return;

	// 	var swordAbilityInstance = SwordAbility.Instantiate() as SwordAbility;

	// 	var foregroundLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_FOREGROUND_LAYER);

	// 	foregroundLayer.AddChild(swordAbilityInstance);

	// 	swordAbilityInstance.HitboxComponent.Damage = GameConstants.SKILL_SWORD_DMG;

	// 	swordAbilityInstance.GlobalPosition = mousePosition;
	// 	swordAbilityInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;
	// }
}
