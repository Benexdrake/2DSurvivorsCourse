using Godot;
using System;
using System.Collections.Generic;

public partial class UpgradeScreen : CanvasLayer
{
    [Export] private PackedScene UpgradeCardScene;
    [Export] private HBoxContainer _cardContainer;
    [Signal] public delegate void UpgradeSelectedEventHandler(AbilityUpgrade upgrade);

    public override void _Ready()
    {
        GetTree().Paused = true;
    }
    public void SetAbilityUpgrade(List<AbilityUpgrade> upgrades)
    {
        foreach(var upgrade in upgrades)
        {
            if (UpgradeCardScene.Instantiate() is not AbilityUpgradeCard instance)
                return;
            _cardContainer.AddChild(instance);
            instance.SetAbilityUpgrade(upgrade);
            instance.Selected += () => HandleSelectedAbility(upgrade);
        }
    }

    private void HandleSelectedAbility(AbilityUpgrade upgrade)
    {
        EmitSignal(SignalName.UpgradeSelected, upgrade);
        GetTree().Paused = false;
        QueueFree();
    }

}
