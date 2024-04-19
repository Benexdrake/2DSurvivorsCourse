using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
    [Export] public HealthComponent HealthComponent { get; private set; }

    public override void _Ready()
    {
        AreaEntered += HandleAreaEntered;
    }

    private void HandleAreaEntered(Area2D area)
    {
        if(area is not HitboxComponent)
            return;

        var hitboxComponent = area as HitboxComponent;

        HealthComponent.Damage(hitboxComponent.Damage);
    }

}
