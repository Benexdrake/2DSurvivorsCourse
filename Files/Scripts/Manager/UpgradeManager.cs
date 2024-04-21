using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UpgradeManager : Node
{
    private List<AbilityUpgrade> _upgradePool = new();
    [Export] public ExperienceManager ExperienceManager { get; private set; }
    [Export] public PackedScene UpgradeScreenScene { get; private set; }
    

    [Export] private Resource _upgradeAxe;
    [Export] private Resource _upgradeAxeDamage;
    [Export] private Resource _upgradeSwordRate;
    [Export] private Resource _upgradeSwordDamage;

    private List<AbilityUpgrade> _currentUpgrades = new();

    public override void _Ready()
    {
        _upgradePool.Add(_upgradeAxe as AbilityUpgrade);
        _upgradePool.Add(_upgradeSwordRate as AbilityUpgrade);
        _upgradePool.Add(_upgradeSwordDamage as AbilityUpgrade);
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
        ProofUpgrades();
        var filteredUpgrades = _upgradePool.Where(x => x.MaxQuantity > x.Quantity).ToList();
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

    private void ProofUpgrades()
    {
        var axeUpgrade = _currentUpgrades.FirstOrDefault(x => x.Id.Equals((_upgradeAxe as Ability).Id));
        if (axeUpgrade != null)
        {
            var axeDamage = _currentUpgrades.FirstOrDefault(y => y.Id.Equals((_upgradeAxeDamage as Ability).Id));
            if(axeDamage == null)
            {
                _upgradePool.Add(_upgradeAxeDamage as Ability);
            }
        }
    }

}
