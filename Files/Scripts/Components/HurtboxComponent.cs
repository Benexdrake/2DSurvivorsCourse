using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
    private HealthComponent _healthComponent;

    public override void _Ready()
    {
        _healthComponent = Owner.GetNode<HealthComponent>("HealthComponent");
        AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area2D area)
    {
        if(area is not HitboxComponent)
            return;

        var hitboxComponent = area as HitboxComponent;

        _healthComponent.Damage(hitboxComponent.Damage);
        GD.Print(_healthComponent.MaxHealth);
        GD.Print(_healthComponent.CurrentHealth);
    }

}
