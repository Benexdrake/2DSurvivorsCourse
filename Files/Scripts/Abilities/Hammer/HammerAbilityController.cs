using Godot;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class HammerAbilityController : Node
{
	[Export] public PackedScene HammerAbility { get; private set; }
	[Export] public int Range { get; private set; } = 150;

	[Export] public Timer Timer { get; set; }

	public int BaseAttack { get; set; }

	
	public override void _Ready()
	{
		if(Owner is Player)
        {
            var player = Owner as Player;
			BaseAttack = player.Attack;
        }
		GameEvents.AbilityUpgradeAdded += HandelAbilityUpgradeAdded;
	}

	public void Attack(Vector2 playerPosition)
	{
		if(!Timer.IsStopped())
			return;

		var mousePosition = GetViewport().GetCamera2D().GetLocalMousePosition();

		var hammerAbilityInstance = HammerAbility.Instantiate() as HammerAbility;

		var foregroundLayer = GetTree().GetFirstNodeInGroup(GameConstants.GROUP_FOREGROUND_LAYER);

		foregroundLayer.AddChild(hammerAbilityInstance);

		hammerAbilityInstance.HitboxComponent.Damage = BaseAttack;


		hammerAbilityInstance.GlobalPosition = playerPosition;
		//hammerAbilityInstance.GlobalPosition += Vector2.Right.Rotated((float)GD.RandRange(0, Mathf.Tau)) * 4;
		//hammerAbilityInstance.Rotate();
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
            BaseAttack++;
    }
}
