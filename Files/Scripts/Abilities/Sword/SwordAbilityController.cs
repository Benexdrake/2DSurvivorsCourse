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

	private int _baseDmg = GameConstants.SKILL_SWORD_DMG;

	
	public override void _Ready()
	{
		GameEvents.AbilityUpgradeAdded += HandelAbilityUpgradeAdded;
	}

	public void Attack()
	{
		var mousePosition = GetViewport().GetCamera2D().GetLocalMousePosition();

		var swordAbilityInstance = SwordAbility.Instantiate() as SwordAbility;

		var foregroundLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_FOREGROUND_LAYER);

		foregroundLayer.AddChild(swordAbilityInstance);

		swordAbilityInstance.HitboxComponent.Damage = _baseDmg;

		swordAbilityInstance.GlobalPosition = mousePosition;
		swordAbilityInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;
		Timer.Start();
	}

    private void HandelAbilityUpgradeAdded(AbilityUpgrade upgrade, List<AbilityUpgrade> upgrades)
    {
		if (upgrade.Id.Equals(GameConstants.ABILITY_SWORD_RATE))
		{
			var swordRateAbility = upgrades.FirstOrDefault(x => x.Id.Equals(GameConstants.ABILITY_SWORD_RATE));
			Timer.WaitTime *= 1- (swordRateAbility.Quantity * .1);
		}
		else if(upgrade.Id.Equals(GameConstants.ABILITY_SWORD_DMG))
            _baseDmg++;
    }
}
