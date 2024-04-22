using Godot;
using System;
using System.Collections.Generic;

public partial class UpgradeScreen : CanvasLayer
{
    [Export] private PackedScene UpgradeCardScene;
    [Export] private HBoxContainer _cardContainer;
    [Signal] public delegate void UpgradeSelectedEventHandler(AbilityUpgrade upgrade);
    private AnimationPlayer _animationPlayer;

    public override void _Ready()
    {
        _animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        GetTree().Paused = true;
    }
    public void SetAbilityUpgrade(List<AbilityUpgrade> upgrades)
    {
        float delay = 0;
        foreach(var upgrade in upgrades)
        {
            if (UpgradeCardScene.Instantiate() is not AbilityUpgradeCard instance)
                return;
            _cardContainer.AddChild(instance);
            instance.SetAbilityUpgrade(upgrade);
            instance.PlayIn(delay);
            instance.Selected += () => HandleSelectedAbility(upgrade);
            delay += .2f;
        }
    }

    private async void HandleSelectedAbility(AbilityUpgrade upgrade)
    {
        EmitSignal(SignalName.UpgradeSelected, upgrade);
        _animationPlayer.Play(GameConstants.ANIM_OUT);

        await ToSignal(_animationPlayer, "animation_finished");
        
        GetTree().Paused = false;
        QueueFree();
    }

}
