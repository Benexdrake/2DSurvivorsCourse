using Godot;
using System;

public partial class HurtboxComponent : Area2D
{
    private HealthComponent _healthComponent;
    [Export] private PackedScene _floatingText;
    [Signal] public delegate void HitEventHandler();

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
        var floatingText = _floatingText.Instantiate() as FloatingText;
        GetTree().GetFirstNodeInGroup(GameConstants.GROUP_FOREGROUND_LAYER).AddChild(floatingText);
        floatingText.GlobalPosition = GlobalPosition + Vector2.Up * 16;
        floatingText.Start(hitboxComponent.Damage.ToString());
        EmitSignal(SignalName.Hit);
    }

}
