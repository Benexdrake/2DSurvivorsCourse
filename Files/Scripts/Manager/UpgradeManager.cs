using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

public partial class UpgradeManager : Node
{
    [Export] public AbilityUpgrade[] UpgradePool { get; private set; }
    [Export] public ExperienceManager ExperienceManager { get; private set; }
    [Export] public PackedScene UpgradeScreenScene { get; private set; }

    private List<AbilityUpgrade> _currentUpgrades = new();

    public override void _Ready()
    {
        ExperienceManager.LevelUp += HandlerLevelUp;
    }

    private void HandlerLevelUp(int level)
    {
       var upgradeList = PickUpgrades();

        var upgradeScreenInstantiate = UpgradeScreenScene.Instantiate() as UpgradeScreen;
        AddChild(upgradeScreenInstantiate);

        upgradeScreenInstantiate.SetAbilityUpgrade(upgradeList);
        upgradeScreenInstantiate.UpgradeSelected += HandleUpgradeSelected;
    }

    private void HandleUpgradeSelected(AbilityUpgrade upgrade)
    {
        ApplyUpgrade(upgrade);
    }


    private void ApplyUpgrade(AbilityUpgrade upgrade)
    {
        GD.Print(upgrade.Name);
        var cu = _currentUpgrades.FirstOrDefault(x => x.Id.Equals(upgrade.Id));

        var sortedUpgrades = _currentUpgrades;

        if (cu == null)
        {   
            upgrade.Quantity = 1;
            _currentUpgrades.Add(upgrade);   
        }
        else
        {
            upgrade.Quantity++;
        }
        GameEvents.EmitAbilityUpgradeAdded(upgrade, sortedUpgrades);
    }

    private List<AbilityUpgrade> PickUpgrades()
    {
        var filteredUpgrades = UpgradePool.ToList().Where(x => x.MaxQuantity > x.Quantity).ToList();
        var newUpgrades = new List<AbilityUpgrade>();

        for (int i = 0; i < 3; i++)
        {
            Random rand = new();
            var shuffeledList = filteredUpgrades.OrderBy(x => rand.Next()).ToList();
            var upgrade = shuffeledList.FirstOrDefault();
            if (upgrade == null)
                break;
            newUpgrades.Add(upgrade);
            filteredUpgrades.Remove(upgrade);
        }
        return newUpgrades;
    }

}
