using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class UpgradeManager : Node
{
    [Export] public AbilityUpgrade[] UpgradePool { get; private set; }
    [Export] public ExperienceManager ExperienceManager { get; private set; }
    [Export] public PackedScene UpgradeScreenScene { get; private set; }

    private List<AbilityUpgrade> _currentUpgrade = new();

    public override void _Ready()
    {
        ExperienceManager.LevelUp += HandlerLevelUp;
        _currentUpgrade.Add(new AbilityUpgrade(){Id="1", Name="Test", Description="This is a Test for the Ability"});
    }

    private void HandlerLevelUp(int level)
    {
        var choosenUpgrade = UpgradePool.FirstOrDefault();
        if (choosenUpgrade == null)
            return;

        var upgradeScreenInstantiate = UpgradeScreenScene.Instantiate() as UpgradeScreen;
        AddChild(upgradeScreenInstantiate);

        List<AbilityUpgrade> upgradeList = new();
        upgradeList.Add(choosenUpgrade);

        upgradeScreenInstantiate.SetAbilityUpgrade(upgradeList);
    }

    private void ApplyUpgrade(AbilityUpgrade upgrade)
    {

    }

}
